// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Response
{
    public sealed class ListSchemasResponse
    {
        private Api.V1.Response.ListSchemasResponse R { get; }

        public Api.V1.Response.ListSchemasResponse Raw => R;

        public List<string> SchemaIds => R.SchemaIds.ToList();

        public ListSchemasResponse(Api.V1.Response.ListSchemasResponse response)
        {
            R = response;
        }
    }
}