// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class PurgeStoreRevisionsRequestTest
{
    [Test]
    public void TestNewInstance()
    {
        var request = PurgeStoreRevisionsRequest.NewInstance(1)
            .ToPurgeStoreRevisionsRequest();

        Assert.That(request.KeepLast, Is.EqualTo(1));
    }
}
