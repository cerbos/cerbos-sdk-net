// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class Schemas
    {
        private Api.V1.Policy.Schemas S { get; }

        public Api.V1.Policy.Schemas Raw => S;

        public Types.Schema PrincipalSchema => new Types.Schema(S.PrincipalSchema);

        public Types.Schema ResourceSchema => new Types.Schema(S.ResourceSchema);

        public Schemas(Api.V1.Policy.Schemas schemas)
        {
            S = schemas;
        }

        public static class Types
        {
            public sealed class IgnoreWhen
            {
                private Api.V1.Policy.Schemas.Types.IgnoreWhen IW { get; }

                public Api.V1.Policy.Schemas.Types.IgnoreWhen Raw => IW;

                public List<string> Actions => IW.Actions.ToList();

                public IgnoreWhen(Api.V1.Policy.Schemas.Types.IgnoreWhen ignoreWhen)
                {
                    IW = ignoreWhen;
                }
            }

            public sealed class Schema
            {
                private Api.V1.Policy.Schemas.Types.Schema S { get; }

                public Api.V1.Policy.Schemas.Types.Schema Raw => S;

                public string Ref => S.Ref;

                public IgnoreWhen IgnoreWhen => new Types.IgnoreWhen(S.IgnoreWhen);

                public Schema(Api.V1.Policy.Schemas.Types.Schema schema)
                {
                    S = schema;
                }
            }
        }
    }
}