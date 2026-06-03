// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Request
{
    public sealed class DeletePolicyRequest
    {
        private List<string> Id { get; }

        private DeletePolicyRequest()
        {
            Id = new List<string>();
        }

        public static DeletePolicyRequest NewInstance()
        {
            return new DeletePolicyRequest();
        }

        public DeletePolicyRequest WithId(params string[] id)
        {
            Id.AddRange(id);
            return this;
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