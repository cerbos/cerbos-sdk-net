// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class ListSchemasRequestTest
{
    [Test]
    public void TestNewInstance()
    {
        Assert.That((Func<Api.V1.Request.ListSchemasRequest>)(() => ListSchemasRequest.NewInstance().ToListSchemasRequest()), Throws.Nothing);
    }
}
