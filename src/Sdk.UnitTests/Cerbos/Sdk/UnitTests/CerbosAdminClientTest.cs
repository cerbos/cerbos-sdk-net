// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests
{
    [TestFixture]
    public class CerbosAdminClientTest : CerbosTest
    {
        [Test]
        public void ListPolicies()
        {
            var request = ListPoliciesRequest.NewInstance()
                .WithIncludeDisabled(true)
                .WithPolicyId("resource_policies/policy_01.yaml", "resource_policies/policy_02.yaml");

            var have = _clientAdmin.ListPolicies(request, _metadata).PolicyIds;
            Assert.That(have, Is.EqualTo(new List<string> { "resource_policies/policy_01.yaml", "resource_policies/policy_02.yaml" }));
        }
    }
}
