// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class ResourcePolicy
    {
        private Api.V1.Policy.ResourcePolicy RP { get; }

        public Api.V1.Policy.ResourcePolicy Raw => RP;

        public string Resource => RP.Resource;

        public string Version => RP.Version;

        public List<string> ImportDerivedRoles => RP.ImportDerivedRoles.ToList();

        public List<ResourceRule> Rules => RP.Rules.Select(X => new ResourceRule(X)).ToList();

        public string Scope => RP.Scope;

        public Schemas Schemas => new Schemas(RP.Schemas);

        public Variables Variables => new Variables(RP.Variables);

        public Api.V1.Policy.ScopePermissions ScopePermissions => RP.ScopePermissions;

        public Constants Constants => new Constants(RP.Constants);

        public ResourcePolicy(Api.V1.Policy.ResourcePolicy resourcePolicy)
        {
            RP = resourcePolicy;
        }
    }
}