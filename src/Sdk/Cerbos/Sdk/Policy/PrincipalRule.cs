// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class PrincipalRule
    {
        private Api.V1.Policy.PrincipalRule PR { get; }

        public Api.V1.Policy.PrincipalRule Raw => PR;

        public string Resource => PR.Resource;

        public List<Types.Action> Actions => PR.Actions.Select(X => new Types.Action(X)).ToList();

        public PrincipalRule(Api.V1.Policy.PrincipalRule principalRule)
        {
            PR = principalRule;
        }

        public static class Types
        {
            public sealed class Action
            {
                private Api.V1.Policy.PrincipalRule.Types.Action A { get; }

                public Api.V1.Policy.PrincipalRule.Types.Action Raw => A;

                public string Action_ => A.Action_;

                public Condition Condition => new Condition(A.Condition);

                public Api.V1.Effect.Effect Effect => A.Effect;

                public string Name => A.Name;

                public Output Output => new Output(A.Output);

                public Action(Api.V1.Policy.PrincipalRule.Types.Action action)
                {
                    A = action;
                }
            }
        }
    }
}
