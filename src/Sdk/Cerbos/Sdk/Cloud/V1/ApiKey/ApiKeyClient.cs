// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;
using Cerbos.Api.Cloud.V1.ApiKey;

namespace Cerbos.Sdk.Cloud.V1.ApiKey
{
    internal interface IApiKeyClient
    {
        IssueAccessTokenResponse IssueAccessToken(IssueAccessTokenRequest request);
        Task<IssueAccessTokenResponse> IssueAccessTokenAsync(IssueAccessTokenRequest request);
    }

    /// <summary>
    /// ApiKeyClient provides a client implementation that communicates with the Cerbos Hub API Key Service.
    /// </summary>
    /// 
    internal sealed class ApiKeyClient : IApiKeyClient
    {
        private ApiKeyService.ApiKeyServiceClient Client { get; }

        internal ApiKeyClient(ApiKeyService.ApiKeyServiceClient apiKeyServiceClient)
        {
            Client = apiKeyServiceClient;
        }

        public IssueAccessTokenResponse IssueAccessToken(IssueAccessTokenRequest request)
        {
            try
            {
                return Resilience.Pipeline.Execute(() =>
                {
                    return new IssueAccessTokenResponse(
                        Client.IssueAccessToken(
                            request.ToIssueAccessTokenRequest()
                        )
                    );
                });
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to issue access token: ${e}");
            }
        }

        public Task<IssueAccessTokenResponse> IssueAccessTokenAsync(IssueAccessTokenRequest request)
        {
            try
            {
                return Resilience.Pipeline.ExecuteAsync(
                    async (token) =>
                    {
                        return await Client.IssueAccessTokenAsync(request.ToIssueAccessTokenRequest()).ResponseAsync.ContinueWith(r => new IssueAccessTokenResponse(r.Result));
                    }
                ).AsTask();
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to issue access token: ${e}");
            }
        }
    }
}
