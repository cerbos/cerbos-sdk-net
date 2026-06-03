// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Policy
{
    public sealed class Policy
    {
        private Api.V1.Policy.Policy P { get; }

        public Policy(Api.V1.Policy.Policy policy)
        {
            P = policy;
        }

        private Policy()
        {
            P = new Api.V1.Policy.Policy();
        }

        public Api.V1.Policy.Policy ToPolicy()
        {
            return P;
        }
    }
}