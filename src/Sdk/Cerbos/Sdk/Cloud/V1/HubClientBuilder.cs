// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Cerbos.Api.Cloud.V1.ApiKey;
using Cerbos.Api.Cloud.V1.Store;
using Cerbos.Sdk.Cloud.V1.Interceptor;
using Cerbos.Sdk.Cloud.V1.Store;
using Cerbos.Sdk.Cloud.V1.ApiKey;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace Cerbos.Sdk.Cloud.V1
{
    public sealed class HubClientBuilder
    {
        private string Target { get; }
        private string CaCertificate { get; set; }
        private Credentials Credentials { get; set; }
        private bool Plaintext { get; set; }

        private HubClientBuilder(string target)
        {
            Target = target;
        }

        public static HubClientBuilder ForTarget(string target)
        {
            return new HubClientBuilder(target);
        }

        public HubClientBuilder WithCaCertificate(string path) {
            CaCertificate = path;
            return this;
        }

        public HubClientBuilder WithCredentials(Credentials credentials)
        {
            Credentials = credentials;
            return this;
        }

        public HubClientBuilder WithCredentials(string clientId, string clientSecret)
        {
            Credentials = new Credentials(clientId, clientSecret);
            return this;
        }

        public HubClientBuilder WithPlaintext()
        {
            Plaintext = true;
            return this;
        }

        public IHubClient Build()
        {
            if (string.IsNullOrEmpty(Target))
            {
                throw new Exception("Target must be specified");
            }

            if (CaCertificate != null && Plaintext)
            {
                throw new Exception("CaCertificate and plaintext must not be specified at the same time");
            }

            if (Credentials == null)
            {
                throw new Exception("Credentials must be specified");
            }

            var channelOptions = new GrpcChannelOptions{};
            if (CaCertificate != null)
            {
                var handler = new HttpClientHandler();
                var cert = new X509Certificate(CaCertificate);
                handler.ClientCertificates.Add(cert);
                channelOptions.HttpHandler = handler;
            }
            else if (!Plaintext)
            {
                channelOptions.Credentials = ChannelCredentials.SecureSsl;
            }

            var authInterceptor = new AuthInterceptor(
                new ApiKeyClient(
                    new ApiKeyService.ApiKeyServiceClient(
                        GrpcChannel.ForAddress(
                            Target,
                            channelOptions
                        )
                    ),
                    Resilience.NewInstance().WithCircuitBreaker().WithRetry(StatusCode.ResourceExhausted)
                ),
                Credentials
            );

            var resilience = Resilience.NewInstance().WithCircuitBreaker().WithRetry(StatusCode.Internal, StatusCode.Unavailable);
            var channelWithInterceptor = GrpcChannel.ForAddress(Target, channelOptions).Intercept(authInterceptor);
            return new HubClient(
                new ApiKeyClient(new ApiKeyService.ApiKeyServiceClient(channelWithInterceptor), resilience),
                new StoreClient(new CerbosStoreService.CerbosStoreServiceClient(channelWithInterceptor), resilience)
            );
        }
    }
}