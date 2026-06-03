// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Response
{
    public sealed class GetPolicyResponse
    {
        private Api.V1.Response.GetPolicyResponse R { get; }

        public Api.V1.Response.GetPolicyResponse Raw => R;

        public List<Policy.Policy> Policies
        {
            get
            {
                var policies = new List<Policy.Policy>();
                foreach (var policy in R.Policies)
                {
                    policies.Add(new Policy.Policy(policy));
                }

                return policies;
            }
        }

        public GetPolicyResponse(Api.V1.Response.GetPolicyResponse response)
        {
            R = response;
        }
    }
}