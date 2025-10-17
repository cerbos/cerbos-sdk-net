// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class GetFilesRequest
    {
        private string StoreId { get; set; }
        private List<string> Files { get; }

        private GetFilesRequest(string storeId, params string[] files)
        {
            StoreId = storeId;
            Files = new List<string>(files);
        }

        public static GetFilesRequest NewInstance(string storeId, params string[] files)
        {
            return new GetFilesRequest(storeId, files);
        }

        public Api.Cloud.V1.Store.GetFilesRequest ToGetFilesRequest()
        {
            return new Api.Cloud.V1.Store.GetFilesRequest
            {
                StoreId = StoreId,
                Files = { Files }
            };
        }
    }
}