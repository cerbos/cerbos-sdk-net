// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class ListFilesRequest
    {
        private string StoreId { get; set; }
        private FileFilter Filter { get; set; }

        private ListFilesRequest() { }
        
        public static ListFilesRequest NewInstance()
        {
            return new ListFilesRequest();
        }

        public ListFilesRequest WithStoreId(string storeId)
        {
            StoreId = storeId;
            return this;
        }

        public ListFilesRequest WithFilter(FileFilter filter)
        {
            Filter = filter;
            return this;
        }
        
        public Api.Cloud.V1.Store.ListFilesRequest ToListFilesRequest()
        {
            if (StoreId == null)
            {
                throw new Exception("StoreId must be specified");
            }

            var request = new Api.Cloud.V1.Store.ListFilesRequest
            {
                StoreId = StoreId,
            };

            if (Filter != null)
            {
                request.Filter = Filter.ToFileFilter();
            }

            return request;
        }
    }
}