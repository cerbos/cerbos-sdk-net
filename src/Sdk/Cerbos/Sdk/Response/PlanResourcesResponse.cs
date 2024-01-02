// Copyright 2021-2024 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Response
{
    public sealed class PlanResourcesResponse
    {
        private Api.V1.Response.PlanResourcesResponse R { get; }
        
        public string Action => R.Action;
        public Api.V1.Engine.PlanResourcesFilter Filter => R.Filter;
        public Types.Meta Meta => new Types.Meta(R.Meta);
        public string PolicyVersion => R.PolicyVersion;
        public Api.V1.Response.PlanResourcesResponse Raw => R;
        public string ResourceKind => R.ResourceKind;
        public string RequestId => R.RequestId;
        public List<Cerbos.Api.V1.Schema.ValidationError> ValidationErrors => R.ValidationErrors.ToList();

        public PlanResourcesResponse(Api.V1.Response.PlanResourcesResponse response)
        {
            R = response;
        }

        public bool HasValidationErrors()
        {
            return R.ValidationErrors.Count > 0;
        }

        public bool IsAlwaysAllowed()
        {
            return R.Filter.Kind == Api.V1.Engine.PlanResourcesFilter.Types.Kind.AlwaysAllowed;
        }
        
        public bool IsAlwaysDenied()
        {
            return R.Filter.Kind == Api.V1.Engine.PlanResourcesFilter.Types.Kind.AlwaysDenied;
        }
        
        public bool IsConditional()
        {
            return R.Filter.Kind == Api.V1.Engine.PlanResourcesFilter.Types.Kind.Conditional;
        }

        public static class Types
        {
            public sealed class Meta
            {
                private Api.V1.Response.PlanResourcesResponse.Types.Meta M { get; }
        
                public string FilterDebug => M.FilterDebug;
                public string MatchedScope => M.MatchedScope;
                public Api.V1.Response.PlanResourcesResponse.Types.Meta Raw => M;

                public Meta(Api.V1.Response.PlanResourcesResponse.Types.Meta meta)
                {
                    M = meta;
                }
            }
        }
    }
}