// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class DeleteSchemaRequestTest
{
    [Test]
    public void TestNewInstance()
    {
        var request = DeleteSchemaRequest.NewInstance("leave_request.yaml").ToDeleteSchemaRequest();
        Assert.That(request.Id, Has.Member("leave_request.yaml"));
    }
}
