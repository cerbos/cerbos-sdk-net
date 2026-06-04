// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Request
{
    public sealed class PurgeStoreRevisionsRequest
    {
        private uint KeepLast { get; }

        private PurgeStoreRevisionsRequest(uint keepLast)
        {
            KeepLast = keepLast;
        }

        public static PurgeStoreRevisionsRequest NewInstance(uint keepLast)
        {
            return new PurgeStoreRevisionsRequest(keepLast);
        }

        public Api.V1.Request.PurgeStoreRevisionsRequest ToPurgeStoreRevisionsRequest()
        {
            return new Api.V1.Request.PurgeStoreRevisionsRequest()
            {
                KeepLast = KeepLast
            };
        }
    }
}