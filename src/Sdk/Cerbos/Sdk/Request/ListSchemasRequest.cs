// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Request
{
    public sealed class ListSchemasRequest
    {
        private ListSchemasRequest() { }

        public static ListSchemasRequest NewInstance()
        {
            return new ListSchemasRequest();
        }

        public Api.V1.Request.ListSchemasRequest ToListSchemasRequest()
        {
            return new Api.V1.Request.ListSchemasRequest();
        }
    }
}