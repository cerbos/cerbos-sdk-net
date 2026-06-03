// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Request
{
    public sealed class GetPolicyRequest
    {
        private List<string> Id { get; }

        private GetPolicyRequest(params string[] id)
        {
            Id = new List<string>(id);
        }

        public static GetPolicyRequest NewInstance(params string[] id)
        {
            return new GetPolicyRequest(id);
        }

        public Api.V1.Request.GetPolicyRequest ToGetPolicyRequest()
        {
            var request = new Api.V1.Request.GetPolicyRequest();
            if (Id.Count > 0)
            {
                request.Id.AddRange(Id);
            }

            return request;
        }
    }
}