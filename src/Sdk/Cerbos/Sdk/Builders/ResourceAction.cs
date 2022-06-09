using System.Collections.Generic;
using System.Linq;
using Cerbos.Api.V1.Request;

namespace Cerbos.Sdk.Builders
{
    public class ResourceAction
    {
        private Cerbos.Api.V1.Engine.Resource R { get; }
        private List<string> A { get; }

        private ResourceAction(string kind, string id) {
            R = new Cerbos.Api.V1.Engine.Resource
            {
                Id = id,
                Kind = kind
            };
            A = new List<string>();
        }

        private ResourceAction(Cerbos.Api.V1.Engine.Resource resource, params string[] actions)
        {
            R = resource;
            A = actions.ToList();
        }
        
        public static ResourceAction NewInstance(string kind, string id) {
            return new ResourceAction(kind, id);
        }
        
        public static ResourceAction NewInstance(Resource resource, params string[] actions) {
            return new ResourceAction(resource.ToResource(), actions);
        }
        
        public ResourceAction WithPolicyVersion(string version) {
            R.PolicyVersion = version;
            return this;
        }
        
        public ResourceAction WithAttribute(string key, AttributeValue value) {
            R.Attr.Add(key, value.ToValue());
            return this;
        }
        
        public ResourceAction WithAttributes(Dictionary<string, AttributeValue> attributes) {
            foreach (KeyValuePair<string, AttributeValue> attribute in attributes)
            {
                R.Attr.Add(attribute.Key, attribute.Value.ToValue());
            }
            return this;
        }
        
        public ResourceAction WithActions(params string[] actions) {
            A.AddRange(actions);
            return this;
        }

        public CheckResourcesRequest.Types.ResourceEntry ToResourceEntry()
        {
            return new CheckResourcesRequest.Types.ResourceEntry
            {
                Resource = R,
                Actions = { A }
            };
        }
    }
}