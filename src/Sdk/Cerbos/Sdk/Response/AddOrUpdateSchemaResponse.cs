// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Response
{
    public sealed class AddOrUpdateSchemaResponse
    {
        private Api.V1.Response.AddOrUpdateSchemaResponse R { get; }

        public Api.V1.Response.AddOrUpdateSchemaResponse Raw => R;

        public AddOrUpdateSchemaResponse(Api.V1.Response.AddOrUpdateSchemaResponse response)
        {
            R = response;
        }
    }
}