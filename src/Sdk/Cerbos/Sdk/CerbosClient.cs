using System;
using System.Collections.Generic;
using Cerbos.Api.V1.Effect;
using Cerbos.Api.V1.Request;
using Cerbos.Api.V1.Response;
using Cerbos.Api.V1.Svc;
using Cerbos.Sdk.Builders;
using Grpc.Core;

namespace Cerbos.Sdk
{
    public class CerbosBlockingClient
    {
        private readonly CerbosService.CerbosServiceClient _csc;
        private readonly CerbosAdminService.CerbosAdminServiceClient _casc;
        private readonly CerbosPlaygroundService.CerbosPlaygroundServiceClient _cpsc;
        public CerbosBlockingClient(CerbosService.CerbosServiceClient csc, CerbosAdminService.CerbosAdminServiceClient casc,
            CerbosPlaygroundService.CerbosPlaygroundServiceClient cpsc)
        {
            _csc = csc;
            _casc = casc;
            _cpsc = cpsc;
        }

        public CheckResult Check(Principal principal, Resource resource, IEnumerable<string> actions)
        {
            return Check(RequestId.Generate(), principal, resource, actions);
        }

        public CheckResult Check(string requestId, Principal principal, Resource resource, IEnumerable<string> actions)
        {
            var request = new CheckResourceBatchRequest() {
                AuxData = null,
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