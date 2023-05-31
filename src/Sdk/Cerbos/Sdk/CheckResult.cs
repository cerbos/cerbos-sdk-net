// Copyright 2021-2023 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Cerbos.Api.V1.Effect;
using Cerbos.Api.V1.Engine;
using Cerbos.Api.V1.Response;

namespace Cerbos.Sdk
{
    /// <summary>
    /// CheckResult provides an interface to see whether some actions are allowed on a resource.
    /// </summary>
    /// <remarks>
    /// <see cref="Cerbos.Sdk.CheckResult"/> only covers the results where only a single resource with one or more actions exists.
    /// Please see <see cref="Cerbos.Sdk.CheckResourcesResult"/> for multiple resources and one or more actions.
    /// </remarks>
    public class CheckResult
    {
        private readonly Dictionary<string, Effect> _effects;
        public CheckResourcesResponse.Types.ResultEntry.Types.Resource Resource { get; }
        public CheckResourcesResponse.Types.ResultEntry.Types.Meta Meta { get; }
        public List<OutputEntry> Outputs { get; }

        public CheckResult(CheckResourcesResponse.Types.ResultEntry result)
        {
            var effectsDictionary = new Dictionary<string, Effect>();
            foreach (var kvp in result.Actions)
            {
                effectsDictionary.Add(kvp.Key, kvp.Value);
            }
            _effects = effectsDictionary;

            Meta = result.Meta;
            Resource = result.Resource;
            
            var outputList = new List<OutputEntry>();
            foreach (var output in result.Outputs)
            {
                outputList.Add(output);
            }
            Outputs = outputList;
        }

        public CheckResult(Dictionary<string, Effect> effects) {
            _effects = effects;
        }

        /// <summary>
        /// IsAllowed returns whether the given <paramref name="action"/> is allowed or denied.
        /// </summary>
        public bool IsAllowed(string action)
        {
            return _effects[action].Equals(Effect.Allow);
        }

        public Dictionary<string, bool> GetAll()
        {
            var all = new Dictionary<string, bool>();
            foreach(var effect in _effects)
            {
                all[effect.Key] = effect.Value.Equals(Effect.Allow);
            }
            return all;
        }
    }
}