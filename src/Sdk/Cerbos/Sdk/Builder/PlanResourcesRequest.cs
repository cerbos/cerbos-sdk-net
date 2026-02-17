// Copyright 2021-2026 Zenauth Ltd.
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
        private bool AllowPartialRequests { get; set; }
        private bool IncludeMeta { get; set; }
        private Principal Principal { get; set; }
        private Resource Resource { get; set; }
        private string RequestId { get; set; } = "";

        private PlanResourcesRequest()
        {
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

        public PlanResourcesRequest WithAllowPartialRequests(bool allowPartialRequests)
        {
            AllowPartialRequests = allowPartialRequests;
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
            var request = new Api.V1.Request.PlanResourcesRequest
            {
                AuxData = AuxData?.ToAuxData(),
                IncludeMeta = IncludeMeta,
                RequestId = string.IsNullOrEmpty(RequestId) ? Utility.RequestId.Generate() : RequestId,
            };

            if (!string.IsNullOrEmpty(Action))
            {
#pragma warning disable CS0612
                request.Action = Action;
#pragma warning restore CS0612
            }
            else if (Actions.Count > 0)
            {
                request.Actions.AddRange(Actions);
            }
            else if (!AllowPartialRequests)
            {
                throw new Exception("Action(s) is not set");
            }

            if (Principal != null)
            {
                request.Principal = Principal.ToPrincipal();
            }
            else if (!AllowPartialRequests)
            {
                throw new Exception("Principal is not set");
            }

            if (Resource != null)
            {
                request.Resource = Resource.ToPlanResource();
            }
            else if (!AllowPartialRequests)
            {
                throw new Exception("Resource is not set");
            }

            return request;
        }
    }
}