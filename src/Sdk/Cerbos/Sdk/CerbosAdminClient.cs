// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;
using Cerbos.Sdk.Request;
using Cerbos.Sdk.Response;
using Grpc.Core;

namespace Cerbos.Sdk
{
    /// <summary>
    /// CerbosClient provides a client implementation that communicates with the PDP.
    /// </summary>
    public sealed class CerbosAdminClient : ICerbosAdminClient
    {
        private Api.V1.Svc.CerbosAdminService.CerbosAdminServiceClient CerbosAdminServiceClient { get; }
        private readonly Metadata _metadata;

        public CerbosAdminClient(
            Api.V1.Svc.CerbosAdminService.CerbosAdminServiceClient cerbosAdminServiceClient,
            Metadata metadata = null
        )
        {
            CerbosAdminServiceClient = cerbosAdminServiceClient;
            _metadata = metadata;
        }

        public ListPoliciesResponse ListPolicies(ListPoliciesRequest request, Metadata headers = null)
        {
            try
            {
                return new ListPoliciesResponse(CerbosAdminServiceClient.ListPolicies(request.ToListPoliciesRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to list policies: ${e}");
            }
        }

        public Task<ListPoliciesResponse> ListPoliciesAsync(ListPoliciesRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .ListPoliciesAsync(request.ToListPoliciesRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new ListPoliciesResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to check resources: ${e}");
            }
        }
    }
}
