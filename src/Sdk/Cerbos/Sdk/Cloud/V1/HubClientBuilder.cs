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

namespace Cerbos.Sdk.Cloud.V1
{
    public sealed class HubClientBuilder
    {
        private string Target { get; }
        private HubCredentials HubCredentials { get; set; }
        private bool Plaintext { get; set; }

        private HubClientBuilder(string target)
        {
            Target = target;
        }

        public static HubClientBuilder ForTarget(string target)
        {
            return new HubClientBuilder(target);
        }

        public HubClientBuilder WithCredentials(HubCredentials hubCredentials)
        {
            HubCredentials = hubCredentials;
            return this;
        }

        public HubClientBuilder WithCredentials(string clientId, string clientSecret)
        {
            HubCredentials = new HubCredentials(clientId, clientSecret);
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

            if (HubCredentials == null)
            {
                throw new Exception("Credentials must be specified");
            }

            var grpcChannelOptions = new GrpcChannelOptions();
            if (!Plaintext)
            {
                grpcChannelOptions.Credentials = ChannelCredentials.SecureSsl;
            }

            var authInterceptor = new AuthInterceptor(
                new ApiKeyClient(new ApiKeyService.ApiKeyServiceClient(GrpcChannel.ForAddress(Target, grpcChannelOptions))),
                HubCredentials
            );

            var grpcChannel = GrpcChannel.ForAddress(Target, grpcChannelOptions).Intercept(authInterceptor);

            return new HubClient(
                new ApiKeyClient(new ApiKeyService.ApiKeyServiceClient(grpcChannel)),
                new StoreClient(new CerbosStoreService.CerbosStoreServiceClient(grpcChannel))
            );
        }
    }
}