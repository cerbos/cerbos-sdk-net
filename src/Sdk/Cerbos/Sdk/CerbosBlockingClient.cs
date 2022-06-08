// Copyright 2021-2022 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
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
            IEnumerable<CheckResourcesRequest.Types.ResourceEntry> resourceEntries)
        {
            var request = new CheckResourcesRequest
            {
                RequestId = requestId,
                IncludeMeta = false,
                Principal = principal.ToPrincipal(),
            };
            request.Resources.Add(resourceEntries);
            
            return CheckResources(request);;
        }
        
        public CheckResult CheckResources(string requestId, Principal principal,
            CheckResourcesRequest.Types.ResourceEntry resourceEntry)
        {
            var result = CheckResources(new CheckResourcesRequest
            {
                RequestId = requestId,
                IncludeMeta = false,
                Principal = principal.ToPrincipal(),
                Resources = { resourceEntry }
            });

            return result.Find(resourceEntry.Resource.Id);
        }

        public CheckResult CheckResources(Principal principal, CheckResourcesRequest.Types.ResourceEntry resourceEntry)
        {
            return CheckResources(RequestId.Generate(), principal, resourceEntry);
        }

        public CheckResult CheckResources(string requestId, Principal principal, Resource resource,
            IEnumerable<string> actions)
        {
            return CheckResources(requestId, principal, new CheckResourcesRequest.Types.ResourceEntry
            {
                Resource = resource.ToResource(),
                Actions = { actions }
            });
        }

        public CheckResult CheckResources(Principal principal, Resource resource, IEnumerable<string> actions)
        {
            return CheckResources(RequestId.Generate(), principal, new CheckResourcesRequest.Types.ResourceEntry
            {
                Resource = resource.ToResource(),
                Actions = { actions }
            });
        }
    }
}