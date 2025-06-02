// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using Cerbos.Sdk.Cloud.V1.ApiKey;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Cerbos.Sdk.Cloud.V1.Interceptor
{
    public sealed class AuthInterceptor : Grpc.Core.Interceptors.Interceptor
    {
        private const string AuthTokenHeader = "x-cerbos-auth";
        private Credentials Credentials { get; }
        private IApiKeyClient ApiKeyClient { get; }
        private string AccessToken { get; set; }
        private DateTime AccessTokenExpiresAt { get; set; }

        public AuthInterceptor(IApiKeyClient apiKeyClient, Credentials credentials)
        {
            ApiKeyClient = apiKeyClient;
            Credentials = credentials;
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            BlockingUnaryCallContinuation<TRequest, TResponse> continuation
        )
        {
            IssueAccessToken();
            var newContext = new ClientInterceptorContext<TRequest, TResponse>(
                context.Method,
                context.Host,
                context.Options.WithHeaders(MetadataWithAuthToken(context.Options.Headers))
            );
            return continuation(request, newContext);
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            IssueAccessToken();
            var newContext = new ClientInterceptorContext<TRequest, TResponse>(
                context.Method,
                context.Host,
                context.Options.WithHeaders(MetadataWithAuthToken(context.Options.Headers))
            );
            return continuation(request, newContext);
        }

        private void IssueAccessToken()
        {
            if (DateTime.UtcNow.CompareTo(AccessTokenExpiresAt) < 1)
            {
                return;
            }

            try
            {
                var response = ApiKeyClient.IssueAccessToken(Credentials.ToIssueAccessTokenRequest());
                AccessToken = response.AccessToken;
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
}