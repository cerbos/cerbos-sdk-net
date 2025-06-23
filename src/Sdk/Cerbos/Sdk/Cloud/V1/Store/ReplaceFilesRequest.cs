// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;

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

        private ReplaceFilesRequest(string storeId)
        {
            StoreId = storeId;
        }

        public static ReplaceFilesRequest NewInstance(string storeId)
        {
            return new ReplaceFilesRequest(storeId);
        }

        public ReplaceFilesRequest WithCondition(Types.Condition condition)
        {
            Condition = condition;
            return this;
        }

        public ReplaceFilesRequest WithZippedContents(byte[] zippedContents)
        {
            ContentsOneOf = Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase.ZippedContents;
            ZippedContents = zippedContents;
            return this;
        }

        public ReplaceFilesRequest WithFiles(Types.Files files)
        {
            ContentsOneOf = Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase.Files;
            Files = files;
            return this;
        }

        public ReplaceFilesRequest WithChangeDetails(ChangeDetails changeDetails)
        {
            ChangeDetails = changeDetails;
            return this;
        }

        public Api.Cloud.V1.Store.ReplaceFilesRequest ToReplaceFilesRequest()
        {
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

                private Condition(long storeVersionMustEqual)
                {
                    StoreVersionMustEqual = storeVersionMustEqual;
                }

                public static Condition NewInstance(long storeVersionMustEqual)
                {
                    return new Condition(storeVersionMustEqual);
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

                private Files(File[] files)
                {
                    F = new List<File>(files);
                }

                public static Files NewInstance(File[] files)
                {
                    return new Files(files);
                }

                public Api.Cloud.V1.Store.ReplaceFilesRequest.Types.Files ToFiles()
                {
                    return new Api.Cloud.V1.Store.ReplaceFilesRequest.Types.Files()
                    {
                        Files_ = { F.Select((file) => { return file.ToFile(); }) }
                    };
                }
            }
        }
    }
}