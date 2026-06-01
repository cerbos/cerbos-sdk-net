// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Builder;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builder;

public class PlanResourcesRequestTest
{
    [Test]
    public void TestWithAllowPartialRequests()
    {
        Assert.That((Func<Api.V1.Request.PlanResourcesRequest>)(() => PlanResourcesRequest.NewInstance().ToPlanResourcesRequest()), Throws.Exception);
        Assert.That((Func<Api.V1.Request.PlanResourcesRequest>)(() => PlanResourcesRequest.NewInstance().WithAllowPartialRequests(true).ToPlanResourcesRequest()), Throws.Nothing);
    }
}
