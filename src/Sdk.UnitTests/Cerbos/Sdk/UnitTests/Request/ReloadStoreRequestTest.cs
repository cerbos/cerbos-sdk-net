// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class ReloadStoreRequestTest
{
    [Test]
    public void TestNewInstance()
    {
        var request = ReloadStoreRequest.NewInstance(true)
            .ToReloadStoreRequest();

        Assert.That(request.Wait, Is.EqualTo(true));
    }
}
