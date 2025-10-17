// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Cerbos.Sdk.Cloud.V1.ApiKey
{
    internal sealed class IssueAccessTokenRequest
    {
        private string ClientId { get; set; }
        private string ClientSecret { get; set; }

        private IssueAccessTokenRequest()
        {
        }

        public static IssueAccessTokenRequest NewInstance()
        {
            return new IssueAccessTokenRequest();
        }

        public IssueAccessTokenRequest WithClientId(string clientId)
        {
            ClientId = clientId;
            return this;
        }

        public IssueAccessTokenRequest WithClientSecret(string clientSecret)
        {
            ClientSecret = clientSecret;
            return this;
        }

        public Api.Cloud.V1.ApiKey.IssueAccessTokenRequest ToIssueAccessTokenRequest()
        {
            if (ClientId == null)
            {
                throw new Exception("Client ID must be specified");
            }

            if (ClientSecret == null)
            {
                throw new Exception("Client secret must be specified");
            }

            return new Api.Cloud.V1.ApiKey.IssueAccessTokenRequest
            {
                ClientId = ClientId,
                ClientSecret = ClientSecret
            };
        }
    }
}