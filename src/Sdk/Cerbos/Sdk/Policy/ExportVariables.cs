// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class ExportVariables
    {
        private Api.V1.Policy.ExportVariables EV { get; }

        public Api.V1.Policy.ExportVariables Raw => EV;

        public string Name => EV.Name;

        public Dictionary<string, string> Definitions => EV.Definitions.ToDictionary(X => X.Key, X => X.Value);

        public ExportVariables(Api.V1.Policy.ExportVariables exportVariables)
        {
            EV = exportVariables;
        }
    }
}