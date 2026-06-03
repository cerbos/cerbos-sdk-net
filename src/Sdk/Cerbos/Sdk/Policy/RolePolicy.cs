// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class RolePolicy
    {
        private Api.V1.Policy.RolePolicy RP { get; }

        public Api.V1.Policy.RolePolicy Raw => RP;

        public Api.V1.Policy.RolePolicy.PolicyTypeOneofCase OneOf => RP.PolicyTypeCase;

        public string Role => OneOf == Api.V1.Policy.RolePolicy.PolicyTypeOneofCase.Role ? RP.Role : "";

        public string Version => RP.Version;

        public List<string> ParentRoles => RP.ParentRoles.ToList();

        public string Scope => RP.Scope;

        public List<RoleRule> Rules => RP.Rules.Select(X => new RoleRule(X)).ToList();

        public Variables Variables => new Variables(RP.Variables);

        public Constants Constants => new Constants(RP.Constants);

        public RolePolicy(Api.V1.Policy.RolePolicy rolePolicy)
        {
            RP = rolePolicy;
        }
    }
}