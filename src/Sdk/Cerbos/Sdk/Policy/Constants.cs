// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class Constants
    {
        private Api.V1.Policy.Constants C { get; }

        public Api.V1.Policy.Constants Raw => C;

        public List<string> Import => C.Import.ToList();

        public Dictionary<string, Google.Protobuf.WellKnownTypes.Value> Local => C.Local.ToDictionary(X => X.Key, X => X.Value);

        public Constants(Api.V1.Policy.Constants constants)
        {
            C = constants;
        }
    }
}