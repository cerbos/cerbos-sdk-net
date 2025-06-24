// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Cloud.V1.ApiKey;

namespace Cerbos.Sdk.Cloud.V1
{
    internal sealed class Credentials
    {
        public string ClientId { get; }
        public string ClientSecret { get; }

        public Credentials(string clientId, string clientSecret)
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
