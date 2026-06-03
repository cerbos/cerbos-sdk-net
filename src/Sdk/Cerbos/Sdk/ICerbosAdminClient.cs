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
        AddOrUpdatePolicyResponse AddOrUpdatePolicy(AddOrUpdatePolicyRequest request, Metadata headers = null);
        Task<AddOrUpdatePolicyResponse> AddOrUpdatePolicyAsync(AddOrUpdatePolicyRequest request, Metadata headers = null);
        DeletePolicyResponse DeletePolicy(DeletePolicyRequest request, Metadata headers = null);
        Task<DeletePolicyResponse> DeletePolicyAsync(DeletePolicyRequest request, Metadata headers = null);
        DisablePolicyResponse DisablePolicy(DisablePolicyRequest request, Metadata headers = null);
        Task<DisablePolicyResponse> DisablePolicyAsync(DisablePolicyRequest request, Metadata headers = null);
        EnablePolicyResponse EnablePolicy(EnablePolicyRequest request, Metadata headers = null);
        Task<EnablePolicyResponse> EnablePolicyAsync(EnablePolicyRequest request, Metadata headers = null);
        GetPolicyResponse GetPolicy(GetPolicyRequest request, Metadata headers = null);
        Task<GetPolicyResponse> GetPolicyAsync(GetPolicyRequest request, Metadata headers = null);
        ListPoliciesResponse ListPolicies(ListPoliciesRequest request, Metadata headers = null);
        Task<ListPoliciesResponse> ListPoliciesAsync(ListPoliciesRequest request, Metadata headers = null);
    }
}