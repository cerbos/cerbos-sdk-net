// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

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

        public AddOrUpdatePolicyResponse AddOrUpdatePolicy(AddOrUpdatePolicyRequest request, Metadata headers = null)
        {
            var req = request.ToAddOrUpdatePolicyRequest();
            return Utility.Rpc.Call(req, () =>
                new AddOrUpdatePolicyResponse(
                    CerbosAdminServiceClient.AddOrUpdatePolicy(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<AddOrUpdatePolicyResponse> AddOrUpdatePolicyAsync(AddOrUpdatePolicyRequest request, Metadata headers = null)
        {
            var req = request.ToAddOrUpdatePolicyRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new AddOrUpdatePolicyResponse(
                    await CerbosAdminServiceClient
                        .AddOrUpdatePolicyAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public AddOrUpdateSchemaResponse AddOrUpdateSchema(AddOrUpdateSchemaRequest request, Metadata headers = null)
        {
            var req = request.ToAddOrUpdateSchemaRequest();
            return Utility.Rpc.Call(req, () =>
                new AddOrUpdateSchemaResponse(
                    CerbosAdminServiceClient.AddOrUpdateSchema(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<AddOrUpdateSchemaResponse> AddOrUpdateSchemaAsync(AddOrUpdateSchemaRequest request, Metadata headers = null)
        {
            var req = request.ToAddOrUpdateSchemaRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new AddOrUpdateSchemaResponse(
                    await CerbosAdminServiceClient
                        .AddOrUpdateSchemaAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public DeletePolicyResponse DeletePolicy(DeletePolicyRequest request, Metadata headers = null)
        {
            var req = request.ToDeletePolicyRequest();
            return Utility.Rpc.Call(req, () =>
                new DeletePolicyResponse(
                    CerbosAdminServiceClient.DeletePolicy(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<DeletePolicyResponse> DeletePolicyAsync(DeletePolicyRequest request, Metadata headers = null)
        {
            var req = request.ToDeletePolicyRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new DeletePolicyResponse(
                    await CerbosAdminServiceClient
                        .DeletePolicyAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public DeleteSchemaResponse DeleteSchema(DeleteSchemaRequest request, Metadata headers = null)
        {
            var req = request.ToDeleteSchemaRequest();
            return Utility.Rpc.Call(req, () =>
                new DeleteSchemaResponse(
                    CerbosAdminServiceClient.DeleteSchema(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<DeleteSchemaResponse> DeleteSchemaAsync(DeleteSchemaRequest request, Metadata headers = null)
        {
            var req = request.ToDeleteSchemaRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new DeleteSchemaResponse(
                    await CerbosAdminServiceClient
                        .DeleteSchemaAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public DisablePolicyResponse DisablePolicy(DisablePolicyRequest request, Metadata headers = null)
        {
            var req = request.ToDisablePolicyRequest();
            return Utility.Rpc.Call(req, () =>
                new DisablePolicyResponse(
                    CerbosAdminServiceClient.DisablePolicy(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<DisablePolicyResponse> DisablePolicyAsync(DisablePolicyRequest request, Metadata headers = null)
        {
            var req = request.ToDisablePolicyRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new DisablePolicyResponse(
                    await CerbosAdminServiceClient
                        .DisablePolicyAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public EnablePolicyResponse EnablePolicy(EnablePolicyRequest request, Metadata headers = null)
        {
            var req = request.ToEnablePolicyRequest();
            return Utility.Rpc.Call(req, () =>
                new EnablePolicyResponse(
                    CerbosAdminServiceClient.EnablePolicy(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<EnablePolicyResponse> EnablePolicyAsync(EnablePolicyRequest request, Metadata headers = null)
        {
            var req = request.ToEnablePolicyRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new EnablePolicyResponse(
                    await CerbosAdminServiceClient
                        .EnablePolicyAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public GetPolicyResponse GetPolicy(GetPolicyRequest request, Metadata headers = null)
        {
            var req = request.ToGetPolicyRequest();
            return Utility.Rpc.Call(req, () =>
                new GetPolicyResponse(
                    CerbosAdminServiceClient.GetPolicy(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<GetPolicyResponse> GetPolicyAsync(GetPolicyRequest request, Metadata headers = null)
        {
            var req = request.ToGetPolicyRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new GetPolicyResponse(
                    await CerbosAdminServiceClient
                        .GetPolicyAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public GetSchemaResponse GetSchema(GetSchemaRequest request, Metadata headers = null)
        {
            var req = request.ToGetSchemaRequest();
            return Utility.Rpc.Call(req, () =>
                new GetSchemaResponse(
                    CerbosAdminServiceClient.GetSchema(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<GetSchemaResponse> GetSchemaAsync(GetSchemaRequest request, Metadata headers = null)
        {
            var req = request.ToGetSchemaRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new GetSchemaResponse(
                    await CerbosAdminServiceClient
                        .GetSchemaAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public InspectPoliciesResponse InspectPolicies(InspectPoliciesRequest request, Metadata headers = null)
        {
            var req = request.ToInspectPoliciesRequest();
            return Utility.Rpc.Call(req, () =>
                new InspectPoliciesResponse(
                    CerbosAdminServiceClient.InspectPolicies(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<InspectPoliciesResponse> InspectPoliciesAsync(InspectPoliciesRequest request, Metadata headers = null)
        {
            var req = request.ToInspectPoliciesRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new InspectPoliciesResponse(
                    await CerbosAdminServiceClient
                        .InspectPoliciesAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public ListPoliciesResponse ListPolicies(ListPoliciesRequest request, Metadata headers = null)
        {
            var req = request.ToListPoliciesRequest();
            return Utility.Rpc.Call(req, () =>
                new ListPoliciesResponse(
                    CerbosAdminServiceClient.ListPolicies(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<ListPoliciesResponse> ListPoliciesAsync(ListPoliciesRequest request, Metadata headers = null)
        {
            var req = request.ToListPoliciesRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new ListPoliciesResponse(
                    await CerbosAdminServiceClient
                        .ListPoliciesAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public ListSchemasResponse ListSchemas(ListSchemasRequest request, Metadata headers = null)
        {
            var req = request.ToListSchemasRequest();
            return Utility.Rpc.Call(req, () =>
                new ListSchemasResponse(
                    CerbosAdminServiceClient.ListSchemas(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<ListSchemasResponse> ListSchemasAsync(ListSchemasRequest request, Metadata headers = null)
        {
            var req = request.ToListSchemasRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new ListSchemasResponse(
                    await CerbosAdminServiceClient
                        .ListSchemasAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public PurgeStoreRevisionsResponse PurgeStoreRevisions(PurgeStoreRevisionsRequest request, Metadata headers = null)
        {
            var req = request.ToPurgeStoreRevisionsRequest();
            return Utility.Rpc.Call(req, () =>
                new PurgeStoreRevisionsResponse(
                    CerbosAdminServiceClient.PurgeStoreRevisions(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<PurgeStoreRevisionsResponse> PurgeStoreRevisionsAsync(PurgeStoreRevisionsRequest request, Metadata headers = null)
        {
            var req = request.ToPurgeStoreRevisionsRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new PurgeStoreRevisionsResponse(
                    await CerbosAdminServiceClient
                        .PurgeStoreRevisionsAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }

        public ReloadStoreResponse ReloadStore(ReloadStoreRequest request, Metadata headers = null)
        {
            var req = request.ToReloadStoreRequest();
            return Utility.Rpc.Call(req, () =>
                new ReloadStoreResponse(
                    CerbosAdminServiceClient.ReloadStore(req, Utility.Metadata.Merge(_metadata, headers))
                )
            );
        }

        public Task<ReloadStoreResponse> ReloadStoreAsync(ReloadStoreRequest request, Metadata headers = null)
        {
            var req = request.ToReloadStoreRequest();
            return Utility.Rpc.CallAsync(req, async () =>
                new ReloadStoreResponse(
                    await CerbosAdminServiceClient
                        .ReloadStoreAsync(req, Utility.Metadata.Merge(_metadata, headers))
                        .ResponseAsync
                )
            );
        }
    }
}
