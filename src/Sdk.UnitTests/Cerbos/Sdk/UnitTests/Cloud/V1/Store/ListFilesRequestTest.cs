// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

public class ListFilesRequestTest
{
    private const string StoreId = "MD1LAP5BJNA9";
    private const string File = "policy.yaml";

    [Test]
    public void NewInstance()
    {
        var request = Sdk.Cloud.V1.Store.ListFilesRequest.NewInstance(StoreId).ToListFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
    }

    [Test]
    public void WithFilter()
    {
        var fileFilter = Sdk.Cloud.V1.Store.FileFilter.PathEquals(File);

        var request = Sdk.Cloud.V1.Store.ListFilesRequest.WithFilter(StoreId, fileFilter).ToListFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
        Assert.That(request.Filter.Path.Equals_, Is.EqualTo(File));
    }
}