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

        public AddOrUpdatePolicyResponse AddOrUpdatePolicy(AddOrUpdatePolicyRequest request, Metadata headers = null)
        {
            try
            {
                return new AddOrUpdatePolicyResponse(CerbosAdminServiceClient.AddOrUpdatePolicy(request.ToAddOrUpdatePolicyRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to add/update policy: ${e}");
            }
        }

        public Task<AddOrUpdatePolicyResponse> AddOrUpdatePolicyAsync(AddOrUpdatePolicyRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .AddOrUpdatePolicyAsync(request.ToAddOrUpdatePolicyRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new AddOrUpdatePolicyResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to add/update policy: ${e}");
            }
        }

        public AddOrUpdateSchemaResponse AddOrUpdateSchema(AddOrUpdateSchemaRequest request, Metadata headers = null)
        {
            try
            {
                return new AddOrUpdateSchemaResponse(CerbosAdminServiceClient.AddOrUpdateSchema(request.ToAddOrUpdateSchemaRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to add/update schema: ${e}");
            }
        }

        public Task<AddOrUpdateSchemaResponse> AddOrUpdateSchemaAsync(AddOrUpdateSchemaRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .AddOrUpdateSchemaAsync(request.ToAddOrUpdateSchemaRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new AddOrUpdateSchemaResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to add/update schema: ${e}");
            }
        }

        public DeletePolicyResponse DeletePolicy(DeletePolicyRequest request, Metadata headers = null)
        {
            try
            {
                return new DeletePolicyResponse(CerbosAdminServiceClient.DeletePolicy(request.ToDeletePolicyRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to delete policy: ${e}");
            }
        }

        public Task<DeletePolicyResponse> DeletePolicyAsync(DeletePolicyRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .DeletePolicyAsync(request.ToDeletePolicyRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new DeletePolicyResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to delete policy: ${e}");
            }
        }

        public DeleteSchemaResponse DeleteSchema(DeleteSchemaRequest request, Metadata headers = null)
        {
            try
            {
                return new DeleteSchemaResponse(CerbosAdminServiceClient.DeleteSchema(request.ToDeleteSchemaRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to delete schema: ${e}");
            }
        }

        public Task<DeleteSchemaResponse> DeleteSchemaAsync(DeleteSchemaRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .DeleteSchemaAsync(request.ToDeleteSchemaRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new DeleteSchemaResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to delete schema: ${e}");
            }
        }

        public DisablePolicyResponse DisablePolicy(DisablePolicyRequest request, Metadata headers = null)
        {
            try
            {
                return new DisablePolicyResponse(CerbosAdminServiceClient.DisablePolicy(request.ToDisablePolicyRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to disable policy: ${e}");
            }
        }

        public Task<DisablePolicyResponse> DisablePolicyAsync(DisablePolicyRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .DisablePolicyAsync(request.ToDisablePolicyRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new DisablePolicyResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to disable policy: ${e}");
            }
        }

        public EnablePolicyResponse EnablePolicy(EnablePolicyRequest request, Metadata headers = null)
        {
            try
            {
                return new EnablePolicyResponse(CerbosAdminServiceClient.EnablePolicy(request.ToEnablePolicyRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to enable policy: ${e}");
            }
        }

        public Task<EnablePolicyResponse> EnablePolicyAsync(EnablePolicyRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .EnablePolicyAsync(request.ToEnablePolicyRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new EnablePolicyResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to enable policy: ${e}");
            }
        }

        public GetPolicyResponse GetPolicy(GetPolicyRequest request, Metadata headers = null)
        {
            try
            {
                return new GetPolicyResponse(CerbosAdminServiceClient.GetPolicy(request.ToGetPolicyRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to get policy: ${e}");
            }
        }

        public Task<GetPolicyResponse> GetPolicyAsync(GetPolicyRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .GetPolicyAsync(request.ToGetPolicyRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new GetPolicyResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to get policy: ${e}");
            }
        }

        public GetSchemaResponse GetSchema(GetSchemaRequest request, Metadata headers = null)
        {
            try
            {
                return new GetSchemaResponse(CerbosAdminServiceClient.GetSchema(request.ToGetSchemaRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to get schema: ${e}");
            }
        }

        public Task<GetSchemaResponse> GetSchemaAsync(GetSchemaRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .GetSchemaAsync(request.ToGetSchemaRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new GetSchemaResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to get schema: ${e}");
            }
        }

        public InspectPoliciesResponse InspectPolicies(InspectPoliciesRequest request, Metadata headers = null)
        {
            try
            {
                return new InspectPoliciesResponse(CerbosAdminServiceClient.InspectPolicies(request.ToInspectPoliciesRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to inspect policies: ${e}");
            }
        }

        public Task<InspectPoliciesResponse> InspectPoliciesAsync(InspectPoliciesRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .InspectPoliciesAsync(request.ToInspectPoliciesRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new InspectPoliciesResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to inspect policies: ${e}");
            }
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
                throw new Exception($"Failed to list policies: ${e}");
            }
        }

        public ListSchemasResponse ListSchemas(ListSchemasRequest request, Metadata headers = null)
        {
            try
            {
                return new ListSchemasResponse(CerbosAdminServiceClient.ListSchemas(request.ToListSchemasRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to list schemas: ${e}");
            }
        }

        public Task<ListSchemasResponse> ListSchemasAsync(ListSchemasRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .ListSchemasAsync(request.ToListSchemasRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new ListSchemasResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to list schemas: ${e}");
            }
        }

        public PurgeStoreRevisionsResponse PurgeStoreRevisions(PurgeStoreRevisionsRequest request, Metadata headers = null)
        {
            try
            {
                return new PurgeStoreRevisionsResponse(CerbosAdminServiceClient.PurgeStoreRevisions(request.ToPurgeStoreRevisionsRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to purge store revisions: ${e}");
            }
        }

        public Task<PurgeStoreRevisionsResponse> PurgeStoreRevisionsAsync(PurgeStoreRevisionsRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .PurgeStoreRevisionsAsync(request.ToPurgeStoreRevisionsRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new PurgeStoreRevisionsResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to purge store revisions: ${e}");
            }
        }

        public ReloadStoreResponse ReloadStore(ReloadStoreRequest request, Metadata headers = null)
        {
            try
            {
                return new ReloadStoreResponse(CerbosAdminServiceClient.ReloadStore(request.ToReloadStoreRequest(), Utility.Metadata.Merge(_metadata, headers)));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to reload store: ${e}");
            }
        }

        public Task<ReloadStoreResponse> ReloadStoreAsync(ReloadStoreRequest request, Metadata headers = null)
        {
            try
            {
                return CerbosAdminServiceClient
                    .ReloadStoreAsync(request.ToReloadStoreRequest(), Utility.Metadata.Merge(_metadata, headers))
                    .ResponseAsync
                    .ContinueWith(
                        r => new ReloadStoreResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to reload store: ${e}");
            }
        }
    }
}
