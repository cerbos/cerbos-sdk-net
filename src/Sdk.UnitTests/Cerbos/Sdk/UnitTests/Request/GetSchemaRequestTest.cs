// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class GetSchemaRequestTest
{
    [Test]
    public void TestNewInstance()
    {
        var request = GetSchemaRequest.NewInstance("principal.json").ToGetSchemaRequest();
        Assert.That(request.Id, Has.Member("principal.json"));
    }
}
