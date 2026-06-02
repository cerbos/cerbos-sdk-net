// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Threading.Tasks;
using Grpc.Core;
using Cerbos.Sdk.Request;
using Cerbos.Sdk.Response;

namespace Cerbos.Sdk
{
    public interface ICerbosAdminClient
    {
        ListPoliciesResponse ListPolicies(ListPoliciesRequest request, Metadata headers = null);
        Task<ListPoliciesResponse> ListPoliciesAsync(ListPoliciesRequest request, Metadata headers = null);
    }
}