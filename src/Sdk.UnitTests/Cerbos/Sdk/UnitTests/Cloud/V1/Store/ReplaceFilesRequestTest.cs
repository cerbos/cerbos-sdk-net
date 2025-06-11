// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Cloud.V1.Store;
using NUnit.Framework;
using static Cerbos.Sdk.Cloud.V1.Store.ChangeDetails.Types;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

public class ReplaceFilesRequestTest
{
    private const string StoreId = "MD1LAP5BJNA9";
    private const string Description = "description";
    private const string Name = "name";
    private const string Source = "source";
    private const string MetadataKeyAndValue1 = "keyAndValue1";
    private const string MetadataKeyAndValue2 = "keyAndValue2";
    private const string PathToZippedContents = "./../../../res/cloud/v1/store.zip";

    [Test]
    public void ReplaceFilesRequest()
    {
        var condition = Sdk.Cloud.V1.Store.ReplaceFilesRequest.Types.Condition.NewInstance().
            WithStoreVersionMustEqual(1);

        var uploader = Uploader.NewInstance().
            WithName(Name);

        var internal_ = ChangeDetails.Types.Internal.NewInstance().
            WithSource(Source).
            WithMetadata(MetadataKeyAndValue1, MetadataValue.StringValue(MetadataKeyAndValue1)).
            WithMetadata(MetadataKeyAndValue2, MetadataValue.StringValue(MetadataKeyAndValue2));

        var changeDetails = ChangeDetails.NewInstance().
            WithDescription(Description).
            WithUploader(uploader).
            OriginInternal(internal_);

        var zippedContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToZippedContents));
        var request = Sdk.Cloud.V1.Store.ReplaceFilesRequest.NewInstance().
            WithStoreId(StoreId).
            WithCondition(condition).
            WithZippedContents(zippedContents).
            WithChangeDetails(changeDetails).
            ToReplaceFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
        Assert.That(request.Condition, Is.EqualTo(condition.ToCondition()));
        Assert.That(request.ZippedContents, Is.EqualTo(zippedContents));
        Assert.That(request.ChangeDetails, Is.EqualTo(changeDetails.ToChangeDetails()));
    }

    [Test]
    public void Optional()
    {
        var zippedContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToZippedContents));
        var request = Sdk.Cloud.V1.Store.ReplaceFilesRequest.NewInstance().
            WithStoreId(StoreId).
            WithZippedContents(zippedContents).
            ToReplaceFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
    }
}