using System.Collections.Generic;

namespace Cerbos.Sdk.Builders
{
    public class Principal
    {
        private Cerbos.Api.V1.Engine.Principal P { get; }
            
        private Principal(string id, string []roles) {
            P = new Cerbos.Api.V1.Engine.Principal
            {
                Id = id,
                Roles = { roles }
            };
        }

        public static Principal NewInstance(string id, string []roles) {
            return new Principal(id, roles);
        }

        public Principal WithPolicyVersion(string version) {
            P.PolicyVersion = version;
            return this;
        }

        public Principal WithRoles(string []roles)
        {
            P.Roles.Add(roles);
            return this;
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

        public Cerbos.Api.V1.Engine.Principal ToPrincipal() {
            return P;
        }
    }
}