// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Builder
{
    public sealed class RequestContext
    {
        private Cerbos.Api.V1.Audit.RequestContext R { get; }

        private RequestContext()
        {
            R = new Cerbos.Api.V1.Audit.RequestContext();
        }

        public static RequestContext NewInstance()
        {
            return new RequestContext();
        }

        public static RequestContext WithAnnotations(Dictionary<string, AttributeValue> annotations)
        {
            RequestContext requestContext = new RequestContext();
            foreach (KeyValuePair<string, AttributeValue> annotation in annotations)
            {
                requestContext.R.Annotations.Add(annotation.Key, annotation.Value.ToValue());
            }

            return requestContext;
        }

        public RequestContext WithAnnotation(string key, AttributeValue value)
        {
            R.Annotations.Add(key, value.ToValue());
            return this;
        }

        public Cerbos.Api.V1.Audit.RequestContext ToRequestContext()
        {
            return R;
        }
    }
}