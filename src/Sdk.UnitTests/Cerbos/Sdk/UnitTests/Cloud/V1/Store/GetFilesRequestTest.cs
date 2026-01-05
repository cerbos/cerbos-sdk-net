// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

public class GetFilesRequestTest
{
    private const string StoreId = "MD1LAP5BJNA9";
    private const string File = "policy.yaml";
    private const string File1 = "policy1.yaml";

    [Test]
    public void NewInstance()
    {
        var request = Sdk.Cloud.V1.Store.GetFilesRequest.NewInstance(StoreId, [File, File1]).ToGetFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
        Assert.That(request.Files.ToList(), Is.EqualTo(new List<string>() { File, File1 }));
    }
}