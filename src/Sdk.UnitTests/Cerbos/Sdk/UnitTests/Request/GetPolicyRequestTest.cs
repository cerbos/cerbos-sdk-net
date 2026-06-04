// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class GetPolicyRequestTest
{
    [Test]
    public void TestNewInstance()
    {
        var request = GetPolicyRequest.NewInstance("leave_request.yaml").ToGetPolicyRequest();
        Assert.That(request.Id, Has.Member("leave_request.yaml"));
    }
}
