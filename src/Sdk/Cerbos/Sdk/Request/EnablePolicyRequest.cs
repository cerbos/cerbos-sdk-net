// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace Cerbos.Sdk.Request
{
    public sealed class EnablePolicyRequest
    {
        private List<string> Id { get; }

        private EnablePolicyRequest(params string[] id)
        {
            Id = new List<string>(id);
        }

        public static EnablePolicyRequest NewInstance(params string[] id)
        {
            return new EnablePolicyRequest(id);
        }

        public Api.V1.Request.EnablePolicyRequest ToEnablePolicyRequest()
        {
            var request = new Api.V1.Request.EnablePolicyRequest();
            if (Id.Count > 0)
            {
                request.Id.AddRange(Id);
            }

            return request;
        }
    }
}