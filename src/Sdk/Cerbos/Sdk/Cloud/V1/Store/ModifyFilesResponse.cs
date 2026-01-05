// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class ModifyFilesResponse
    {
        private Api.Cloud.V1.Store.ModifyFilesResponse R { get; }

        public long NewStoreVersion => R.NewStoreVersion;

        public Api.Cloud.V1.Store.ModifyFilesResponse Raw => R;

        public ModifyFilesResponse(Api.Cloud.V1.Store.ModifyFilesResponse response)
        {
            R = response;
        }
    }
}
