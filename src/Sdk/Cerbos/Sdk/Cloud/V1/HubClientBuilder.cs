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
        private const string DefaultTarget = "https://api.cerbos.cloud";

        private HubClientBuilder(string target, string clientId, string clientSecret)
        {
            Target = target;
            Credentials = new Credentials(clientId, clientSecret);
        }

        public static HubClientBuilder FromEnv()
        {
            var target = EnvOrDefault("CERBOS_HUB_API_ENDPOINT", DefaultTarget);
            var clientId = EnvOrDefault("CERBOS_HUB_CLIENT_ID", "");
            var clientSecret = EnvOrDefault("CERBOS_HUB_CLIENT_SECRET", "");
            if (string.IsNullOrEmpty(clientId) && string.IsNullOrEmpty(clientSecret))
            {
                throw new Exception("CERBOS_HUB_CLIENT_ID and CERBOS_HUB_CLIENT_SECRET environment variables must be specified");
            }

            return new HubClientBuilder(target, clientId, clientSecret);
        }

        public static HubClientBuilder FromCredentials(string clientId, string clientSecret)
        {
            var target = EnvOrDefault("CERBOS_HUB_API_ENDPOINT", DefaultTarget);
            if (string.IsNullOrEmpty(clientId) && string.IsNullOrEmpty(clientSecret))
            {
                throw new Exception("ClientId and clientSecret must be specified");
            }

            return new HubClientBuilder(target, clientId, clientSecret);
        }

        private static string EnvOrDefault(string environment, string defaultValue)
        {
            var value = Environment.GetEnvironmentVariable(environment);
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }

            return defaultValue;
        }

        public IHubClient Build()
        {
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