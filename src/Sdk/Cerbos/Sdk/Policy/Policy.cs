// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Google.Protobuf;

namespace Cerbos.Sdk.Policy
{
    public sealed class Policy
    {
        private Api.V1.Policy.Policy P { get; }

        public Api.V1.Policy.Policy Raw => P;

        public Policy(Api.V1.Policy.Policy policy)
        {
            P = policy;
        }

        public static Policy NewInstance(string policy)
        {
            return new Policy(JsonParser.Default.Parse<Api.V1.Policy.Policy>(policy));
        }

        public Api.V1.Policy.Policy ToPolicy()
        {
            return Raw;
        }
    }
}