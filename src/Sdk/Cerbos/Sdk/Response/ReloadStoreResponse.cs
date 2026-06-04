// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Response
{
    public sealed class ReloadStoreResponse
    {
        private Api.V1.Response.ReloadStoreResponse R { get; }

        public Api.V1.Response.ReloadStoreResponse Raw => R;

        public ReloadStoreResponse(Api.V1.Response.ReloadStoreResponse response)
        {
            R = response;
        }
    }
}