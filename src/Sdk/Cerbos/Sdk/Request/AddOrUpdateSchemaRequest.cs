// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.IO;

namespace Cerbos.Sdk.Request
{
    public sealed class AddOrUpdateSchemaRequest
    {
        private readonly Api.V1.Request.AddOrUpdateSchemaRequest R;

        private AddOrUpdateSchemaRequest()
        {
            R = new Api.V1.Request.AddOrUpdateSchemaRequest();
        }

        public static AddOrUpdateSchemaRequest NewInstance()
        {
            return new AddOrUpdateSchemaRequest();
        }

        public AddOrUpdateSchemaRequest With(params Schema.Schema[] schema)
        {
            foreach (var s in schema)
            {
                R.Schemas.Add(s.ToSchema());
            }
            return this;
        }

        public AddOrUpdateSchemaRequest WithJson(string id, TextReader definition)
        {
            return WithJson(id, definition.ReadToEnd());
        }

        public AddOrUpdateSchemaRequest WithJson(string id, string definition)
        {
            return AddSchema(id, definition);
        }

        private AddOrUpdateSchemaRequest AddSchema(string id, string definition)
        {
            R.Schemas.Add(Schema.Schema.NewInstance(id, definition).ToSchema());
            return this;
        }

        public Api.V1.Request.AddOrUpdateSchemaRequest ToAddOrUpdateSchemaRequest()
        {
            return R;
        }
    }
}
