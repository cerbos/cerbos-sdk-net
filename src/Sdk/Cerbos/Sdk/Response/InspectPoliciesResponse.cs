// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Response
{
    public sealed class InspectPoliciesResponse
    {
        private Api.V1.Response.InspectPoliciesResponse R { get; }

        public Api.V1.Response.InspectPoliciesResponse Raw => R;

        public Dictionary<string, Types.Result> Results => R.Results.ToDictionary(x => x.Key, x => new Types.Result(x.Value));

        public InspectPoliciesResponse(Api.V1.Response.InspectPoliciesResponse response)
        {
            R = response;
        }

        public static class Types
        {
            public sealed class Attribute
            {
                private Api.V1.Response.InspectPoliciesResponse.Types.Attribute A { get; }

                public Api.V1.Response.InspectPoliciesResponse.Types.Attribute Raw => A;

                public Api.V1.Response.InspectPoliciesResponse.Types.Attribute.Types.Kind Kind => A.Kind;

                public string Name => A.Name;

                public Attribute(Api.V1.Response.InspectPoliciesResponse.Types.Attribute attribute)
                {
                    A = attribute;
                }
            }

            public sealed class DerivedRole
            {
                private Api.V1.Response.InspectPoliciesResponse.Types.DerivedRole DR { get; }

                public Api.V1.Response.InspectPoliciesResponse.Types.DerivedRole Raw => DR;

                public string Name => DR.Name;

                public Api.V1.Response.InspectPoliciesResponse.Types.DerivedRole.Types.Kind Kind => DR.Kind;

                public string Source => DR.Source;

                public DerivedRole(Api.V1.Response.InspectPoliciesResponse.Types.DerivedRole derivedRole)
                {
                    DR = derivedRole;
                }
            }

            public sealed class Constant
            {
                private Api.V1.Response.InspectPoliciesResponse.Types.Constant C { get; }

                public Api.V1.Response.InspectPoliciesResponse.Types.Constant Raw => C;

                public string Name => C.Name;

                public Api.V1.Response.InspectPoliciesResponse.Types.Constant.Types.Kind Kind => C.Kind;

                public string Source => C.Source;

                public bool Used => C.Used;

                public Constant(Api.V1.Response.InspectPoliciesResponse.Types.Constant constant)
                {
                    C = constant;
                }
            }

            public sealed class Result
            {
                private Api.V1.Response.InspectPoliciesResponse.Types.Result R { get; }

                public Api.V1.Response.InspectPoliciesResponse.Types.Result Raw => R;

                public List<string> Actions => R.Actions.ToList();

                public List<Variable> Variables => R.Variables.Select(X => new Variable(X)).ToList();

                public string PolicyId => R.PolicyId;

                public List<DerivedRole> DerivedRoles => R.DerivedRoles.Select(X => new DerivedRole(X)).ToList();

                public List<Attribute> Attributes => R.Attributes.Select(X => new Attribute(X)).ToList();

                public List<Constant> Constants => R.Constants.Select(X => new Constant(X)).ToList();

                public Result(Api.V1.Response.InspectPoliciesResponse.Types.Result result)
                {
                    R = result;
                }
            }

            public sealed class Variable
            {
                private Api.V1.Response.InspectPoliciesResponse.Types.Variable V { get; }

                public Api.V1.Response.InspectPoliciesResponse.Types.Variable Raw => V;

                public string Name => V.Name;

                public string Value => V.Value;

                public Api.V1.Response.InspectPoliciesResponse.Types.Variable.Types.Kind Kind => V.Kind;

                public string Source => V.Source;

                public bool Used => V.Used;

                public Variable(Api.V1.Response.InspectPoliciesResponse.Types.Variable variable)
                {
                    V = variable;
                }
            }
        }
    }
}