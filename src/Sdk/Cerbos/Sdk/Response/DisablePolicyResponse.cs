// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Response
{
    public sealed class DisablePolicyResponse
    {
        private Api.V1.Response.DisablePolicyResponse R { get; }

        public uint DisabledPolicies => R.DisabledPolicies;
        public Api.V1.Response.DisablePolicyResponse Raw => R;

        public DisablePolicyResponse(Api.V1.Response.DisablePolicyResponse response)
        {
            R = response;
        }
    }
}