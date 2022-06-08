// Copyright 2021-2022 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Cerbos.Api.V1.Effect;
using Google.Protobuf.Collections;

namespace Cerbos.Sdk
{
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