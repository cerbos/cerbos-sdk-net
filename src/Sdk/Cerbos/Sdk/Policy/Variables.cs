// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Policy
{
    public sealed class Variables
    {
        private Api.V1.Policy.Variables V { get; }

        public Api.V1.Policy.Variables Raw => V;

        public List<string> Import => V.Import.ToList();

        public Dictionary<string, string> Local => V.Local.ToDictionary(x => x.Key, x => x.Value);

        public Variables(Api.V1.Policy.Variables variables)
        {
            V = variables;
        }
    }
}