// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class EnablePolicyRequestTest
{
    [Test]
    public void TestWithId()
    {
        var request = EnablePolicyRequest.NewInstance().WithId("leave_request.yaml").ToEnablePolicyRequest();
        Assert.That(request.Id, Has.Member("leave_request.yaml"));
    }
}
