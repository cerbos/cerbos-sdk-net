// Copyright 2021-2022 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Cerbos.Api.V1.Engine;
using Cerbos.Api.V1.Response;
using Cerbos.Api.V1.Schema;

namespace Cerbos.Sdk
{
    /// <summary>
    /// PlanResourcesResult provides an interface to see query plan.
    /// </summary>
    public class PlanResourcesResult
    {
        private readonly PlanResourcesResponse _response;

        public PlanResourcesResult(PlanResourcesResponse resp) {
            _response = resp;
        }
        
        public string GetAction() {
            return _response.Action;
        }
        
        public string GetResourceKind() {
            return _response.ResourceKind;
        }
        
        public string GetPolicyVersion() {
            return _response.PolicyVersion;
        }
        
        public bool IsAlwaysAllowed() {
            return _response.Filter.Kind == PlanResourcesFilter.Types.Kind.AlwaysAllowed;
        }

        public bool IsAlwaysDenied() {
            return _response.Filter.Kind == PlanResourcesFilter.Types.Kind.AlwaysDenied;
        }
        
        public bool IsConditional() {
            return _response.Filter.Kind == PlanResourcesFilter.Types.Kind.Conditional;
        }
        
        public PlanResourcesFilter.Types.Expression.Types.Operand GetCondition() {
            return _response.Filter.Condition;
        }
        
        public bool HasValidationErrors() {
            return _response.ValidationErrors.Count > 0;
        }

        public IEnumerable<ValidationError> GetValidationErrors() {
            return _response.ValidationErrors;
        }
        
        public PlanResourcesResponse GetRaw()
        {
            return _response;
        }
    }
}