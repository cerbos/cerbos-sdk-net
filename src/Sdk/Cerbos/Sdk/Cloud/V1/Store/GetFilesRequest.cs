// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class GetFilesRequest
    {
        private string StoreId { get; set; }
        private List<string> Files { get; }

        private GetFilesRequest()
        {
            Files = new List<string>();
        }
        
        public static GetFilesRequest NewInstance()
        {
            return new GetFilesRequest();
        }

        public GetFilesRequest WithStoreId(string storeId)
        {
            StoreId = storeId;
            return this;
        }

        public GetFilesRequest WithFiles(params string[] files)
        {
            Files.AddRange(files);
            return this;
        }
        
        public Api.Cloud.V1.Store.GetFilesRequest ToGetFilesRequest()
        {
            if (StoreId == null)
            {
                throw new Exception("StoreId must be specified");
            }

            return new Api.Cloud.V1.Store.GetFilesRequest
            {
                StoreId = StoreId,
                Files = { Files }
            };
        }
    }
}