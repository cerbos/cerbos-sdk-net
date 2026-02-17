// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Builder;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builder;

public class CheckResourcesRequestTest
{
    [Test]
    public void TestWithAllowPartialRequests()
    {
        Assert.That(() => CheckResourcesRequest.NewInstance().ToCheckResourcesRequest(), Throws.Exception);
        Assert.That(() => CheckResourcesRequest.NewInstance().WithAllowPartialRequests(true).ToCheckResourcesRequest(), Throws.Nothing);
    }
}
