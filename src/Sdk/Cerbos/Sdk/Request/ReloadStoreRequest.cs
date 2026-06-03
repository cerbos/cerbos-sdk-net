// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Request
{
    public sealed class ReloadStoreRequest
    {
        private bool Wait { get; }

        private ReloadStoreRequest(bool wait)
        {
            Wait = wait;
        }

        public static ReloadStoreRequest NewInstance(bool wait)
        {
            return new ReloadStoreRequest(wait);
        }

        public Api.V1.Request.ReloadStoreRequest ToReloadStoreRequest()
        {
            return new Api.V1.Request.ReloadStoreRequest()
            {
                Wait = Wait
            };
        }
    }
}