// Copyright 2021-2023 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Cerbos.Api.V1.Effect;
using Google.Protobuf.Collections;

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

        public CheckResult(MapField<string, Effect> effects)
        {
            var effectsDictionary = new Dictionary<string, Effect>();
            foreach (var effect in effects)
            {
                effectsDictionary.Add(effect.Key, effect.Value);
            }
            _effects = effectsDictionary;
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