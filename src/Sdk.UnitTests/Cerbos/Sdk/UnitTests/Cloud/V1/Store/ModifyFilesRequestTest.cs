// Copyright 2021-2026 Zenauth Ltd.
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
    public void NewInstance()
    {
        var condition = Sdk.Cloud.V1.Store.ModifyFilesRequest.Types.Condition.NewInstance(1);

        var contentBytes = Encoding.UTF8.GetBytes(Content);
        var file = Sdk.Cloud.V1.Store.File.NewInstance(File, contentBytes);
        var fileOp = Sdk.Cloud.V1.Store.FileOp.AddOrUpdate(file);

        var uploader = Uploader.NewInstance(Name);

        var internal_ = ChangeDetails.Types.Internal.NewInstance(Source).
            WithMetadata(MetadataKeyAndValue1, MetadataValue.StringValue(MetadataKeyAndValue1)).
            WithMetadata(MetadataKeyAndValue2, MetadataValue.StringValue(MetadataKeyAndValue2));

        var changeDetails = ChangeDetails.Internal(Description, uploader, internal_);

        var request = Sdk.Cloud.V1.Store.ModifyFilesRequest.NewInstance(StoreId, condition, changeDetails, fileOp).ToModifyFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
        Assert.That(request.Condition, Is.EqualTo(condition.ToCondition()));
        Assert.That(request.Operations[0], Is.EqualTo(fileOp.ToFileOp()));
        Assert.That(request.ChangeDetails, Is.EqualTo(changeDetails.ToChangeDetails()));
    }

    [Test]
    public void WithCondition()
    {
        var condition = Sdk.Cloud.V1.Store.ModifyFilesRequest.Types.Condition.NewInstance(1);

        var contentBytes = Encoding.UTF8.GetBytes(Content);
        var file = Sdk.Cloud.V1.Store.File.NewInstance(File, contentBytes);
        var fileOp = Sdk.Cloud.V1.Store.FileOp.AddOrUpdate(file);

        var request = Sdk.Cloud.V1.Store.ModifyFilesRequest.WithCondition(StoreId, condition, fileOp).ToModifyFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
        Assert.That(request.Condition, Is.EqualTo(condition.ToCondition()));
        Assert.That(request.Operations[0], Is.EqualTo(fileOp.ToFileOp()));
    }

    [Test]
    public void WithChangeDetails()
    {
        var contentBytes = Encoding.UTF8.GetBytes(Content);
        var file = Sdk.Cloud.V1.Store.File.NewInstance(File, contentBytes);
        var fileOp = Sdk.Cloud.V1.Store.FileOp.AddOrUpdate(file);

        var uploader = Uploader.NewInstance(Name);

        var internal_ = ChangeDetails.Types.Internal.NewInstance(Source).
            WithMetadata(MetadataKeyAndValue1, MetadataValue.StringValue(MetadataKeyAndValue1)).
            WithMetadata(MetadataKeyAndValue2, MetadataValue.StringValue(MetadataKeyAndValue2));

        var changeDetails = ChangeDetails.Internal(Description, uploader, internal_);

        var request = Sdk.Cloud.V1.Store.ModifyFilesRequest.WithChangeDetails(StoreId, changeDetails, fileOp).ToModifyFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
        Assert.That(request.Operations[0], Is.EqualTo(fileOp.ToFileOp()));
        Assert.That(request.ChangeDetails, Is.EqualTo(changeDetails.ToChangeDetails()));
    }
}