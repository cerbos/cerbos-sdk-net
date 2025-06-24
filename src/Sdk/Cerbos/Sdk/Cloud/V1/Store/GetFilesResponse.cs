// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class GetFilesResponse
    {
        private Api.Cloud.V1.Store.GetFilesResponse R { get; }

        public long StoreVersion => R.StoreVersion;

        public List<File> Files { get; }

        public Api.Cloud.V1.Store.GetFilesResponse Raw => R;

        public GetFilesResponse(Api.Cloud.V1.Store.GetFilesResponse response)
        {
            R = response;
            if (response.Files.Count > 0)
            {
                Files = new List<File>(response.Files.Count);
                foreach (var file in response.Files)
                {
                    Files.Add(new File(file));
                }
            }
        }
    }
}
