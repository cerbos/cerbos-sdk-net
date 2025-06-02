// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class ReplaceFilesRequest
    {
        private string StoreId { get; set; }
        private Types.Condition Condition { get; set; }
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

        public ReplaceFilesRequest WithZippedContents(byte[] zippedContents)
        {
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

            if (ZippedContents == null)
            {
                throw new Exception("ZippedContents must be specified");
            }

            var request = new Api.Cloud.V1.Store.ReplaceFilesRequest
            {
                StoreId = StoreId,
                ZippedContents = Google.Protobuf.ByteString.CopyFrom(ZippedContents),
            };

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
        }
    }
}