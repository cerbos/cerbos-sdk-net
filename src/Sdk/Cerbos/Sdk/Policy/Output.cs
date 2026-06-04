// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Policy
{
    public sealed class Output
    {
        private Api.V1.Policy.Output O { get; }

        public Api.V1.Policy.Output Raw => O;

        public Types.When When => new Types.When(O.When);

        public Output(Api.V1.Policy.Output output)
        {
            O = output;
        }

        public static class Types
        {
            public sealed class When
            {
                private Api.V1.Policy.Output.Types.When W { get; }

                public Api.V1.Policy.Output.Types.When Raw => W;

                public string RuleActivated => W.RuleActivated;

                public string ConditionNotMet => W.ConditionNotMet;

                public When(Api.V1.Policy.Output.Types.When when)
                {
                    W = when;
                }
            }
        }
    }
}