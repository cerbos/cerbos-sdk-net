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
        private const double EarlyExpireInMinutes = 5;
        private RpcException Unauthenticated { get; set; }
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
            if (Unauthenticated != null)
            {
                throw Unauthenticated;
            }

            IssueAccessTokenIfExpired();
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
            if (Unauthenticated != null)
            {
                throw Unauthenticated;
            }

            IssueAccessTokenIfExpired();
            var newContext = new ClientInterceptorContext<TRequest, TResponse>(
                context.Method,
                context.Host,
                context.Options.WithHeaders(MetadataWithAuthToken(context.Options.Headers))
            );
            return continuation(request, newContext);
        }

        private void IssueAccessTokenIfExpired()
        {
            if (!TokenExpired(AccessTokenExpiresAt))
            {
                return;
            }

            try
            {
                var response = ApiKeyClient.IssueAccessToken(Credentials.ToIssueAccessTokenRequest());
                AccessToken = response.AccessToken;
                AccessTokenExpiresAt = response.ExpiresAt;
            }
            catch (RpcException e)
            {
                if (e.StatusCode == StatusCode.Unauthenticated)
                {
                    // There is no point retrying because the given credentials are not valid
                    Unauthenticated = e;
                }

                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to issue access token: ${e}");
            }
        }

        private bool TokenExpired(DateTime expiresAt) {
            if (expiresAt.CompareTo(DateTime.MinValue) == 0)
            {
                return true;
            }

            var earlyExpiresAt = expiresAt.Subtract(TimeSpan.FromMinutes(EarlyExpireInMinutes));
            return DateTime.UtcNow.CompareTo(earlyExpiresAt) > 0;
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