// Copyright 2021-2024 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Builder
{
    public sealed class Principal
    {
        private Cerbos.Api.V1.Engine.Principal P { get; }

        private Principal(string id, params string[] roles) {
            P = new Cerbos.Api.V1.Engine.Principal
            {
                Id = id,
                Roles = { roles }
            };
        }

        public static Principal NewInstance(string id, params string[] roles) {
            return new Principal(id, roles);
        }

        public Principal WithAttribute(string key, AttributeValue value) {
            P.Attr.Add(key, value.ToValue());
            return this;
        }

        public Principal WithAttributes(Dictionary<string, AttributeValue> attributes) {
            foreach (KeyValuePair<string, AttributeValue> attribute in attributes)
            {
                P.Attr.Add(attribute.Key, attribute.Value.ToValue());
            }
            return this;
        }

        public Principal WithId(string id)
        {
            P.Id = id;
            return this;
        }

        public Principal WithPolicyVersion(string version) {
            P.PolicyVersion = version;
            return this;
        }

        public Principal WithRoles(params string[] roles)
        {
            P.Roles.AddRange(roles);
            return this;
        }

        public Principal WithScope(string scope)
        {
            P.Scope = scope;
            return this;
        }

        public Cerbos.Api.V1.Engine.Principal ToPrincipal() {
            return P;
        }
    }
}