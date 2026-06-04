// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class DerivedRoles
    {
        private Api.V1.Policy.DerivedRoles DR { get; }

        public Api.V1.Policy.DerivedRoles Raw => DR;

        public string Name => DR.Name;

        public List<RoleDef> Definitions => DR.Definitions.Select(X => new RoleDef(X)).ToList();

        public Variables Variables => new Variables(DR.Variables);

        public Constants Constants => new Constants(DR.Constants);

        public DerivedRoles(Api.V1.Policy.DerivedRoles derivedRoles)
        {
            DR = derivedRoles;
        }
    }
}