// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

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
                    var req = request.ToGetFilesRequest();
                    return Utility.Rpc.Call(req, () =>
                        new GetFilesResponse(
                            Client.GetFiles(req)
                        )
                    );
                });
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
        }

        public Task<GetFilesResponse> GetFilesAsync(GetFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.ExecuteAsync(
                    async (token) =>
                    {
                        var req = request.ToGetFilesRequest();
                        return await Utility.Rpc.CallAsync(req, async () =>
                            new GetFilesResponse(
                                await Client.GetFilesAsync(req).ResponseAsync
                            )
                        );
                    }
                ).AsTask();
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
        }

        public ListFilesResponse ListFiles(ListFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.Execute(() =>
                {
                    var req = request.ToListFilesRequest();
                    return Utility.Rpc.Call(req, () =>
                        new ListFilesResponse(
                            Client.ListFiles(req)
                        )
                    );
                });
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
        }

        public Task<ListFilesResponse> ListFilesAsync(ListFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.ExecuteAsync(
                    async (token) =>
                    {
                        var req = request.ToListFilesRequest();
                        return await Utility.Rpc.CallAsync(req, async () =>
                            new ListFilesResponse(
                                await Client.ListFilesAsync(req).ResponseAsync
                            )
                        );
                    }
                ).AsTask();
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
        }

        public ModifyFilesResponse ModifyFiles(ModifyFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.Execute(() =>
                {
                    var req = request.ToModifyFilesRequest();
                    return Utility.Rpc.Call(req, () =>
                        new ModifyFilesResponse(
                            Client.ModifyFiles(req)
                        )
                    );
                });
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
        }

        public Task<ModifyFilesResponse> ModifyFilesAsync(ModifyFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.ExecuteAsync(
                    async (token) =>
                    {
                        var req = request.ToModifyFilesRequest();
                        return await Utility.Rpc.CallAsync(req, async () =>
                            new ModifyFilesResponse(
                                await Client.ModifyFilesAsync(req).ResponseAsync
                            )
                        );
                    }
                ).AsTask();
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
        }

        public ReplaceFilesResponse ReplaceFiles(ReplaceFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.Execute(() =>
                {
                    var req = request.ToReplaceFilesRequest();
                    return Utility.Rpc.Call(req, () =>
                        new ReplaceFilesResponse(
                            Client.ReplaceFiles(req)
                        )
                    );
                });
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
        }

        public Task<ReplaceFilesResponse> ReplaceFilesAsync(ReplaceFilesRequest request)
        {
            try
            {
                return Resilience.Pipeline.ExecuteAsync(
                    async (token) =>
                    {
                        var req = request.ToReplaceFilesRequest();
                        return await Utility.Rpc.CallAsync(req, async () =>
                            new ReplaceFilesResponse(
                                await Client.ReplaceFilesAsync(req).ResponseAsync
                            )
                        );
                    }
                ).AsTask();
            }
            catch (RpcException e)
            {
                ErrorDetailException.FromTrailers(e.Message, e.Trailers);
                throw;
            }
        }
    }
}
