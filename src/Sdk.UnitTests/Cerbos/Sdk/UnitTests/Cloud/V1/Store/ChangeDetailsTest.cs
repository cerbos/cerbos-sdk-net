// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Cloud.V1.Store;
using static Cerbos.Sdk.Cloud.V1.Store.ChangeDetails.Types;
using Google.Protobuf.WellKnownTypes;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

public class ChangeDetailsTest
{
    private const string Description = "description";
    private const string Name = "name";
    private const string Author = "author";
    private const string Committer = "committer";
    private const string Hash = "hash";
    private const string Message = "message";
    private const string Repo = "repo";
    private const string Ref = "ref";
    private DateTime AuthorDate = DateTime.UtcNow;
    private DateTime CommitDate = DateTime.UtcNow;
    private const string Source = "source";
    private const string MetadataKeyAndValue1 = "keyAndValue1";
    private const string MetadataKeyAndValue2 = "keyAndValue2";

    [Test]
    public void Git()
    {
        var uploader = Uploader.NewInstance(Name);

        var git = ChangeDetails.Types.Git.NewInstance(Repo, Hash).
            WithAuthor(Author).
            WithCommitter(Committer).
            WithMessage(Message).
            WithRef(Ref).
            WithAuthorDate(AuthorDate).
            WithCommitDate(CommitDate);

        var changeDetails = ChangeDetails.Git(Description, uploader, git).ToChangeDetails();

        Assert.That(changeDetails.Description, Is.EqualTo(Description));
        Assert.That(changeDetails.Uploader.Name, Is.EqualTo(Name));

        Assert.That(changeDetails.Git.Author, Is.EqualTo(Author));
        Assert.That(changeDetails.Git.Committer, Is.EqualTo(Committer));
        Assert.That(changeDetails.Git.Hash, Is.EqualTo(Hash));
        Assert.That(changeDetails.Git.Message, Is.EqualTo(Message));
        Assert.That(changeDetails.Git.Repo, Is.EqualTo(Repo));
        Assert.That(changeDetails.Git.Ref, Is.EqualTo(Ref));
        Assert.That(changeDetails.Git.AuthorDate.ToDateTime(), Is.EqualTo(AuthorDate));
        Assert.That(changeDetails.Git.CommitDate.ToDateTime(), Is.EqualTo(CommitDate));
    }

    [Test]
    public void Internal()
    {
        var uploader = Uploader.NewInstance(Name);

        var internal_ = ChangeDetails.Types.Internal.NewInstance(Source).
            WithMetadata(MetadataKeyAndValue1, MetadataValue.StringValue(MetadataKeyAndValue1)).
            WithMetadata(MetadataKeyAndValue2, MetadataValue.StringValue(MetadataKeyAndValue2));

        var changeDetails = ChangeDetails.Internal(Description, uploader, internal_).ToChangeDetails();

        Assert.That(changeDetails.Description, Is.EqualTo(Description));
        Assert.That(changeDetails.Uploader.Name, Is.EqualTo(Name));

        Assert.That(changeDetails.Internal.Metadata.Count, Is.EqualTo(2));
        Assert.That(changeDetails.Internal.Metadata[MetadataKeyAndValue1].StringValue, Is.EqualTo(MetadataKeyAndValue1));
        Assert.That(changeDetails.Internal.Metadata[MetadataKeyAndValue2].StringValue, Is.EqualTo(MetadataKeyAndValue2));
    }

    public static class Types
    {
        public sealed class GitTest
        {
            private const string Author = "author";
            private const string Committer = "committer";
            private const string Hash = "hash";
            private const string Message = "message";
            private const string Repo = "repo";
            private const string Ref = "ref";
            private DateTime AuthorDate = DateTime.UtcNow;
            private DateTime CommitDate = DateTime.UtcNow;

            [Test]
            public void All()
            {
                var git = ChangeDetails.Types.Git.NewInstance(Repo, Hash).
                    WithAuthor(Author).
                    WithCommitter(Committer).
                    WithMessage(Message).
                    WithRef(Ref).
                    WithAuthorDate(AuthorDate).
                    WithCommitDate(CommitDate).
                    ToGit();

                Assert.That(git.Author, Is.EqualTo(Author));
                Assert.That(git.Committer, Is.EqualTo(Committer));
                Assert.That(git.Hash, Is.EqualTo(Hash));
                Assert.That(git.Message, Is.EqualTo(Message));
                Assert.That(git.Repo, Is.EqualTo(Repo));
                Assert.That(git.Ref, Is.EqualTo(Ref));
                Assert.That(git.AuthorDate.ToDateTime(), Is.EqualTo(AuthorDate));
                Assert.That(git.CommitDate.ToDateTime(), Is.EqualTo(CommitDate));
            }
        }

        public sealed class InternalTest
        {
            private const string Source = "source";
            private const string MetadataKeyAndValue1 = "keyAndValue1";
            private const string MetadataKeyAndValue2 = "keyAndValue2";
            private const string MetadataKeyAndValue3 = "keyAndValue3";

            [Test]
            public void WithMetadata()
            {
                var i = ChangeDetails.Types.Internal.NewInstance(Source).
                    WithMetadata(MetadataKeyAndValue1, MetadataValue.StringValue(MetadataKeyAndValue1)).
                    WithMetadata(MetadataKeyAndValue2, MetadataValue.StringValue(MetadataKeyAndValue2)).
                    ToInternal();

                Assert.That(i.Metadata.Count, Is.EqualTo(2));
                Assert.That(i.Metadata[MetadataKeyAndValue1].StringValue, Is.EqualTo(MetadataKeyAndValue1));
                Assert.That(i.Metadata[MetadataKeyAndValue2].StringValue, Is.EqualTo(MetadataKeyAndValue2));
            }

            [Test]
            public void WithMetadatas()
            {
                var i = ChangeDetails.Types.Internal.NewInstance(Source).
                    WithMetadatas(
                        new Dictionary<string, MetadataValue>
                        {
                            { MetadataKeyAndValue1, MetadataValue.StringValue(MetadataKeyAndValue1) },
                            { MetadataKeyAndValue2, MetadataValue.StringValue(MetadataKeyAndValue2) },
                        }
                    ).
                    WithMetadatas(
                        new Dictionary<string, MetadataValue>
                        {
                            { MetadataKeyAndValue3, MetadataValue.StringValue(MetadataKeyAndValue3) },
                        }
                    ).
                    ToInternal();

                Assert.That(i.Metadata.Count, Is.EqualTo(3));
                Assert.That(i.Metadata[MetadataKeyAndValue1].StringValue, Is.EqualTo(MetadataKeyAndValue1));
                Assert.That(i.Metadata[MetadataKeyAndValue2].StringValue, Is.EqualTo(MetadataKeyAndValue2));
                Assert.That(i.Metadata[MetadataKeyAndValue3].StringValue, Is.EqualTo(MetadataKeyAndValue3));
            }

            [Test]
            public void WithSource()
            {
                var i = ChangeDetails.Types.Internal.NewInstance(Source).
                    ToInternal();

                Assert.That(i.Source, Is.EqualTo(Source));
            }
        }

        public sealed class MetadataTest
        {
            [Test]
            public void BoolAttribute()
            {
                var boolAttr = MetadataValue.BoolValue(true).ToValue();
                Assert.That(boolAttr.BoolValue, Is.EqualTo(true));
            }

            [Test]
            public void DoubleAttribute()
            {
                var doubleAttr = MetadataValue.DoubleValue(1.32).ToValue();
                Assert.That(doubleAttr.NumberValue, Is.EqualTo(1.32));
            }

            [Test]
            public void NullAttribute()
            {
                var nullAttr = MetadataValue.NullValue().ToValue();
                Assert.That(nullAttr.NullValue, Is.EqualTo(NullValue.NullValue));
            }

            [Test]
            public void StringAttribute()
            {
                var stringAttr = MetadataValue.StringValue("GB").ToValue();
                Assert.That(stringAttr.StringValue, Is.EqualTo("GB"));
            }

            [Test]
            public void ListAttribute()
            {
                var listAttr = MetadataValue.ListValue([MetadataValue.BoolValue(true), MetadataValue.StringValue("GB")]).ToValue();
                Assert.That(listAttr.ListValue.Values.Count, Is.EqualTo(2));
                Assert.That(listAttr.ListValue.Values[0].BoolValue, Is.EqualTo(true));
                Assert.That(listAttr.ListValue.Values[1].StringValue, Is.EqualTo("GB"));
            }

            [Test]
            public void MapAttribute()
            {
                var mapAttr = MetadataValue.MapValue(new Dictionary<string, MetadataValue>
                {
                    {"boolAttr", MetadataValue.BoolValue(true)},
                    {"stringAttr", MetadataValue.StringValue("GB")}
                }).ToValue();
                Assert.That(mapAttr.StructValue.Fields.Count, Is.EqualTo(2));
                Assert.That(mapAttr.StructValue.Fields["boolAttr"].BoolValue, Is.EqualTo(true));
                Assert.That(mapAttr.StructValue.Fields["stringAttr"].StringValue, Is.EqualTo("GB"));
            }
        }

        public sealed class UploaderTest
        {
            private const string Name = "name";
            private const string MetadataKeyAndValue1 = "keyAndValue1";
            private const string MetadataKeyAndValue2 = "keyAndValue2";
            private const string MetadataKeyAndValue3 = "keyAndValue3";

            [Test]
            public void WithName()
            {
                var uploader = Uploader.NewInstance(Name).
                    ToUploader();

                Assert.That(uploader.Name, Is.EqualTo(Name));
            }

            [Test]
            public void WithMetadata()
            {
                var uploader = Uploader.NewInstance(Name).
                    WithMetadata(MetadataKeyAndValue1, MetadataValue.StringValue(MetadataKeyAndValue1)).
                    WithMetadata(MetadataKeyAndValue2, MetadataValue.StringValue(MetadataKeyAndValue2)).
                    ToUploader();

                Assert.That(uploader.Metadata.Count, Is.EqualTo(2));
                Assert.That(uploader.Metadata[MetadataKeyAndValue1].StringValue, Is.EqualTo(MetadataKeyAndValue1));
                Assert.That(uploader.Metadata[MetadataKeyAndValue2].StringValue, Is.EqualTo(MetadataKeyAndValue2));
            }

            [Test]
            public void WithMetadatas()
            {
                var uploader = Uploader.NewInstance(Name).
                    WithMetadatas(
                        new Dictionary<string, MetadataValue>
                        {
                            { MetadataKeyAndValue1, MetadataValue.StringValue(MetadataKeyAndValue1) },
                            { MetadataKeyAndValue2, MetadataValue.StringValue(MetadataKeyAndValue2) },
                        }
                    ).
                    WithMetadatas(
                        new Dictionary<string, MetadataValue>
                        {
                            { MetadataKeyAndValue3, MetadataValue.StringValue(MetadataKeyAndValue3) },
                        }
                    ).
                    ToUploader();

                Assert.That(uploader.Metadata.Count, Is.EqualTo(3));
                Assert.That(uploader.Metadata[MetadataKeyAndValue1].StringValue, Is.EqualTo(MetadataKeyAndValue1));
                Assert.That(uploader.Metadata[MetadataKeyAndValue2].StringValue, Is.EqualTo(MetadataKeyAndValue2));
                Assert.That(uploader.Metadata[MetadataKeyAndValue3].StringValue, Is.EqualTo(MetadataKeyAndValue3));
            }
        }
    }
}