// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Builder
{
    public sealed class ResourceEntry
    {
        private Api.V1.Engine.Resource R { get; }
        private List<string> A { get; }

        private ResourceEntry(string kind, string id)
        {
            R = new Api.V1.Engine.Resource
            {
                Id = id,
                Kind = kind
            };
            A = new List<string>();
        }

        private ResourceEntry(Api.V1.Engine.Resource resource, params string[] actions)
        {
            R = resource;
            A = actions.ToList();
        }

        public static ResourceEntry NewInstance(string kind, string id)
        {
            return new ResourceEntry(kind, id);
        }

        public static ResourceEntry NewInstance(Resource resource, params string[] actions)
        {
            return new ResourceEntry(resource.ToResource(), actions);
        }

        public ResourceEntry WithActions(params string[] actions)
        {
            A.AddRange(actions);
            return this;
        }

        public ResourceEntry WithAttribute(string key, AttributeValue value)
        {
            R.Attr.Add(key, value.ToValue());
            return this;
        }

        public ResourceEntry WithAttributes(Dictionary<string, AttributeValue> attributes)
        {
            foreach (KeyValuePair<string, AttributeValue> attribute in attributes)
            {
                R.Attr.Add(attribute.Key, attribute.Value.ToValue());
            }
            return this;
        }
        public ResourceEntry WithId(string id)
        {
            R.Id = id;
            return this;
        }

        public ResourceEntry WithKind(string kind)
        {
            R.Kind = kind;
            return this;
        }

        public ResourceEntry WithPolicyVersion(string policyVersion)
        {
            R.PolicyVersion = policyVersion;
            return this;
        }

        public ResourceEntry WithScope(string scope)
        {
            R.Scope = scope;
            return this;
        }

        public Api.V1.Request.CheckResourcesRequest.Types.ResourceEntry ToResourceEntry()
        {
            return new Api.V1.Request.CheckResourcesRequest.Types.ResourceEntry
            {
                Actions = { A },
                Resource = R
            };
        }
    }
}