// Copyright 2021-2023 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cerbos.Api.V1.Effect;
using Cerbos.Api.V1.Engine;
using Cerbos.Api.V1.Response;
using Cerbos.Api.V1.Schema;
using Google.Protobuf.WellKnownTypes;

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
        private readonly CheckResourcesResponse.Types.ResultEntry _result;
        public CheckResourcesResponse.Types.ResultEntry.Types.Resource Resource => _result.Resource;
        public Types.Meta Meta => new Types.Meta(_result.Meta);
        public Types.Outputs Outputs => new Types.Outputs(_result.Outputs.ToList());
        public List<ValidationError> ValidationErrors => _result.ValidationErrors.ToList();

        internal CheckResult(CheckResourcesResponse.Types.ResultEntry result)
        {
            _result = result;
        }

        /// <summary>
        /// IsAllowed returns whether the given <paramref name="action"/> is allowed or denied.
        /// </summary>
        public bool IsAllowed(string action)
        {
            if (_result == null)
            {
                return false;
            }

            return _result.Actions.ToDictionary(
                x => x.Key,
                x => x.Value
            ).TryGetValue(action, out var effect) && effect == Effect.Allow;
        }

        public Dictionary<string, bool> GetAll()
        {
            return _result.Actions.ToDictionary(
                x => x.Key,
                x => x.Value == Effect.Allow
            );
        }

        public Types.Meta GetMeta()
        {
            return new Types.Meta(_result.Meta);
        }
        
        public Types.Outputs GetOutputs()
        {
            return new Types.Outputs(_result.Outputs.ToList());
        }

        public List<ValidationError> GetValidationErrors()
        {
            return _result.ValidationErrors.ToList();
        }

        public CheckResourcesResponse.Types.ResultEntry GetRaw()
        {
            return _result;
        }

        public static class Types 
        {
            public sealed class Meta
            {
                private readonly CheckResourcesResponse.Types.ResultEntry.Types.Meta _meta;
                public Dictionary<string, CheckResourcesResponse.Types.ResultEntry.Types.Meta.Types.EffectMeta> Actions => GetActions();
                public List<string> EffectiveDerivedRoles => GetEffectiveDerivedRoles();

                internal Meta(CheckResourcesResponse.Types.ResultEntry.Types.Meta meta)
                {
                    _meta = meta;
                }
            
                public List<string> GetEffectiveDerivedRoles() {
                    return _meta.EffectiveDerivedRoles.ToList();
                }
            
                public Dictionary<string, CheckResourcesResponse.Types.ResultEntry.Types.Meta.Types.EffectMeta> GetActions() {
                    return _meta.Actions.ToDictionary(
                        x => x.Key,
                        x => x.Value
                    );
                }

                public CheckResourcesResponse.Types.ResultEntry.Types.Meta GetRaw()
                {
                    return _meta;
                }
            }
        
            public sealed class Outputs
            {
                private readonly List<OutputEntry> _outputs;
            
                internal Outputs(List<OutputEntry> outputs) {
                    _outputs = outputs;
                }

                public Value Get(string src)
                {
                    return _outputs.Find(outputEntry => outputEntry.Src == src)?.Val;
                }

                public Dictionary<string, Value> ToDictionary()
                {
                    return _outputs
                        .ToDictionary(
                            x => x.Src,
                            x => x.Val
                        );
                }

                public List<OutputEntry> GetRaw()
                {
                    return _outputs;
                }
            }
        }
    }
}