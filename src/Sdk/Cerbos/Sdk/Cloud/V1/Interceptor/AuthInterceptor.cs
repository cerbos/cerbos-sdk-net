// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading;
using Cerbos.Sdk.Cloud.V1.ApiKey;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Cerbos.Sdk.Cloud.V1.Interceptor
{
    public sealed class AuthInterceptor : Grpc.Core.Interceptors.Interceptor
    {
        private const string AuthTokenHeader = "x-cerbos-auth";
        private const double EarlyExpireInMinutes = 5;
        private Credentials Credentials { get; }
        private IApiKeyClient ApiKeyClient { get; }

        private string AccessToken { get; set; }
        private DateTime AccessTokenExpiresAt { get; set; }
        private bool Unauthenticated { get; set; }
        private ReaderWriterLock RWLock;

        public AuthInterceptor(IApiKeyClient apiKeyClient, Credentials credentials)
        {
            ApiKeyClient = apiKeyClient;
            Credentials = credentials;
            RWLock = new ReaderWriterLock();
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            BlockingUnaryCallContinuation<TRequest, TResponse> continuation
        )
        {
            ThrowIfUnauthenticated();

            if (Expired())
            {
                IssueToken();
            }

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
            ThrowIfUnauthenticated();

            if (Expired())
            {
                IssueToken();
            }

            var newContext = new ClientInterceptorContext<TRequest, TResponse>(
                context.Method,
                context.Host,
                context.Options.WithHeaders(MetadataWithAuthToken(context.Options.Headers))
            );
            return continuation(request, newContext);
        }

        private void IssueToken()
        {
            RWLock.AcquireWriterLock(0);

            try
            {
                if (!Expired())
                {
                    return;
                }

                var response = ApiKeyClient.IssueAccessToken(Credentials.ToIssueAccessTokenRequest());
                AccessToken = response.AccessToken;
                AccessTokenExpiresAt = response.ExpiresAt;
                Unauthenticated = false;
            }
            catch (RpcException e)
            {
                if (e.StatusCode == StatusCode.Unauthenticated)
                {
                    Unauthenticated = true;
                }

                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to issue access token: ${e}");
            }
            finally
            {
                RWLock.ReleaseWriterLock();
            }
        }

        private void ThrowIfUnauthenticated()
        {
            RWLock.AcquireReaderLock(0);
            try
            {
                if (Unauthenticated)
                {
                    throw new RpcException(new Status(StatusCode.Unauthenticated, "Given credentials results in unauthenticated response from server"));
                }
            }
            finally
            {
                RWLock.ReleaseReaderLock();
            }
        }

        private bool Expired()
        {
            RWLock.AcquireReaderLock(0);
            try
            {
                if (AccessTokenExpiresAt.CompareTo(DateTime.MinValue) == 0)
                {
                    return true;
                }

                var earlyExpiresAt = AccessTokenExpiresAt.Subtract(TimeSpan.FromMinutes(EarlyExpireInMinutes));
                return DateTime.UtcNow.CompareTo(earlyExpiresAt) > 0;
            }
            finally
            {
                RWLock.ReleaseReaderLock();
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