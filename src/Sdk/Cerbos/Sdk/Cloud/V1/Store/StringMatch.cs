// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class StringMatch
    {
        private Api.Cloud.V1.Store.StringMatch.MatchOneofCase OneOf = Api.Cloud.V1.Store.StringMatch.MatchOneofCase.None;
        private string Contains_ { get; set; }
        private string Equals_ { get; set; }
        private Types.InList In_ { get; set; }

        private StringMatch(
            string contains = null,
            string equals = null,
            Types.InList inList = null
        )
        {
            if (contains == null && equals == null && inList == null)
            {
                throw new Exception("Either contains, equals or in match operator must be specified");
            }

            if (contains != null)
            {
                OneOf = Api.Cloud.V1.Store.StringMatch.MatchOneofCase.Contains;
                Contains_ = contains;
            }
            else if (equals != null)
            {
                OneOf = Api.Cloud.V1.Store.StringMatch.MatchOneofCase.Equals_;
                Equals_ = equals;
            }
            else if (inList != null)
            {
                OneOf = Api.Cloud.V1.Store.StringMatch.MatchOneofCase.In;
                In_ = inList;
            }
        }

        public static StringMatch Contains(string contains)
        {
            return new StringMatch(contains, null, null);
        }

        public static StringMatch Equals(string equals)
        {
            return new StringMatch(null, equals, null);
        }

        public static StringMatch In(Types.InList inList)
        {
            if (inList == null)
            {
                throw new Exception("Specify non-null value for in match operator");
            }

            return new StringMatch(null, null, inList);
        }

        public Api.Cloud.V1.Store.StringMatch ToStringMatch()
        {
            if (OneOf == Api.Cloud.V1.Store.StringMatch.MatchOneofCase.Contains)
            {
                return new Api.Cloud.V1.Store.StringMatch
                {
                    Contains = Contains_
                };
            }
            else if (OneOf == Api.Cloud.V1.Store.StringMatch.MatchOneofCase.Equals_)
            {
                return new Api.Cloud.V1.Store.StringMatch
                {
                    Equals_ = Equals_
                };
            }

            return new Api.Cloud.V1.Store.StringMatch
            {
                In = In_.ToInList()
            };
        }
        
        public static class Types
        {
            public sealed class InList
            {
                private List<string> Values { get; set; }

                private InList()
                {
                    Values = new List<string>();
                }

                public InList WithValues(params string[] values)
                {
                    Values.AddRange(values);
                    return this;
                }

                public static InList NewInstance()
                {
                    return new InList();
                }

                public Api.Cloud.V1.Store.StringMatch.Types.InList ToInList()
                {
                    return new Api.Cloud.V1.Store.StringMatch.Types.InList()
                    {
                        Values = { Values }
                    };
                }
            }
        }
    }
}
