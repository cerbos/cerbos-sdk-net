// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class Match
    {
        private Api.V1.Policy.Match M { get; }

        public Api.V1.Policy.Match Raw => M;

        public Api.V1.Policy.Match.OpOneofCase OneOf => M.OpCase;

        public Types.ExprList All => OneOf == Api.V1.Policy.Match.OpOneofCase.All ? new Types.ExprList(M.All) : null;

        public Types.ExprList Any => OneOf == Api.V1.Policy.Match.OpOneofCase.Any ? new Types.ExprList(M.Any) : null;

        public Types.ExprList None => OneOf == Api.V1.Policy.Match.OpOneofCase.None ? new Types.ExprList(M.None) : null;

        public string Expr => OneOf == Api.V1.Policy.Match.OpOneofCase.Expr ? M.Expr : "";

        public Match(Api.V1.Policy.Match match)
        {
            M = match;
        }

        public static class Types
        {
            public sealed class ExprList
            {
                private Api.V1.Policy.Match.Types.ExprList EL { get; }

                public Api.V1.Policy.Match.Types.ExprList Raw => EL;

                public List<Match> Of => EL.Of.Select(X => new Match(X)).ToList();

                public ExprList(Api.V1.Policy.Match.Types.ExprList exprList)
                {
                    EL = exprList;
                }
            }
        }
    }
}