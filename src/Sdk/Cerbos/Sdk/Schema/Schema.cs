// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Google.Protobuf;

namespace Cerbos.Sdk.Schema
{
    public sealed class Schema
    {
        private Api.V1.Schema.Schema S { get; }

        public Api.V1.Schema.Schema Raw => S;

        public string Id => S.Id;

        public string Definition => S.Definition.ToStringUtf8();

        private Schema(string id, string definition)
        {
            S = new Api.V1.Schema.Schema()
            {
                Id = id,
                Definition = ByteString.CopyFromUtf8(definition)
            };
        }

        public Schema(Api.V1.Schema.Schema schema)
        {
            S = schema;
        }

        public static Schema NewInstance(string id, string definition)
        {
            return new Schema(id, definition);
        }

        public Api.V1.Schema.Schema ToSchema()
        {
            return Raw;
        }
    }
}