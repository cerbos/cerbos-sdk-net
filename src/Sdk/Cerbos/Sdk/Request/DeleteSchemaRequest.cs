// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Request
{
    public sealed class DeleteSchemaRequest
    {
        private List<string> Id { get; }

        private DeleteSchemaRequest(params string[] id)
        {
            Id = new List<string>(id);
        }

        public static DeleteSchemaRequest NewInstance(params string[] id)
        {
            return new DeleteSchemaRequest(id);
        }

        public Api.V1.Request.DeleteSchemaRequest ToDeleteSchemaRequest()
        {
            var request = new Api.V1.Request.DeleteSchemaRequest();
            if (Id.Count > 0)
            {
                request.Id.AddRange(Id);
            }

            return request;
        }
    }
}
