using System.Collections.Generic;

namespace Cerbos.Sdk.Builders
{
    public class Resource
    {
        private Cerbos.Api.V1.Engine.Resource R { get; }
            
        private Resource(string kind, string id) {
            R = new Cerbos.Api.V1.Engine.Resource
            {
                Id = id,
                Kind = kind
            };
        }

        public static Resource NewInstance(string kind) {
            return new Resource(kind, "_NEW_");
        }
        
        public static Resource NewInstance(string kind, string id) {
            return new Resource(kind, id);
        }

        public Resource WithPolicyVersion(string version) {
            R.PolicyVersion = version;
            return this;
        }
        
        public Resource WithAttribute(string key, AttributeValue value) {
            R.Attr.Add(key, value.ToValue());
            return this;
        }
        
        public Resource WithAttributes(Dictionary<string, AttributeValue> attributes) {
            foreach (KeyValuePair<string, AttributeValue> attribute in attributes)
            {
                R.Attr.Add(attribute.Key, attribute.Value.ToValue());
            }
            return this;
        }

        public Cerbos.Api.V1.Engine.Resource ToResource() {
            return R;
        }
    }
}