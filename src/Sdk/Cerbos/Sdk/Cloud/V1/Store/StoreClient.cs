// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;
using Cerbos.Api.Cloud.V1.Store;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public interface IStoreClient
    {
        GetFilesResponse GetFiles(GetFilesRequest request);
        Task<GetFilesResponse> GetFilesAsync(GetFilesRequest request);
        ListFilesResponse ListFiles(ListFilesRequest request);
        Task<ListFilesResponse> ListFilesAsync(ListFilesRequest request);
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
                return new GetFilesResponse(
                    Client.GetFiles(
                        request.ToGetFilesRequest()
                    )
                );
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
                return Client
                    .GetFilesAsync(request.ToGetFilesRequest())
                    .ResponseAsync
                    .ContinueWith(
                        r => new GetFilesResponse(r.Result)
                    );
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
                return new ListFilesResponse(
                    Client.ListFiles(
                        request.ToListFilesRequest()
                    )
                );
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
                return Client
                    .ListFilesAsync(request.ToListFilesRequest())
                    .ResponseAsync
                    .ContinueWith(
                        r => new ListFilesResponse(r.Result)
                    );
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to list files: ${e}");
            }
        }
    }
}
