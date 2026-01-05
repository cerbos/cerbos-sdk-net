// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class ReplaceFilesResponse
    {
        private Api.Cloud.V1.Store.ReplaceFilesResponse R { get; }

        public long NewStoreVersion => R.NewStoreVersion;

        public List<string> IgnoredFiles { get; }

        public Api.Cloud.V1.Store.ReplaceFilesResponse Raw => R;

        public ReplaceFilesResponse(Api.Cloud.V1.Store.ReplaceFilesResponse response)
        {
            R = response;
            IgnoredFiles = response.IgnoredFiles.ToList();
        }
    }
}
