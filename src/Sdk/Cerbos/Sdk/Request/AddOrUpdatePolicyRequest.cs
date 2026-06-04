// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.IO;

namespace Cerbos.Sdk.Request
{
    public sealed class AddOrUpdatePolicyRequest
    {
        private readonly Api.V1.Request.AddOrUpdatePolicyRequest R;

        private AddOrUpdatePolicyRequest()
        {
            R = new Api.V1.Request.AddOrUpdatePolicyRequest();
        }

        public static AddOrUpdatePolicyRequest NewInstance()
        {
            return new AddOrUpdatePolicyRequest();
        }

        public AddOrUpdatePolicyRequest With(params Policy.Policy[] policy)
        {
            foreach (var p in policy)
            {
                R.Policies.Add(p.ToPolicy());
            }
            return this;
        }

        public AddOrUpdatePolicyRequest WithJson(TextReader policy)
        {
            return WithJson(policy.ReadToEnd());
        }

        public AddOrUpdatePolicyRequest WithJson(string policy)
        {
            return AddPolicy(policy);
        }

        private AddOrUpdatePolicyRequest AddPolicy(string policy)
        {
            R.Policies.Add(Policy.Policy.NewInstance(policy).ToPolicy());
            return this;
        }

        public Api.V1.Request.AddOrUpdatePolicyRequest ToAddOrUpdatePolicyRequest()
        {
            return R;
        }
    }
}
