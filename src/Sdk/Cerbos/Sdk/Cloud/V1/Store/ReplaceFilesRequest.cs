// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Google.Protobuf.Collections;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class ReplaceFilesRequest
    {
        private string StoreId { get; set; }
        private Types.Condition Condition { get; set; }
        private Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase ContentsOneOf = Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase.None;
        private Types.Files Files { get; set; }
        private byte[] ZippedContents { get; set; }
        private ChangeDetails ChangeDetails { get; set; }

        private ReplaceFilesRequest() { }

        public static ReplaceFilesRequest NewInstance()
        {
            return new ReplaceFilesRequest();
        }

        public ReplaceFilesRequest WithStoreId(string storeId)
        {
            StoreId = storeId;
            return this;
        }

        public ReplaceFilesRequest WithCondition(Types.Condition condition)
        {
            Condition = condition;
            return this;
        }

        public ReplaceFilesRequest ContentsZippedContents(byte[] zippedContents)
        {
            ContentsOneOf = Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase.ZippedContents;
            ZippedContents = zippedContents;
            return this;
        }

        public ReplaceFilesRequest ContentsFiles(byte[] zippedContents)
        {
            ContentsOneOf = Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase.Files;
            ZippedContents = zippedContents;
            return this;
        }

        public ReplaceFilesRequest WithChangeDetails(ChangeDetails changeDetails)
        {
            ChangeDetails = changeDetails;
            return this;
        }

        public Api.Cloud.V1.Store.ReplaceFilesRequest ToReplaceFilesRequest()
        {
            if (StoreId == null)
            {
                throw new Exception("StoreId must be specified");
            }

            var request = new Api.Cloud.V1.Store.ReplaceFilesRequest
            {
                StoreId = StoreId,
            };

            if (ContentsOneOf == Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase.Files)
            {
                if (Files == null)
                {
                    throw new Exception("Specify non-null value for files");
                }

                request.Files = Files.ToFiles();
            }
            else if (ContentsOneOf == Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase.ZippedContents)
            {
                if (ZippedContents == null)
                {
                    throw new Exception("Specify non-null value for zipped contents");
                }

                request.ZippedContents = Google.Protobuf.ByteString.CopyFrom(ZippedContents);
            }
            else
            {
                throw new Exception("Either addOrUpdate or delete operation must be specified");
            }

            if (ChangeDetails != null)
            {
                request.ChangeDetails = ChangeDetails.ToChangeDetails();
            }

            if (Condition != null)
            {
                request.Condition = Condition.ToCondition();
            }

            return request;
        }

        public static class Types
        {
            public sealed class Condition
            {
                private long StoreVersionMustEqual { get; set; }

                private Condition() { }

                public static Condition NewInstance()
                {
                    return new Condition();
                }

                public Condition WithStoreVersionMustEqual(long storeVersionMustEqual)
                {
                    StoreVersionMustEqual = storeVersionMustEqual;
                    return this;
                }

                public Api.Cloud.V1.Store.ReplaceFilesRequest.Types.Condition ToCondition()
                {
                    return new Api.Cloud.V1.Store.ReplaceFilesRequest.Types.Condition()
                    {
                        StoreVersionMustEqual = StoreVersionMustEqual,
                    };
                }
            }

            public sealed class Files
            {
                private List<File> F { get; set; }

                private Files()
                {
                    F = new List<File>();
                }

                public static Files NewInstance()
                {
                    return new Files();
                }

                public Files WithFiles(params File[] files)
                {
                    F.AddRange(files);
                    return this;
                }

                public Api.Cloud.V1.Store.ReplaceFilesRequest.Types.Files ToFiles()
                {
                    var files = new RepeatedField<Api.Cloud.V1.Store.File>();
                    foreach (var f in F)
                    {
                        files.Add(f.ToFile());
                    }

                    return new Api.Cloud.V1.Store.ReplaceFilesRequest.Types.Files()
                    {
                        Files_ = { files }
                    };
                }
            }
        }
    }
}