// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Reflection.Metadata;
using System.Text;
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
    private const string PathToPolicy = "/path/to/policy.yaml";
    private const string Content = "content";

    [Test]
    public void WithZippedContents()
    {
        var condition = ReplaceFilesRequest.Types.Condition.NewInstance(1);

        var uploader = Uploader.NewInstance(Name);

        var internal_ = Internal.NewInstance(Source).
            WithMetadata(MetadataKeyAndValue1, MetadataValue.StringValue(MetadataKeyAndValue1)).
            WithMetadata(MetadataKeyAndValue2, MetadataValue.StringValue(MetadataKeyAndValue2));

        var changeDetails = ChangeDetails.Internal(Description, uploader, internal_);

        var zippedContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToZippedContents));
        var request = ReplaceFilesRequest.WithZippedContents(StoreId, zippedContents, condition, changeDetails).
            ToReplaceFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
        Assert.That(request.Condition, Is.EqualTo(condition.ToCondition()));
        Assert.That(request.ZippedContents, Is.EqualTo(zippedContents));
        Assert.That(request.ChangeDetails, Is.EqualTo(changeDetails.ToChangeDetails()));
    }

        [Test]
    public void WithFiles()
    {
        var condition = ReplaceFilesRequest.Types.Condition.NewInstance(1);

        var uploader = Uploader.NewInstance(Name);

        var internal_ = Internal.NewInstance(Source).
            WithMetadata(MetadataKeyAndValue1, MetadataValue.StringValue(MetadataKeyAndValue1)).
            WithMetadata(MetadataKeyAndValue2, MetadataValue.StringValue(MetadataKeyAndValue2));

        var changeDetails = ChangeDetails.Internal(Description, uploader, internal_);

        var contentBytes = Encoding.UTF8.GetBytes(Content);

        var files = ReplaceFilesRequest.Types.Files.NewInstance(
            Sdk.Cloud.V1.Store.File.NewInstance(PathToPolicy, contentBytes)
        );

        var request = ReplaceFilesRequest.WithFiles(StoreId, files, condition, changeDetails).
            ToReplaceFilesRequest();

        Assert.That(request.StoreId, Is.EqualTo(StoreId));
        Assert.That(request.Condition, Is.EqualTo(condition.ToCondition()));
        Assert.That(request.Files.Files_[0], Is.EqualTo(files.ToFiles().Files_[0]));
        Assert.That(request.ChangeDetails, Is.EqualTo(changeDetails.ToChangeDetails()));
    }
}