// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Request
{
    public sealed class GetSchemaRequest
    {
        private List<string> Id { get; }

        private GetSchemaRequest(params string[] id)
        {
            Id = new List<string>(id);
        }

        public static GetSchemaRequest NewInstance(params string[] id)
        {
            return new GetSchemaRequest(id);
        }

        public Api.V1.Request.GetSchemaRequest ToGetSchemaRequest()
        {
            var request = new Api.V1.Request.GetSchemaRequest();
            if (Id.Count > 0)
            {
                request.Id.AddRange(Id);
            }

            return request;
        }
    }
}