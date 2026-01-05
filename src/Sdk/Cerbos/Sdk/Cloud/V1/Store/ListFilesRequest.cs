// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class ListFilesRequest
    {
        private string StoreId { get; set; }
        private FileFilter Filter { get; set; }

        private ListFilesRequest(string storeId)
        {
            StoreId = storeId;
        }

        public static ListFilesRequest NewInstance(string storeId)
        {
            return new ListFilesRequest(storeId);
        }

        public static ListFilesRequest WithFilter(string storeId, FileFilter filter)
        {
            return new ListFilesRequest(storeId)
            {
                Filter = filter
            };
        }

        public Api.Cloud.V1.Store.ListFilesRequest ToListFilesRequest()
        {
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