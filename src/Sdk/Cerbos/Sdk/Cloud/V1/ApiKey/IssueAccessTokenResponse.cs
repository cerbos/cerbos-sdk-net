// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Cerbos.Sdk.Cloud.V1.ApiKey
{
    internal sealed class IssueAccessTokenResponse
    {
        private Api.Cloud.V1.ApiKey.IssueAccessTokenResponse R { get; }

        public string AccessToken => R.AccessToken;
        public DateTime ExpiresAt { get; }
        public Api.Cloud.V1.ApiKey.IssueAccessTokenResponse Raw => R;

        public IssueAccessTokenResponse(Api.Cloud.V1.ApiKey.IssueAccessTokenResponse response)
        {
            R = response;
            ExpiresAt = DateTime.UtcNow.Add(response.ExpiresIn.ToTimeSpan());
        }
    }
}
