// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Policy
{
    public sealed class Condition
    {
        private Api.V1.Policy.Condition C { get; }

        public Api.V1.Policy.Condition Raw => C;

        public Api.V1.Policy.Condition.ConditionOneofCase OneOf => C.ConditionCase;

        public Match Match => OneOf == Api.V1.Policy.Condition.ConditionOneofCase.Match ? new Match(C.Match) : null;

        public string Script => OneOf == Api.V1.Policy.Condition.ConditionOneofCase.Script ? C.Script : "";

        public Condition(Api.V1.Policy.Condition condition)
        {
            C = condition;
        }
    }
}