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
        AddOrUpdateSchemaResponse AddOrUpdateSchema(AddOrUpdateSchemaRequest request, Metadata headers = null);
        Task<AddOrUpdateSchemaResponse> AddOrUpdateSchemaAsync(AddOrUpdateSchemaRequest request, Metadata headers = null);
        DeletePolicyResponse DeletePolicy(DeletePolicyRequest request, Metadata headers = null);
        Task<DeletePolicyResponse> DeletePolicyAsync(DeletePolicyRequest request, Metadata headers = null);
        DeleteSchemaResponse DeleteSchema(DeleteSchemaRequest request, Metadata headers = null);
        Task<DeleteSchemaResponse> DeleteSchemaAsync(DeleteSchemaRequest request, Metadata headers = null);
        DisablePolicyResponse DisablePolicy(DisablePolicyRequest request, Metadata headers = null);
        Task<DisablePolicyResponse> DisablePolicyAsync(DisablePolicyRequest request, Metadata headers = null);
        EnablePolicyResponse EnablePolicy(EnablePolicyRequest request, Metadata headers = null);
        Task<EnablePolicyResponse> EnablePolicyAsync(EnablePolicyRequest request, Metadata headers = null);
        GetPolicyResponse GetPolicy(GetPolicyRequest request, Metadata headers = null);
        Task<GetPolicyResponse> GetPolicyAsync(GetPolicyRequest request, Metadata headers = null);
        GetSchemaResponse GetSchema(GetSchemaRequest request, Metadata headers = null);
        Task<GetSchemaResponse> GetSchemaAsync(GetSchemaRequest request, Metadata headers = null);
        InspectPoliciesResponse InspectPolicies(InspectPoliciesRequest request, Metadata headers = null);
        Task<InspectPoliciesResponse> InspectPoliciesAsync(InspectPoliciesRequest request, Metadata headers = null);
        ListPoliciesResponse ListPolicies(ListPoliciesRequest request, Metadata headers = null);
        Task<ListPoliciesResponse> ListPoliciesAsync(ListPoliciesRequest request, Metadata headers = null);
        ListSchemasResponse ListSchemas(ListSchemasRequest request, Metadata headers = null);
        Task<ListSchemasResponse> ListSchemasAsync(ListSchemasRequest request, Metadata headers = null);
        PurgeStoreRevisionsResponse PurgeStoreRevisions(PurgeStoreRevisionsRequest request, Metadata headers = null);
        Task<PurgeStoreRevisionsResponse> PurgeStoreRevisionsAsync(PurgeStoreRevisionsRequest request, Metadata headers = null);
        ReloadStoreResponse ReloadStore(ReloadStoreRequest request, Metadata headers = null);
        Task<ReloadStoreResponse> ReloadStoreAsync(ReloadStoreRequest request, Metadata headers = null);
    }
}