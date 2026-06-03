// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

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

        public Api.V1.Policy.Policy ToPolicy()
        {
            return Raw;
        }
    }
}