// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Builder
{
    public sealed class Resource
    {
        private Dictionary<string, Google.Protobuf.WellKnownTypes.Value> Attributes { get; } =
            new Dictionary<string, Google.Protobuf.WellKnownTypes.Value>();
        private string Id { get; set; }
        private string Kind { get; set; }
        private string PolicyVersion { get; set; } = "";
        private string Scope { get; set; } = "";

        private Resource(string kind, string id)
        {
            Id = id;
            Kind = kind;
        }

        public static Resource NewInstance(string kind)
        {
            return new Resource(kind, "_NEW_");
        }

        public static Resource NewInstance(string kind, string id)
        {
            return new Resource(kind, id);
        }

        public Resource WithAttribute(string key, AttributeValue value)
        {
            Attributes.Add(key, value.ToValue());
            return this;
        }

        public Resource WithAttributes(Dictionary<string, AttributeValue> attributes)
        {
            foreach (KeyValuePair<string, AttributeValue> attribute in attributes)
            {
                Attributes.Add(attribute.Key, attribute.Value.ToValue());
            }
            return this;
        }

        public Resource WithId(string id)
        {
            Id = id;
            return this;
        }

        public Resource WithKind(string kind)
        {
            Kind = kind;
            return this;
        }

        public Resource WithPolicyVersion(string policyVersion)
        {
            PolicyVersion = policyVersion;
            return this;
        }

        public Resource WithScope(string scope)
        {
            Scope = scope;
            return this;
        }

        public Api.V1.Engine.Resource ToResource()
        {
            var r = new Api.V1.Engine.Resource()
            {
                Attr = { Attributes },
                Kind = Kind,
                Id = Id,
                Scope = Scope,
                PolicyVersion = PolicyVersion,
            };
            return r;
        }

        public Api.V1.Engine.PlanResourcesInput.Types.Resource ToPlanResource()
        {
            Api.V1.Engine.PlanResourcesInput.Types.Resource r = new Api.V1.Engine.PlanResourcesInput.Types.Resource
            {
                Attr = { Attributes },
                Kind = Kind,
                PolicyVersion = PolicyVersion,
                Scope = Scope,
            };
            return r;
        }
    }
}