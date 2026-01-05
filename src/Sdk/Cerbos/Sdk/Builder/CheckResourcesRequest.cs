// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Google.Protobuf.Collections;

namespace Cerbos.Sdk.Builder
{
    public sealed class CheckResourcesRequest
    {
        private AuxData AuxData { get; set; }
        private bool IncludeMeta { get; set; }
        private Principal Principal { get; set; }
        private List<ResourceEntry> ResourceEntries { get; }
        private string RequestId { get; set; }

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

        public Api.V1.Request.CheckResourcesRequest ToCheckResourcesRequest()
        {
            if (Principal == null)
            {
                throw new Exception("Principal is not set");
            }

            if (ResourceEntries.Count == 0)
            {
                throw new Exception("ResourceEntries are not set");
            }

            if (string.IsNullOrEmpty(RequestId))
            {
                RequestId = Utility.RequestId.Generate();
            }

            var resourceEntries = new RepeatedField<Api.V1.Request.CheckResourcesRequest.Types.ResourceEntry>();
            foreach (var re in ResourceEntries)
            {
                resourceEntries.Add(re.ToResourceEntry());
            }

            var request = new Api.V1.Request.CheckResourcesRequest
            {
                AuxData = AuxData?.ToAuxData(),
                IncludeMeta = IncludeMeta,
                Principal = Principal.ToPrincipal(),
                Resources = { resourceEntries },
                RequestId = RequestId,
            };

            return request;
        }
    }
}