// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class ChangeDetails
    {
        private Api.Cloud.V1.Store.ChangeDetails.OriginOneofCase OneOf = Api.Cloud.V1.Store.ChangeDetails.OriginOneofCase.None;
        private string Description { get; set; }
        private Types.Git Git_ { get; set; }
        private Types.Internal Internal_ { get; set; }
        private Types.Uploader Uploader { get; set; }

        private ChangeDetails(
            string description,
            Types.Uploader uploader,
            Types.Git git = null,
            Types.Internal internal_ = null
        )
        {
            if (uploader == null)
            {
                throw new Exception("Specify non-null value for uploader");
            }

            Description = description;
            Uploader = uploader;

            if (git != null)
            {
                OneOf = Api.Cloud.V1.Store.ChangeDetails.OriginOneofCase.Git;
                Git_ = git;
            }
            else if (internal_ != null)
            {
                OneOf = Api.Cloud.V1.Store.ChangeDetails.OriginOneofCase.Internal;
                Internal_ = internal_;
            }
            else
            {
                throw new Exception("Either git or internal origin must be specified");
            }
        }

        public static ChangeDetails Git(
            string description,
            Types.Uploader uploader,
            Types.Git git
        )
        {
            if (git == null)
            {
                throw new Exception("Specify non-null value for git");
            }

            return new ChangeDetails(description, uploader, git, null);
        }

        public static ChangeDetails Internal(
            string description,
            Types.Uploader uploader,
            Types.Internal internal_
        )
        {
            if (internal_ == null)
            {
                throw new Exception("Specify non-null value for internal");
            }

            return new ChangeDetails(description, uploader, null, internal_);
        }

        public Api.Cloud.V1.Store.ChangeDetails ToChangeDetails()
        {

            var changeDetails = new Api.Cloud.V1.Store.ChangeDetails
            {
                Description = Description,
                Uploader = Uploader.ToUploader(),
            };

            if (OneOf == Api.Cloud.V1.Store.ChangeDetails.OriginOneofCase.Git)
            {
                changeDetails.Git = Git_.ToGit();
            }
            else if (OneOf == Api.Cloud.V1.Store.ChangeDetails.OriginOneofCase.Internal)
            {
                changeDetails.Internal = Internal_.ToInternal();
            }

            return changeDetails;
        }

        public static class Types
        {
            public sealed class Git
            {
                private string Author { get; set; }
                private string Committer { get; set; }
                private string Hash { get; set; }
                private string Message { get; set; }
                private string Repo { get; set; }
                private string Ref { get; set; }
                private DateTime AuthorDate { get; set; }
                private DateTime CommitDate { get; set; }

                private Git(string repo, string hash)
                {
                    Repo = repo;
                    Hash = hash; 
                }

                public static Git NewInstance(string repo, string hash)
                {
                    return new Git(repo, hash);
                }

                public Git WithAuthor(string author)
                {
                    Author = author;
                    return this;
                }

                public Git WithCommitter(string committer)
                {
                    Committer = committer;
                    return this;
                }

                public Git WithMessage(string message)
                {
                    Message = message;
                    return this;
                }

                public Git WithRef(string ref_)
                {
                    Ref = ref_;
                    return this;
                }

                public Git WithAuthorDate(DateTime authorDate)
                {
                    AuthorDate = authorDate;
                    return this;
                }

                public Git WithCommitDate(DateTime commitDate)
                {
                    CommitDate = commitDate;
                    return this;
                }

                public Api.Cloud.V1.Store.ChangeDetails.Types.Git ToGit()
                {
                    return new Api.Cloud.V1.Store.ChangeDetails.Types.Git()
                    {
                        Author = Author,
                        Committer = Committer,
                        Hash = Hash,
                        Message = Message,
                        Repo = Repo,
                        Ref = Ref,
                        AuthorDate = Timestamp.FromDateTime(AuthorDate),
                        CommitDate = Timestamp.FromDateTime(CommitDate),
                    };
                }
            }

            public sealed class Internal
            {
                private string Source { get; set; }

                private Dictionary<string, Value> Metadata { get; }

                private Internal(string source)
                {
                    Source = source;
                    Metadata = new Dictionary<string, Value>();
                }

                public static Internal NewInstance(string source)
                {
                    return new Internal(source);
                }

                public Internal WithMetadata(string key, MetadataValue value)
                {
                    Metadata.Add(key, value.ToValue());
                    return this;
                }

                public Internal WithMetadatas(Dictionary<string, MetadataValue> metadata)
                {
                    foreach (KeyValuePair<string, MetadataValue> m in metadata)
                    {
                        Metadata.Add(m.Key, m.Value.ToValue());
                    }

                    return this;
                }

                public Api.Cloud.V1.Store.ChangeDetails.Types.Internal ToInternal()
                {
                    return new Api.Cloud.V1.Store.ChangeDetails.Types.Internal()
                    {
                        Source = Source,
                        Metadata = { Metadata }
                    };
                }
            }

            public sealed class MetadataValue
            {
                private Value V { get; }

                private MetadataValue(Value value)
                {
                    V = value;
                }

                public static MetadataValue BoolValue(bool value)
                {
                    return new MetadataValue(Value.ForBool(value));
                }

                public static MetadataValue DoubleValue(double value)
                {
                    return new MetadataValue(Value.ForNumber(value));
                }

                public static MetadataValue ListValue(params MetadataValue[] values)
                {
                    Value[] v = new Value[values.Length];
                    for (var i = 0; i < values.Length; i++)
                    {
                        v[i] = values[i].ToValue();
                    }
                    return new MetadataValue(Value.ForList(v));
                }

                public static MetadataValue MapValue(Dictionary<string, MetadataValue> dict)
                {
                    var s = new Struct();
                    foreach (KeyValuePair<string, MetadataValue> kvp in dict)
                    {
                        s.Fields.Add(kvp.Key, kvp.Value.V);
                    }

                    return new MetadataValue(Value.ForStruct(s));
                }

                public static MetadataValue NullValue()
                {
                    return new MetadataValue(Value.ForNull());
                }

                public static MetadataValue StringValue(string value)
                {
                    return new MetadataValue(Value.ForString(value));
                }

                public Value ToValue()
                {
                    return V;
                }
            }

            public sealed class Uploader
            {
                private string Name { get; set; }

                private Dictionary<string, Value> Metadata { get; }

                private Uploader(string name)
                {
                    Name = name;
                    Metadata = new Dictionary<string, Value>();
                }

                public static Uploader NewInstance(string name)
                {
                    return new Uploader(name);
                }

                public Uploader WithMetadata(string key, MetadataValue value)
                {
                    Metadata.Add(key, value.ToValue());
                    return this;
                }

                public Uploader WithMetadatas(Dictionary<string, MetadataValue> metadata)
                {
                    foreach (KeyValuePair<string, MetadataValue> m in metadata)
                    {
                        Metadata.Add(m.Key, m.Value.ToValue());
                    }

                    return this;
                }

                public Api.Cloud.V1.Store.ChangeDetails.Types.Uploader ToUploader()
                {
                    return new Api.Cloud.V1.Store.ChangeDetails.Types.Uploader()
                    {
                        Name = Name,
                        Metadata = { Metadata }
                    };
                }
            }
        }
    }
}