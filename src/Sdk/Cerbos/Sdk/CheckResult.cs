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
            foreach (var (k, v) in effects)
            {
                effectsDictionary.Add(k, v);
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
            foreach(var (k, v) in _effects)
            {
                all[k] = v.Equals(Effect.Allow);
            }
            return all;
        }
    }
}