// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using Cerbos.Sdk.Engine;

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
                private Api.V1.Response.CheckResourcesResponse.Types.ResultEntry RE { get; }

                public Dictionary<string, Api.V1.Effect.Effect> Actions => RE.Actions.ToDictionary(
                        x => x.Key,
                        x => x.Value
                    );
                public Types.Meta Meta => new Types.Meta(RE.Meta);
                public Api.V1.Response.CheckResourcesResponse.Types.ResultEntry Raw => RE;
                public Api.V1.Response.CheckResourcesResponse.Types.ResultEntry.Types.Resource Resource => RE.Resource;
                public List<OutputEntry> Outputs => RE.Outputs.Select(X => new OutputEntry(X)).ToList();
                public List<Api.V1.Schema.ValidationError> ValidationErrors => RE.ValidationErrors.ToList();

                public ResultEntry(Api.V1.Response.CheckResourcesResponse.Types.ResultEntry resultEntry)
                {
                    RE = resultEntry;
                }

                public bool IsAllowed(string action)
                {
                    var ok = RE.Actions.TryGetValue(action, out var effect);
                    if (!ok || effect == Api.V1.Effect.Effect.Deny || effect == Api.V1.Effect.Effect.Unspecified)
                    {
                        return false;
                    }

                    return true;
                }

                public OutputEntry Output(string src)
                {
                    foreach (var output in Outputs)
                    {
                        if (output.Src == src)
                        {
                            OutputEntryEvaluationException.FromOutputEntry(output);
                            return output;
                        }
                    }

                    throw OutputEntryNotFoundException.Src(src);
                }

                public OutputEntry OutputByAction(string action)
                {
                    foreach (var output in Outputs)
                    {
                        if (output.Action == action)
                        {
                            OutputEntryEvaluationException.FromOutputEntry(output);
                            return output;
                        }
                    }

                    throw OutputEntryNotFoundException.Action(action);
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
                }
            }
        }
    }
}
