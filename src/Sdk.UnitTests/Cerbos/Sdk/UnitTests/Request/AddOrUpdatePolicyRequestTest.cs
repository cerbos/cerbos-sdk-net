// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class AddOrUpdatePolicyRequestTest
{
    private readonly string policy = """{"apiVersion":"api.cerbos.dev/v1","resourcePolicy":{"version":"default","resource":"add_or_update_test_policy"}}""";

    [Test]
    public void TestWithJson()
    {
        var request = AddOrUpdatePolicyRequest.NewInstance()
            .WithJson(policy)
            .ToAddOrUpdatePolicyRequest();

        Assert.That(request.Policies[0].ResourcePolicy.Resource, Is.EqualTo("add_or_update_test_policy"));
    }
}
