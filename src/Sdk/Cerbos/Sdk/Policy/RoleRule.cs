// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class RoleRule
    {
        private Api.V1.Policy.RoleRule RR { get; }

        public Api.V1.Policy.RoleRule Raw => RR;

        public string Resource => RR.Resource;

        public List<string> AllowActions => RR.AllowActions.ToList();

        public Condition Condition => new Condition(RR.Condition);

        public string Name => RR.Name;

        public Output Output => new Output(RR.Output);

        public RoleRule(Api.V1.Policy.RoleRule roleRule)
        {
            RR = roleRule;
        }
    }
}
