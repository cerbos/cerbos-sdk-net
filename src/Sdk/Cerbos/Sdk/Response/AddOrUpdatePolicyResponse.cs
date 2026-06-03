// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Response
{
    public sealed class AddOrUpdatePolicyResponse
    {
        private Api.V1.Response.AddOrUpdatePolicyResponse R { get; }
        public Api.V1.Response.AddOrUpdatePolicyResponse Raw => R;

        public AddOrUpdatePolicyResponse(Api.V1.Response.AddOrUpdatePolicyResponse response)
        {
            R = response;
        }
    }
}