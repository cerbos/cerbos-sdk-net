// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;

namespace Cerbos.Sdk.Builder
{
    public sealed class AttributeValue
    {
        private Value V { get; }

        private AttributeValue(Value value)
        {
            V = value;
        }

        public static AttributeValue BoolValue(bool value)
        {
            return new AttributeValue(Value.ForBool(value));
        }

        public static AttributeValue DoubleValue(double value)
        {
            return new AttributeValue(Value.ForNumber(value));
        }

        public static AttributeValue ListValue(params AttributeValue[] values)
        {
            Value[] v = new Value[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                v[i] = values[i].ToValue();
            }
            return new AttributeValue(Value.ForList(v));
        }

        public static AttributeValue MapValue(Dictionary<string, AttributeValue> dict)
        {
            var s = new Struct();
            foreach (KeyValuePair<string, AttributeValue> kvp in dict)
            {
                s.Fields.Add(kvp.Key, kvp.Value.V);
            }

            return new AttributeValue(Value.ForStruct(s));
        }

        public static AttributeValue NullValue()
        {
            return new AttributeValue(Value.ForNull());
        }

        public static AttributeValue StringValue(string value)
        {
            return new AttributeValue(Value.ForString(value));
        }

        public Value ToValue()
        {
            return V;
        }
    }
}