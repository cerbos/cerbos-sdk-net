// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class DisablePolicyRequestTest
{
    [Test]
    public void TestNewInstance()
    {
        var request = DisablePolicyRequest.NewInstance("leave_request.yaml").ToDisablePolicyRequest();
        Assert.That(request.Id, Has.Member("leave_request.yaml"));
    }
}
