// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class PrincipalPolicy
    {
        private Api.V1.Policy.PrincipalPolicy PP { get; }

        public Api.V1.Policy.PrincipalPolicy Raw => PP;

        public string Principal => PP.Principal;

        public string Version => PP.Version;

        public List<PrincipalRule> Rules => PP.Rules.Select(X => new PrincipalRule(X)).ToList();

        public string Scope => PP.Scope;

        public Variables Variables => new Variables(PP.Variables);

        public Api.V1.Policy.ScopePermissions ScopePermissions => PP.ScopePermissions;

        public Constants Constants => new Constants(PP.Constants);

        public PrincipalPolicy(Api.V1.Policy.PrincipalPolicy principalPolicy)
        {
            PP = principalPolicy;
        }
    }
}