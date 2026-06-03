// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Response
{
    public sealed class PurgeStoreRevisionsResponse
    {
        private Api.V1.Response.PurgeStoreRevisionsResponse R { get; }

        public Api.V1.Response.PurgeStoreRevisionsResponse Raw => R;

        public uint AffectedRows => R.AffectedRows;

        public PurgeStoreRevisionsResponse(Api.V1.Response.PurgeStoreRevisionsResponse response)
        {
            R = response;
        }
    }
}