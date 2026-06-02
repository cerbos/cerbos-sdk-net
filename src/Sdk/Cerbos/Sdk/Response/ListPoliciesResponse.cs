// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Response
{
    public sealed class ListPoliciesResponse
    {
        private Api.V1.Response.ListPoliciesResponse R { get; }

        public List<string> PolicyIds => R.PolicyIds.ToList();
        public Api.V1.Response.ListPoliciesResponse Raw => R;

        public ListPoliciesResponse(Api.V1.Response.ListPoliciesResponse response)
        {
            R = response;
        }
    }
}