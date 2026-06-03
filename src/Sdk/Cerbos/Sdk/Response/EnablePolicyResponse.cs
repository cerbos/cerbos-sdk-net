// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Response
{
    public sealed class EnablePolicyResponse
    {
        private Api.V1.Response.EnablePolicyResponse R { get; }

        public uint EnabledPolicies => R.EnabledPolicies;
        public Api.V1.Response.EnablePolicyResponse Raw => R;

        public EnablePolicyResponse(Api.V1.Response.EnablePolicyResponse response)
        {
            R = response;
        }
    }
}