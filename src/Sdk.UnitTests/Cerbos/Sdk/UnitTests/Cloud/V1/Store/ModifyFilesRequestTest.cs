// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Text;
using Cerbos.Sdk.Cloud.V1.Store;
using NUnit.Framework;
using static Cerbos.Sdk.Cloud.V1.Store.ChangeDetails.Types;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

public class ModifyFilesRequestTest
{
    private const string StoreId = "MD1LAP5BJNA9";
    private const string File = "policy.yaml";
    private const string Content = "content";
    private const string Description = "description";
    private const string Name = "name";
    private const string Source = "source";
    private const string MetadataKeyAndValue1 = "keyAndValue1";
    private const string MetadataKeyAndValue2 = "keyAndValue2";


    [Test]
    public void ModifyFilesRequest()
    {
        var condition = Sdk.Cloud.V1.Store.ModifyFilesRequest.Types.Condition.NewInstance().
            WithStoreVersionMustEqual(1);

        var file = Sdk.Cloud.V1.Store.File.NewInstance().
            WithContents(Encoding.UTF8.GetBytes(Content)).
            WithPath(File);

        var fileOp = Sdk.Cloud.V1.Store.FileOp.
            NewInstance().
            OpAddOrUpdate(file);

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

        var request = Sdk.Cloud.V1.Store.ModifyFilesRequest.NewInstance().
            WithStoreId(StoreId).
            WithCondition(condition).
            WithOperations(fileOp).
            WithChangeDetails(changeDetails).
            ToModifyFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
        Assert.That(request.Condition, Is.EqualTo(condition.ToCondition()));
        Assert.That(request.Operations[0], Is.EqualTo(fileOp.ToFileOp()));
        Assert.That(request.ChangeDetails, Is.EqualTo(changeDetails.ToChangeDetails()));
    }

    [Test]
    public void Optional()
    {
        var file = Sdk.Cloud.V1.Store.File.NewInstance().
            WithContents(Encoding.UTF8.GetBytes(Content)).
            WithPath(File);

        var fileOp = FileOp.
            NewInstance().
            OpAddOrUpdate(file);

        var request = Sdk.Cloud.V1.Store.ModifyFilesRequest.NewInstance().
            WithStoreId(StoreId).
            WithOperations(fileOp).
            ToModifyFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
    }
}