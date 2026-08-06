// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace Cerbos.Sdk.Builder
{
    public sealed class AuxData
    {
        private Api.V1.Request.AuxData A { get; }

        private AuxData()
        {
            A = new Api.V1.Request.AuxData();
        }

        public static AuxData WithJwt(string token)
        {
            return new AuxData()
            {
                A = {
                    Jwt = Types.JWT.FromToken(token).ToJWT(),
                }
            };
        }

        public static AuxData WithJwt(string token, string keySetId)
        {
            return new AuxData()
            {
                A = {
                    Jwt = Types.JWT.NewInstance(token, keySetId).ToJWT(),
                }
            };
        }

        public static AuxData WithJwt(Types.JWT jwt)
        {
            return new AuxData()
            {
                A = {
                    Jwt = jwt.ToJWT(),
                }
            };
        }

        public static AuxData WithJwts(Dictionary<string, Types.JWT> jwts)
        {
            if (jwts == null || jwts.Count == 0)
            {
                throw new ArgumentException("There must be at least one JWT in the dictionary");
            }

            var tmp = new Google.Protobuf.Collections.MapField<string, Api.V1.Request.AuxData.Types.JWT>();
            foreach (KeyValuePair<string, Types.JWT> kvp in jwts)
            {
                tmp.Add(kvp.Key, kvp.Value.ToJWT());
            }

            return new AuxData()
            {
                A = {
                    Jwts = { tmp }
                }
            };
        }

        public Api.V1.Request.AuxData ToAuxData()
        {
            return A;
        }

        public static class Types
        {
            public sealed class JWT
            {
                private string Token { get; set; }

                private string KeySetId { get; set; }

                private JWT() { }

                private JWT(string token)
                {
                    Token = token;
                }

                private JWT(string token, string keySetId)
                {
                    Token = token;
                    KeySetId = keySetId;
                }

                public static JWT NewInstance(string token, string keySetId)
                {
                    return new JWT(token, keySetId);
                }

                public static JWT FromToken(string token)
                {
                    return new JWT(token);
                }

                public Api.V1.Request.AuxData.Types.JWT ToJWT()
                {
                    if (string.IsNullOrEmpty(KeySetId))
                    {
                        return new Api.V1.Request.AuxData.Types.JWT()
                        {
                            Token = Token,
                        };
                    }

                    return new Api.V1.Request.AuxData.Types.JWT()
                    {
                        Token = Token,
                        KeySetId = KeySetId,
                    };
                }
            }
        }
    }
}