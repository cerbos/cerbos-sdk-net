// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class ResourceRule
    {
        private Api.V1.Policy.ResourceRule RR { get; }

        public Api.V1.Policy.ResourceRule Raw => RR;

        public List<string> Actions => RR.Actions.ToList();

        public List<string> DerivedRoles => RR.DerivedRoles.ToList();

        public List<string> Roles => RR.Roles.ToList();

        public Condition Condition => new Condition(RR.Condition);

        public Api.V1.Effect.Effect Effect => RR.Effect;

        public string Name => RR.Name;

        public Output Output => new Output(RR.Output);

        public ResourceRule(Api.V1.Policy.ResourceRule resourceRule)
        {
            RR = resourceRule;
        }
    }
}
