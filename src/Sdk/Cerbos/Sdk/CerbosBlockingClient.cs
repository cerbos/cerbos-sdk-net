// Copyright 2021-2022 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using Cerbos.Api.V1.Request;
using Cerbos.Api.V1.Response;
using Cerbos.Api.V1.Svc;
using Cerbos.Sdk.Builders;

namespace Cerbos.Sdk
{
    /// <summary>
    /// CerbosBlockingClient provides a client implementation that blocks waiting for a response from the PDP.
    /// </summary>
    public class CerbosBlockingClient
    {
        private readonly CerbosService.CerbosServiceClient _csc;
        private readonly Builders.AuxData _auxData;

        public CerbosBlockingClient(CerbosService.CerbosServiceClient csc)
        {
            _csc = csc;
        }

        public CerbosBlockingClient(CerbosService.CerbosServiceClient csc, Builders.AuxData auxData)
        {
            _csc = csc;
            _auxData = auxData;
        }

        /// <summary>
        /// Automatically attach the provided auxiliary data to requests.
        /// </summary>
        public CerbosBlockingClient With(Builders.AuxData auxData)
        {
            return new CerbosBlockingClient(_csc, auxData);
        }

        /// <summary>
        /// Send a request consisting of a principal, resource(s) & action(s) to see if the principal is authorized to do the action(s) on the resource(s).
        /// </summary>
        private CheckResourcesResult CheckResources(CheckResourcesRequest request)
        {
            CheckResourcesResponse response;
            try
            {
                request.AuxData = _auxData?.ToAuxData();
                response = _csc.CheckResources(request);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to check resources: ${e}");
            }

            return new CheckResourcesResult(response);
        }
        
        /// <summary>
        /// Check whether the principal is authorized to do the actions on the resources.
        /// </summary>
        /// <param name="requestId">Use the requestId to trace the request on Cerbos</param>
        public CheckResourcesResult CheckResources(string requestId, Principal principal,
            params ResourceAction[] resourceActions)
        {
            var request = new CheckResourcesRequest
            {
                RequestId = requestId,
                IncludeMeta = false,
                Principal = principal.ToPrincipal(),
            };

            foreach (var resourceAction in resourceActions)
            {
                request.Resources.Add(resourceAction.ToResourceEntry());
            }

            return CheckResources(request);;
        }
        
        /// <summary>
        /// Check whether the principal is authorized to do the actions on the resources.
        /// </summary>
        public CheckResourcesResult CheckResources(Principal principal,
            params ResourceAction[] resourceActions)
        {
            return CheckResources(RequestId.Generate(), principal, resourceActions);
        }

        /// <summary>
        /// Check whether the principal is authorized to do the action on the resource.
        /// </summary>
        /// <param name="requestId">Use the requestId to trace the request on Cerbos</param>
        public CheckResult CheckResources(string requestId, Principal principal,
            ResourceAction resourceAction)
        {
            var result = CheckResources(new CheckResourcesRequest
            {
                RequestId = requestId,
                IncludeMeta = false,
                Principal = principal.ToPrincipal(),
                Resources = { resourceAction.ToResourceEntry() }
            });

            return result.Find(resourceAction.ToResourceEntry().Resource.Id);
        }

        /// <summary>
        /// Check whether the principal is authorized to do the action on the resource.
        /// </summary>
        public CheckResult CheckResources(Principal principal, ResourceAction resourceAction)
        {
            return CheckResources(RequestId.Generate(), principal, resourceAction);
        }

        /// <summary>
        /// Check whether the principal is authorized to do the action on the resource.
        /// </summary>
        /// <param name="requestId">Use the requestId to trace the request on Cerbos</param>
        public CheckResult CheckResources(string requestId, Principal principal, Resource resource,
            params string[] actions)
        {
            return CheckResources(requestId, principal, ResourceAction.NewInstance(resource, actions));
        }

        /// <summary>
        /// Check whether the principal is authorized to do the action on the resource.
        /// </summary>
        public CheckResult CheckResources(Principal principal, Resource resource, params string[] actions)
        {
            return CheckResources(RequestId.Generate(), principal, resource, actions);
        }
    }
}