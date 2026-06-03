// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Request
{
    public sealed class DisablePolicyRequest
    {
        private List<string> Id { get; }

        private DisablePolicyRequest(params string[] id)
        {
            Id = new List<string>(id);
        }

        public static DisablePolicyRequest NewInstance(params string[] id)
        {
            return new DisablePolicyRequest(id);
        }

        public Api.V1.Request.DisablePolicyRequest ToDisablePolicyRequest()
        {
            var request = new Api.V1.Request.DisablePolicyRequest();
            if (Id.Count > 0)
            {
                request.Id.AddRange(Id);
            }

            return request;
        }
    }
}