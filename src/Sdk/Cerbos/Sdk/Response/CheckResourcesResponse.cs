// Copyright 2021-2023 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using Cerbos.Api.V1.Engine;
using Google.Protobuf.WellKnownTypes;

namespace Cerbos.Sdk.Response
{
    public sealed class CheckResourcesResponse
    {
        private Api.V1.Response.CheckResourcesResponse R { get; }

        public string RequestId => R.RequestId;
        public Api.V1.Response.CheckResourcesResponse Raw => R;

        public CheckResourcesResponse(Api.V1.Response.CheckResourcesResponse response)
        {
            R = response;
        }

        public Types.ResultEntry Find(string id)
        {
            foreach (var result in R.Results)
            {
                if (result.Resource.Id.Equals(id))
                {
                    return new Types.ResultEntry(result);
                }
            }

            throw new Exception($"Failed to find result entry with id {id}");
        }

        public static class Types
        {
            public sealed class ResultEntry
            {
                private Api.V1.Response.CheckResourcesResponse.Types.ResultEntry Re { get; }

                public Dictionary<string, Api.V1.Effect.Effect> Actions => Re.Actions.ToDictionary(
                        x => x.Key,
                        x => x.Value
                    );
                public Types.Meta Meta => new Types.Meta(Re.Meta);
                public Api.V1.Response.CheckResourcesResponse.Types.ResultEntry Raw => Re;
                public Api.V1.Response.CheckResourcesResponse.Types.ResultEntry.Types.Resource Resource => Re.Resource;
                public Types.Outputs Outputs => new Types.Outputs(Re.Outputs.ToList());
                public List<Api.V1.Schema.ValidationError> ValidationErrors => Re.ValidationErrors.ToList();
                
                public ResultEntry(Api.V1.Response.CheckResourcesResponse.Types.ResultEntry resultEntry)
                {
                    Re = resultEntry;
                }

                public bool IsAllowed(string action)
                {
                    var ok = Re.Actions.TryGetValue(action, out var effect);
                    if (!ok || effect == Cerbos.Api.V1.Effect.Effect.Deny || effect == Cerbos.Api.V1.Effect.Effect.Unspecified) {
                        return false;
                    }

                    return true;
                }

                public static class Types
                {
                    public sealed class Meta
                    {
                        private Api.V1.Response.CheckResourcesResponse.Types.ResultEntry.Types.Meta M { get; }

                        public Dictionary<string, Api.V1.Response.CheckResourcesResponse.Types.ResultEntry.Types.Meta.
                            Types.EffectMeta> Actions => M.Actions.ToDictionary(
                                x => x.Key,
                                x => x.Value
                            );
                        public List<string> EffectiveDerivedRoles => M.EffectiveDerivedRoles.ToList();
                        public Api.V1.Response.CheckResourcesResponse.Types.ResultEntry.Types.Meta Raw => M;

                        public Meta(Api.V1.Response.CheckResourcesResponse.Types.ResultEntry.Types.Meta meta)
                        {
                            M = meta;
                        }
                    }
                    
                    public sealed class Outputs
                    {
                        private List<OutputEntry> O { get; }
            
                        public List<OutputEntry> Raw => O;

                        internal Outputs(List<OutputEntry> outputs) {
                            O = outputs;
                        }

                        public Value Get(string src)
                        {
                            return O.Find(outputEntry => outputEntry.Src == src)?.Val;
                        }

                        public Dictionary<string, Value> ToDictionary()
                        {
                            return O
                                .ToDictionary(
                                    x => x.Src,
                                    x => x.Val
                                );
                        }
                    }
                }
            }
    }
}

}