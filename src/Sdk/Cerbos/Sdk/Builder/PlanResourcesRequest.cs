// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace Cerbos.Sdk.Builder
{
    public sealed class PlanResourcesRequest
    {
        private AuxData AuxData { get; set; }
        private string Action { get; set; } = "";
        private List<string> Actions { get; set; }
        private bool IncludeMeta { get; set; }
        private Principal Principal { get; set; }
        private Resource Resource { get; set; }
        private string RequestId { get; set; } = "";

        private PlanResourcesRequest() { 
            Actions = new List<string>();
        }
        
        public static PlanResourcesRequest NewInstance()
        {
            return new PlanResourcesRequest();
        }
        
        [Obsolete("Use WithActions instead.")]
        public PlanResourcesRequest WithAction(string action)
        {
            Action = action;
            return this;
        }
        
        public PlanResourcesRequest WithActions(params string[] actions)
        {
            Actions.AddRange(actions);
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
            if (string.IsNullOrEmpty(Action) && Actions.Count == 0)
            {
                throw new Exception("Action(s) is not set");
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
                #pragma warning disable CS0612
                Action = Action,
                #pragma warning restore CS0612
                AuxData = AuxData?.ToAuxData(),
                IncludeMeta = IncludeMeta,
                Principal = Principal.ToPrincipal(),
                Resource = Resource.ToPlanResource(),
                RequestId = RequestId,
            };

            if (string.IsNullOrEmpty(Action)) {
                request.Actions.AddRange(Actions);
            }
            else if (Actions.Count == 0) {
                #pragma warning disable CS0612
                request.Action = Action;
                #pragma warning restore CS0612
            }
            else {
                throw new Exception("Either use WithAction or WithActions to specify action(s)");
            }

            return request;
        }
    }
}