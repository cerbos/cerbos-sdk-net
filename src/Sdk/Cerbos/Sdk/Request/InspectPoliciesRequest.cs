// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Request
{
    public sealed class InspectPoliciesRequest
    {
        private bool IncludeDisabled { get; set; }
        private string NameRegexp { get; set; }
        private string ScopeRegexp { get; set; }
        private string VersionRegexp { get; set; }
        private List<string> PolicyId { get; }

        private InspectPoliciesRequest(params string[] policyId)
        {
            PolicyId = new List<string>(policyId);
        }

        public static InspectPoliciesRequest NewInstance(params string[] policyId)
        {
            return new InspectPoliciesRequest(policyId);
        }

        public InspectPoliciesRequest WithIncludeDisabled(bool includeDisabled)
        {
            IncludeDisabled = includeDisabled;
            return this;
        }

        public InspectPoliciesRequest WithNameRegexp(string nameRegexp)
        {
            NameRegexp = nameRegexp;
            return this;
        }

        public InspectPoliciesRequest WithScopeRegexp(string scopeRegexp)
        {
            ScopeRegexp = scopeRegexp;
            return this;
        }

        public InspectPoliciesRequest WithVersionRegexp(string versionRegexp)
        {
            VersionRegexp = versionRegexp;
            return this;
        }


        public Api.V1.Request.InspectPoliciesRequest ToInspectPoliciesRequest()
        {
            var request = new Api.V1.Request.InspectPoliciesRequest();
            if (IncludeDisabled)
            {
                request.IncludeDisabled = IncludeDisabled;
            }

            if (!string.IsNullOrEmpty(NameRegexp))
            {
                request.NameRegexp = NameRegexp;
            }

            if (!string.IsNullOrEmpty(ScopeRegexp))
            {
                request.ScopeRegexp = ScopeRegexp;
            }

            if (!string.IsNullOrEmpty(VersionRegexp))
            {
                request.VersionRegexp = VersionRegexp;
            }

            if (PolicyId.Count > 0)
            {
                request.PolicyId.AddRange(PolicyId);
            }

            return request;
        }
    }
}