// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class ExportConstants
    {
        private Api.V1.Policy.ExportConstants EC { get; }

        public Api.V1.Policy.ExportConstants Raw => EC;

        public string Name => EC.Name;

        public Dictionary<string, Google.Protobuf.WellKnownTypes.Value> Definitions => EC.Definitions.ToDictionary(X => X.Key, X => X.Value);

        public ExportConstants(Api.V1.Policy.ExportConstants exportConstants)
        {
            EC = exportConstants;
        }
    }
}