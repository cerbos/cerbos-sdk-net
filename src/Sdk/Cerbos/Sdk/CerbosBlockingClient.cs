// Copyright 2021-2022 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Cerbos.Api.V1.Effect;
using Cerbos.Api.V1.Request;
using Cerbos.Api.V1.Response;
using Cerbos.Api.V1.Svc;
using Cerbos.Sdk.Builders;
using Grpc.Core;
using AuxData = Cerbos.Sdk.Builders.AuxData;

namespace Cerbos.Sdk
{
    public class CerbosBlockingClient
    {
        private readonly CerbosService.CerbosServiceClient _csc;
        private readonly CerbosAdminService.CerbosAdminServiceClient _casc;
        private readonly CerbosPlaygroundService.CerbosPlaygroundServiceClient _cpsc;
        private readonly AuxData _auxData;
        
        public CerbosBlockingClient(CerbosService.CerbosServiceClient csc, CerbosAdminService.CerbosAdminServiceClient casc,
            CerbosPlaygroundService.CerbosPlaygroundServiceClient cpsc, AuxData auxData)
        {
            _csc = csc;
            _casc = casc;
            _cpsc = cpsc;
            _auxData = auxData;
        }
        
        public CerbosBlockingClient(CerbosService.CerbosServiceClient csc, CerbosAdminService.CerbosAdminServiceClient casc,
            CerbosPlaygroundService.CerbosPlaygroundServiceClient cpsc)
        {
            _csc = csc;
            _casc = casc;
            _cpsc = cpsc;
        }

        public CerbosBlockingClient With(AuxData auxData) {
            return new CerbosBlockingClient(_csc, _casc, _cpsc, auxData);
        }
        
        public CheckResult Check(Principal principal, Resource resource, IEnumerable<string> actions)
        {
            return Check(RequestId.Generate(), principal, resource, actions);
        }

        public CheckResult Check(string requestId, Principal principal, Resource resource, IEnumerable<string> actions)
        {
            var request = new CheckResourceBatchRequest() {
                AuxData = _auxData?.ToAuxData() ,
                Principal = principal.ToPrincipal(),
                Resources =
                {
                    new CheckResourceBatchRequest.Types.BatchEntry
                    {
                        Resource = resource.ToResource(),
                        Actions = { actions }
                    }
                },
                RequestId = requestId,
            };

            CheckResourceBatchResponse response;
            try
            {
                response = _csc.CheckResourceBatch(request, Metadata.Empty);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to check: ${e}");
            }

            foreach (var result in response.Results)
            {
                return new CheckResult(result.Actions);
            }

            return new CheckResult(new Dictionary<string, Effect>());
        }
    }
}