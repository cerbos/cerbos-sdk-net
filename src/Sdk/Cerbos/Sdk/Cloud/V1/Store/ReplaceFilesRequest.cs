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

        private ReplaceFilesRequest(
            string storeId,
            Types.Condition condition = null,
            byte[] zippedContents = null,
            Types.Files files = null,
            ChangeDetails changeDetails = null
        )
        {
            StoreId = storeId;
            Condition = condition;
            ChangeDetails = changeDetails;
            if (files != null)
            {
                ContentsOneOf = Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase.Files;
                Files = files;
            }
            else if (zippedContents != null)
            {
                ContentsOneOf = Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase.ZippedContents;
                ZippedContents = zippedContents;
            }
            else
            {
                throw new Exception("Either files or zippedContents must be specified");
            }
        }

        public static ReplaceFilesRequest WithFiles(
            string storeId,
            Types.Files files,
            Types.Condition condition = null,
            ChangeDetails changeDetails = null
        )
        {
            if (files == null)
            {
                throw new Exception("Specify non-null value for files");
            }

            return new ReplaceFilesRequest(storeId, condition, null, files, changeDetails);
        }

        public static ReplaceFilesRequest WithZippedContents(
            string storeId,
            byte[] zippedContents,
            Types.Condition condition = null,
            ChangeDetails changeDetails = null
        )
        {
            if (zippedContents == null)
            {
                throw new Exception("Specify non-null value for zipped contents");
            }

            return new ReplaceFilesRequest(storeId, condition, zippedContents, null, changeDetails);
        }

        public Api.Cloud.V1.Store.ReplaceFilesRequest ToReplaceFilesRequest()
        {
            var request = new Api.Cloud.V1.Store.ReplaceFilesRequest
            {
                StoreId = StoreId,
            };

            if (ContentsOneOf == Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase.Files)
            {
                request.Files = Files.ToFiles();
            }
            else if (ContentsOneOf == Api.Cloud.V1.Store.ReplaceFilesRequest.ContentsOneofCase.ZippedContents)
            {
                request.ZippedContents = Google.Protobuf.ByteString.CopyFrom(ZippedContents);
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

                public static Files NewInstance(params File[] files)
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