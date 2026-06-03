// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using Cerbos.Api.V1.Policy;
using Google.Protobuf;

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

        public AddOrUpdatePolicyRequest With(TextReader policyJson)
        {
            return With(policyJson.ReadToEnd());
        }

        public AddOrUpdatePolicyRequest With(string policyJson)
        {
            return AddPolicy(policyJson);
        }

        public AddOrUpdatePolicyRequest With(params Policy[] policy)
        {
            R.Policies.AddRange(policy);
            return this;
        }

        private AddOrUpdatePolicyRequest AddPolicy(string policy)
        {
            R.Policies.Add(JsonParser.Default.Parse<Policy>(policy));
            return this;
        }

        public Api.V1.Request.AddOrUpdatePolicyRequest ToAddOrUpdatePolicyRequest()
        {
            return R;
        }
    }
}
