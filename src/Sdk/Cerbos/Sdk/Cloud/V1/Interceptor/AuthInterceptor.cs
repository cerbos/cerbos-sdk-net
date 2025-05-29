// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;
using Cerbos.Sdk.Cloud.V1.ApiKey;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Cerbos.Sdk.Cloud.V1.Interceptor
{
    public sealed class AuthInterceptor : Grpc.Core.Interceptors.Interceptor
    {
        private const string AuthTokenHeader = "x-cerbos-auth";
        private HubCredentials HubCredentials { get; }
        private IApiKeyClient ApiKeyClient { get; }
        private string AccessToken { get; set; }
        private DateTime AccessTokenExpiresAt { get; set; }

        public AuthInterceptor(IApiKeyClient apiKeyClient, HubCredentials hubCredentials)
        {
            ApiKeyClient = apiKeyClient;
            HubCredentials = hubCredentials;
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            BlockingUnaryCallContinuation<TRequest, TResponse> continuation
        )
        {
            IssueAccessToken();
            context.Options.WithHeaders(MetadataWithAuthToken(context.Options.Headers));
            return continuation(request, context);
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            IssueAccessToken();
            context.Options.WithHeaders(MetadataWithAuthToken(context.Options.Headers));
            return continuation(request, context);
        }

        private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> t)
        {
            var response = await t;
            return response;
        }

        private void IssueAccessToken()
        {
            if (DateTime.UtcNow.CompareTo(AccessTokenExpiresAt) < 1)
            {
                return;
            }

            try
            {
                var response = ApiKeyClient.IssueAccessToken(HubCredentials.ToIssueAccessTokenRequest());
                AccessToken = AccessToken;
                AccessTokenExpiresAt = response.ExpiresAt;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to issue access token: ${e}");
            }
        }

        private Metadata MetadataWithAuthToken(Metadata metadata)
        {
            return Utility.Metadata.Merge(
                metadata,
                new Metadata {
                    { AuthTokenHeader, AccessToken }
                }
            );
        }
    }

    public sealed class HubCredentials
    {
        public string ClientId { get; }
        public string ClientSecret { get; }

        public HubCredentials(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public IssueAccessTokenRequest ToIssueAccessTokenRequest()
        {
            return IssueAccessTokenRequest.NewInstance().WithClientId(ClientId).WithClientSecret(ClientSecret);
        }
    }
}