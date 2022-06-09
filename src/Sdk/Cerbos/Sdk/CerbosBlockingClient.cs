// Copyright 2021-2022 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using Cerbos.Api.V1.Request;
using Cerbos.Api.V1.Response;
using Cerbos.Api.V1.Svc;
using Cerbos.Sdk.Builders;

namespace Cerbos.Sdk
{
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

        public CerbosBlockingClient With(Builders.AuxData auxData)
        {
            return new CerbosBlockingClient(_csc, auxData);
        }

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
        
        public CheckResourcesResult CheckResources(Principal principal,
            params ResourceAction[] resourceActions)
        {
            return CheckResources(RequestId.Generate(), principal, resourceActions);
        }

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

        public CheckResult CheckResources(Principal principal, ResourceAction resourceAction)
        {
            return CheckResources(RequestId.Generate(), principal, resourceAction);
        }

        public CheckResult CheckResources(string requestId, Principal principal, Resource resource,
            params string[] actions)
        {
            return CheckResources(requestId, principal, ResourceAction.NewInstance(resource, actions));
        }

        public CheckResult CheckResources(Principal principal, Resource resource, params string[] actions)
        {
            return CheckResources(RequestId.Generate(), principal, resource, actions);
        }
    }
}