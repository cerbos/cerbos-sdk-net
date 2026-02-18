// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.Collections;

namespace Cerbos.Sdk.Builder
{
    public sealed class CheckResourcesRequest
    {
        private AuxData AuxData { get; set; }
        private bool AllowPartialRequests { get; set; }
        private bool IncludeMeta { get; set; }
        private Principal Principal { get; set; }
        private List<ResourceEntry> ResourceEntries { get; }
        private string RequestId { get; set; }
        private RequestContext RequestContext { get; set; }

        private CheckResourcesRequest()
        {
            ResourceEntries = new List<ResourceEntry>();
        }

        public static CheckResourcesRequest NewInstance()
        {
            return new CheckResourcesRequest();
        }

        public CheckResourcesRequest WithAuxData(AuxData auxData)
        {
            AuxData = auxData;
            return this;
        }

        public CheckResourcesRequest WithAllowPartialRequests(bool allowPartialRequests)
        {
            AllowPartialRequests = allowPartialRequests;
            return this;
        }

        public CheckResourcesRequest WithIncludeMeta(bool includeMeta)
        {
            IncludeMeta = includeMeta;
            return this;
        }

        public CheckResourcesRequest WithPrincipal(Principal principal)
        {
            Principal = principal;
            return this;
        }

        public CheckResourcesRequest WithResourceEntries(params ResourceEntry[] resourceEntries)
        {
            ResourceEntries.AddRange(resourceEntries);
            return this;
        }

        public CheckResourcesRequest WithRequestId(string requestId)
        {
            RequestId = requestId;
            return this;
        }

        public CheckResourcesRequest WithRequestContext(RequestContext requestContext)
        {
            RequestContext = requestContext;
            return this;
        }

        public Api.V1.Request.CheckResourcesRequest ToCheckResourcesRequest()
        {
            var request = new Api.V1.Request.CheckResourcesRequest
            {
                AuxData = AuxData?.ToAuxData(),
                IncludeMeta = IncludeMeta,
                RequestId = string.IsNullOrEmpty(RequestId) ? Utility.RequestId.Generate() : RequestId,
                RequestContext = RequestContext?.ToRequestContext()
            };

            if (Principal != null)
            {
                request.Principal = Principal.ToPrincipal();
            }
            else if (!AllowPartialRequests)
            {
                throw new Exception("Principal is not set");
            }

            if (ResourceEntries.Count > 0)
            {
                foreach (var re in ResourceEntries)
                {
                    request.Resources.Add(re.ToResourceEntry());
                }
            }
            else if (!AllowPartialRequests)
            {
                throw new Exception("ResourceEntries are not set");
            }

            return request;
        }
    }
}