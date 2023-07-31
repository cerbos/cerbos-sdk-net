// Copyright 2021-2023 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Cerbos.Sdk.Builder
{
    public sealed class PlanResourcesRequest
    {
        private AuxData AuxData { get; set; }
        private string Action { get; set; } = "";
        private bool IncludeMeta { get; set; }
        private Principal Principal { get; set; }
        private Resource Resource { get; set; }
        private string RequestId { get; set; } = "";

        private PlanResourcesRequest() { }
        
        public static PlanResourcesRequest NewInstance()
        {
            return new PlanResourcesRequest();
        }
        
        public PlanResourcesRequest WithAction(string action)
        {
            Action = action;
            return this;
        }
        public PlanResourcesRequest WithAuxData(AuxData auxData)
        {
            AuxData = auxData;
            return this;
        }

        public PlanResourcesRequest WithIncludeMeta(bool includeMeta)
        {
            IncludeMeta = includeMeta;
            return this;
        }
        
        public PlanResourcesRequest WithPrincipal(Principal principal)
        {
            Principal = principal;
            return this;
        }
        
        public PlanResourcesRequest WithResource(Resource resource)
        {
            Resource = resource;
            return this;
        }
        
        public PlanResourcesRequest WithRequestId(string requestId)
        {
            RequestId = requestId;
            return this;
        }

        public Api.V1.Request.PlanResourcesRequest ToPlanResourcesRequest()
        {
            if (string.IsNullOrEmpty(Action))
            {
                throw new Exception("Action is not set");
            }
            
            if (Principal == null)
            {
                throw new Exception("Principal is not set");
            }
            
            if (Resource == null)
            {
                throw new Exception("Resource is not set");
            }
            
            if (string.IsNullOrEmpty(RequestId))
            {
                RequestId = Utility.RequestId.Generate();
            }
            
            var request = new Api.V1.Request.PlanResourcesRequest
            {
                Action = Action,
                AuxData = AuxData?.ToAuxData(),
                IncludeMeta = IncludeMeta,
                Principal = Principal.ToPrincipal(),
                Resource = Resource.ToPlanResource(),
                RequestId = RequestId,
            };

            return request;
        }
    }
}