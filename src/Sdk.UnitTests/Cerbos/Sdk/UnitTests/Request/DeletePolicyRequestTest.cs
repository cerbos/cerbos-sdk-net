// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class DeletePolicyRequestTest
{
    [Test]
    public void TestWithId()
    {
        var request = DeletePolicyRequest.NewInstance().WithId("leave_request.yaml").ToDeletePolicyRequest();
        Assert.That(request.Id, Has.Member("leave_request.yaml"));
    }
}
