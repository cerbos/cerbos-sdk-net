// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Response
{
    public sealed class GetSchemaResponse
    {
        private Api.V1.Response.GetSchemaResponse R { get; }

        public Api.V1.Response.GetSchemaResponse Raw => R;

        public List<Schema.Schema> Schemas
        {
            get
            {
                var schemas = new List<Schema.Schema>();
                foreach (var schema in R.Schemas)
                {
                    schemas.Add(new Schema.Schema(schema));
                }

                return schemas;
            }
        }

        public GetSchemaResponse(Api.V1.Response.GetSchemaResponse response)
        {
            R = response;
        }
    }
}