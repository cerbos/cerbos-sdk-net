// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Response
{
    public sealed class DeletePolicyResponse
    {
        private Api.V1.Response.DeletePolicyResponse R { get; }

        public Api.V1.Response.DeletePolicyResponse Raw => R;

        public uint DeletedPolicies => R.DeletedPolicies;


        public DeletePolicyResponse(Api.V1.Response.DeletePolicyResponse response)
        {
            R = response;
        }
    }
}