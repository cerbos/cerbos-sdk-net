// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Cerbos.Api.Cloud.V1.ApiKey;
using Cerbos.Api.Cloud.V1.Store;
using Cerbos.Sdk.Cloud.V1.Interceptor;
using Cerbos.Sdk.Cloud.V1.Store;
using Cerbos.Sdk.Cloud.V1.ApiKey;
using Grpc.Net.Client.Configuration;
using Grpc.Core;

namespace Cerbos.Sdk.Cloud.V1
{
    public sealed class HubClientBuilder
    {
        private string Target { get; }
        private Credentials Credentials { get; set; }

        private HubClientBuilder(string target)
        {
            Target = target;
        }

        public static HubClientBuilder ForTarget(string target)
        {
            return new HubClientBuilder(target);
        }

        public HubClientBuilder WithCredentials(string clientId, string clientSecret)
        {
            Credentials = new Credentials(clientId, clientSecret);
            return this;
        }

        public IHubClient Build()
        {
            if (string.IsNullOrEmpty(Target))
            {
                throw new Exception("Target must be specified");
            }

            if (Credentials == null)
            {
                throw new Exception("Credentials must be specified");
            }

            var channelOptions = new GrpcChannelOptions
            {
                ServiceConfig = new ServiceConfig
                {
                    MethodConfigs = {
                        new MethodConfig {
                            Names = {
                                new MethodName() {
                                    Service = "cerbos.cloud.store.v1.CerbosStoreService",
                                    Method = "ModifyFiles"
                                },
                                new MethodName() {
                                    Service = "cerbos.cloud.store.v1.CerbosStoreService",
                                    Method = "ReplaceFiles"
                                }
                            }
                        },
                        new MethodConfig {
                            Names = {
                                MethodName.Default
                            },
                            RetryPolicy = new RetryPolicy
                            {
                                RetryableStatusCodes = {
                                    StatusCode.Unavailable
                                },
                                MaxAttempts = 3,
                                InitialBackoff = TimeSpan.FromSeconds(0.2),
                                MaxBackoff = TimeSpan.FromSeconds(5),
                                BackoffMultiplier = 2
                            }
                        },
                    },
                    RetryThrottling = new RetryThrottlingPolicy
                    {
                        MaxTokens = 10,
                        TokenRatio = 0.5
                    }
                }
            };
            var authInterceptor = new AuthInterceptor(
                new ApiKeyClient(
                    new ApiKeyService.ApiKeyServiceClient(
                        GrpcChannel.ForAddress(
                            Target,
                            channelOptions
                        )
                    )
                ),
                Credentials
            );

            var channelWithInterceptor = GrpcChannel.ForAddress(Target, channelOptions).Intercept(authInterceptor);
            return new HubClient(
                new StoreClient(new CerbosStoreService.CerbosStoreServiceClient(channelWithInterceptor))
            );
        }
    }
}