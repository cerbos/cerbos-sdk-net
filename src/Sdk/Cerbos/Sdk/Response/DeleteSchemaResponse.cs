// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Response
{
    public sealed class DeleteSchemaResponse
    {
        private Api.V1.Response.DeleteSchemaResponse R { get; }

        public Api.V1.Response.DeleteSchemaResponse Raw => R;

        public uint DeletedSchemas => R.DeletedSchemas;


        public DeleteSchemaResponse(Api.V1.Response.DeleteSchemaResponse response)
        {
            R = response;
        }
    }
}