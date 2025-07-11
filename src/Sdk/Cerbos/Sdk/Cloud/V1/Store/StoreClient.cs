// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;
using Cerbos.Api.Cloud.V1.Store;
using Grpc.Core;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public interface IStoreClient
    {
        GetFilesResponse GetFiles(GetFilesRequest request);
        Task<GetFilesResponse> GetFilesAsync(GetFilesRequest request);
        ListFilesResponse ListFiles(ListFilesRequest request);
        Task<ListFilesResponse> ListFilesAsync(ListFilesRequest request);
        ModifyFilesResponse ModifyFiles(ModifyFilesRequest request);
        Task<ModifyFilesResponse> ModifyFilesAsync(ModifyFilesRequest request);
        ReplaceFilesResponse ReplaceFiles(ReplaceFilesRequest request);
        Task<ReplaceFilesResponse> ReplaceFilesAsync(ReplaceFilesRequest request);
    }

    /// <summary>
    /// StoreClient provides a client implementation that communicates with the Cerbos Hub Store Service.
    /// </summary>
    /// 
    public sealed class StoreClient : IStoreClient
    {
        private CerbosStoreService.CerbosStoreServiceClient Client { get; }

        public StoreClient(CerbosStoreService.CerbosStoreServiceClient storeServiceClient)
        {
            Client = storeServiceClient;
        }

        public GetFilesResponse GetFiles(GetFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.Execute(() =>
                {
                    return new GetFilesResponse(
                        Client.GetFiles(
                            request.ToGetFilesRequest()
                        )
                    );
                });
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to get files: ${e}");
            }
        }

        public Task<GetFilesResponse> GetFilesAsync(GetFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.ExecuteAsync(
                    async (token) => {
                        return await Client.GetFilesAsync(request.ToGetFilesRequest()).ResponseAsync.ContinueWith(r => new GetFilesResponse(r.Result));
                    }
                ).AsTask();
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to get files: ${e}");
            }
        }

        public ListFilesResponse ListFiles(ListFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.Execute(() =>
                {
                    return new ListFilesResponse(
                        Client.ListFiles(
                            request.ToListFilesRequest()
                        )
                    );
                });
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to list files: ${e}");
            }
        }

        public Task<ListFilesResponse> ListFilesAsync(ListFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.ExecuteAsync(
                    async (token) => {
                        return await Client.ListFilesAsync(request.ToListFilesRequest()).ResponseAsync.ContinueWith(r => new ListFilesResponse(r.Result));
                    }
                ).AsTask();
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to list files: ${e}");
            }
        }

        public ModifyFilesResponse ModifyFiles(ModifyFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.Execute(() =>
                {
                    return new ModifyFilesResponse(
                        Client.ModifyFiles(
                            request.ToModifyFilesRequest()
                        )
                    );
                });
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to modify files: ${e}");
            }
        }

        public Task<ModifyFilesResponse> ModifyFilesAsync(ModifyFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.ExecuteAsync(
                    async (token) => {
                        return await Client.ModifyFilesAsync(request.ToModifyFilesRequest()).ResponseAsync.ContinueWith(r => new ModifyFilesResponse(r.Result));
                    }
                ).AsTask();
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to modify files: ${e}");
            }
        }

        public ReplaceFilesResponse ReplaceFiles(ReplaceFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.Execute(() =>
                {
                    return new ReplaceFilesResponse(
                        Client.ReplaceFiles(
                            request.ToReplaceFilesRequest()
                        )
                    );
                });
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to replace files: ${e}");
            }
        }

        public Task<ReplaceFilesResponse> ReplaceFilesAsync(ReplaceFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.ExecuteAsync(
                    async (token) => {
                        return await Client.ReplaceFilesAsync(request.ToReplaceFilesRequest()).ResponseAsync.ContinueWith(r => new ReplaceFilesResponse(r.Result));
                    }
                ).AsTask();
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to replace files: ${e}");
            }
        }
    }
}
