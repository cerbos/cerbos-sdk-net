// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Google.Protobuf;

namespace Cerbos.Sdk.Policy
{
    public sealed class Policy
    {
        private Api.V1.Policy.Policy P { get; }

        public Api.V1.Policy.Policy Raw => P;

        public string ApiVersion => P.ApiVersion;

        public string Description => P.Description;

        public bool Disabled => P.Disabled;

        public Api.V1.Policy.Kind Kind
        {
            get
            {
                switch (P.PolicyTypeCase)
                {
                    case Api.V1.Policy.Policy.PolicyTypeOneofCase.DerivedRoles:
                        return Api.V1.Policy.Kind.DerivedRoles;

                    case Api.V1.Policy.Policy.PolicyTypeOneofCase.ExportConstants:
                        return Api.V1.Policy.Kind.ExportConstants;

                    case Api.V1.Policy.Policy.PolicyTypeOneofCase.ExportVariables:
                        return Api.V1.Policy.Kind.ExportVariables;

                    case Api.V1.Policy.Policy.PolicyTypeOneofCase.PrincipalPolicy:
                        return Api.V1.Policy.Kind.Principal;

                    case Api.V1.Policy.Policy.PolicyTypeOneofCase.ResourcePolicy:
                        return Api.V1.Policy.Kind.Resource;

                    case Api.V1.Policy.Policy.PolicyTypeOneofCase.RolePolicy:
                        return Api.V1.Policy.Kind.RolePolicy;

                    default:
                        return Api.V1.Policy.Kind.Unspecified;
                }
            }
        }

        public Api.V1.Policy.Policy.PolicyTypeOneofCase OneOf => P.PolicyTypeCase;

        public DerivedRoles DerivedRoles => OneOf == Api.V1.Policy.Policy.PolicyTypeOneofCase.DerivedRoles ? new DerivedRoles(P.DerivedRoles) : null;

        public ExportConstants ExportConstants => OneOf == Api.V1.Policy.Policy.PolicyTypeOneofCase.ExportConstants ? new ExportConstants(P.ExportConstants) : null;

        public ExportVariables ExportVariables => OneOf == Api.V1.Policy.Policy.PolicyTypeOneofCase.ExportVariables ? new ExportVariables(P.ExportVariables) : null;

        public PrincipalPolicy PrincipalPolicy => OneOf == Api.V1.Policy.Policy.PolicyTypeOneofCase.PrincipalPolicy ? new PrincipalPolicy(P.PrincipalPolicy) : null;

        public ResourcePolicy ResourcePolicy => OneOf == Api.V1.Policy.Policy.PolicyTypeOneofCase.ResourcePolicy ? new ResourcePolicy(P.ResourcePolicy) : null;

        public RolePolicy RolePolicy => OneOf == Api.V1.Policy.Policy.PolicyTypeOneofCase.RolePolicy ? new RolePolicy(P.RolePolicy) : null;

        public Policy(Api.V1.Policy.Policy policy)
        {
            P = policy;
        }

        public static Policy NewInstance(string policy)
        {
            return new Policy(JsonParser.Default.Parse<Api.V1.Policy.Policy>(policy));
        }

        public Api.V1.Policy.Policy ToPolicy()
        {
            return Raw;
        }
    }
}