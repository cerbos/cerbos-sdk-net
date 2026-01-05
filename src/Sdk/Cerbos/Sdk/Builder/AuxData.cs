// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Builder
{
    public sealed class AuxData
    {
        private Api.V1.Request.AuxData A { get; }

        private AuxData()
        {
            A = new Api.V1.Request.AuxData
            {
                Jwt = new Api.V1.Request.AuxData.Types.JWT()
            };
        }

        public static AuxData WithJwt(string token)
        {
            AuxData auxData = new AuxData();
            auxData.A.Jwt.Token = token;
            return auxData;
        }

        public static AuxData WithJwt(string token, string keySetId)
        {
            AuxData auxData = new AuxData();
            auxData.A.Jwt.Token = token;
            auxData.A.Jwt.KeySetId = keySetId;
            return auxData;
        }

        public Api.V1.Request.AuxData ToAuxData()
        {
            return A;
        }
    }
}