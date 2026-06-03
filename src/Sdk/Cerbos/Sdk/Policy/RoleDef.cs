// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class RoleDef
    {
        private Api.V1.Policy.RoleDef RD { get; }

        public Api.V1.Policy.RoleDef Raw => RD;

        public string Name => RD.Name;

        public List<string> ParentRoles => RD.ParentRoles.ToList();

        public Condition Condition => new Condition(RD.Condition);

        public RoleDef(Api.V1.Policy.RoleDef roleDef)
        {
            RD = roleDef;
        }
    }
}