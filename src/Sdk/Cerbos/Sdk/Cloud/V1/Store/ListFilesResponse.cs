// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class ListFilesResponse
    {
        private Api.Cloud.V1.Store.ListFilesResponse R { get; }

        public long StoreVersion => R.StoreVersion;

        public List<string> Files { get; }

        public Api.Cloud.V1.Store.ListFilesResponse Raw => R;

        public ListFilesResponse(Api.Cloud.V1.Store.ListFilesResponse response)
        {
            R = response;
            Files = response.Files.ToList();
        }
    }
}
