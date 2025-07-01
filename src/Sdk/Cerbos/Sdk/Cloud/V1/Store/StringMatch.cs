// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class StringMatch
    {
        private Api.Cloud.V1.Store.StringMatch.MatchOneofCase OneOf = Api.Cloud.V1.Store.StringMatch.MatchOneofCase.None;
        private string Contains { get; set; }
        private string Equals_ { get; set; }
        private Types.InList In { get; set; }

        private StringMatch() {}

        public static StringMatch NewInstance()
        {
            return new StringMatch();
        }

        public StringMatch WithContains(string contains)
        {
            Contains = contains;
            OneOf = Api.Cloud.V1.Store.StringMatch.MatchOneofCase.Contains;
            return this;
        }

        public StringMatch WithEquals(string equals)
        {
            Equals_ = equals;
            OneOf = Api.Cloud.V1.Store.StringMatch.MatchOneofCase.Equals_;
            return this;
        }

        public StringMatch WithIn(Types.InList inList)
        {
            In = inList;
            OneOf = Api.Cloud.V1.Store.StringMatch.MatchOneofCase.In;
            return this;
        }

        public Api.Cloud.V1.Store.StringMatch ToStringMatch()
        {
            if (OneOf == Api.Cloud.V1.Store.StringMatch.MatchOneofCase.Equals_)
            {
                return new Api.Cloud.V1.Store.StringMatch
                {
                    Equals_ = Equals_
                };
            }
            else if (OneOf == Api.Cloud.V1.Store.StringMatch.MatchOneofCase.In)
            {
                if (In == null)
                {
                    throw new Exception("Specify non-null value for in match operator");
                }

                return new Api.Cloud.V1.Store.StringMatch
                {
                    In = In.ToInList()
                };
            }
            else if (OneOf == Api.Cloud.V1.Store.StringMatch.MatchOneofCase.Contains)
            {
                return new Api.Cloud.V1.Store.StringMatch
                {
                    Contains = Contains
                };
            }
            else
            {
                throw new Exception("Either contains, equals or in match operator must be specified");
            }
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
