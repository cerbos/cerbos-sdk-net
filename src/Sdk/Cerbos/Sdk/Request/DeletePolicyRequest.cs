// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Request
{
    public sealed class DeletePolicyRequest
    {
        private List<string> Id { get; }

        private DeletePolicyRequest(params string[] id)
        {
            Id = new List<string>(id);
        }

        public static DeletePolicyRequest NewInstance(params string[] id)
        {
            return new DeletePolicyRequest(id);
        }

        public Api.V1.Request.DeletePolicyRequest ToDeletePolicyRequest()
        {
            var request = new Api.V1.Request.DeletePolicyRequest();
            if (Id.Count > 0)
            {
                request.Id.AddRange(Id);
            }

            return request;
        }
    }
}