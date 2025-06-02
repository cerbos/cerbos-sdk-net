// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Google.Protobuf.Collections;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class ModifyFilesRequest
    {
        private string StoreId { get; set; }
        private Types.Condition Condition { get; set; }
        private List<FileOp> Operations { get; set; }
        private ChangeDetails ChangeDetails { get; set; }

        private ModifyFilesRequest()
        {
            Operations = new List<FileOp>();
        }

        public static ModifyFilesRequest NewInstance()
        {
            return new ModifyFilesRequest();
        }

        public ModifyFilesRequest WithStoreId(string storeId)
        {
            StoreId = storeId;
            return this;
        }

        public ModifyFilesRequest WithCondition(Types.Condition condition)
        {
            Condition = condition;
            return this;
        }

        public ModifyFilesRequest WithOperations(params FileOp[] operations)
        {
            Operations.AddRange(operations);
            return this;
        }

        public ModifyFilesRequest WithChangeDetails(ChangeDetails changeDetails)
        {
            ChangeDetails = changeDetails;
            return this;
        }

        public Api.Cloud.V1.Store.ModifyFilesRequest ToModifyFilesRequest()
        {
            if (StoreId == null)
            {
                throw new Exception("StoreId must be specified");
            }

            if (Operations.Count == 0)
            {
                throw new Exception("Operations must be specified");
            }

            var operations = new RepeatedField<Api.Cloud.V1.Store.FileOp>();
            foreach (var op in Operations)
            {
                operations.Add(op.ToFileOp());
            }

            var request = new Api.Cloud.V1.Store.ModifyFilesRequest
            {
                StoreId = StoreId,
                Operations = { operations },
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

                public Api.Cloud.V1.Store.ModifyFilesRequest.Types.Condition ToCondition()
                {
                    return new Api.Cloud.V1.Store.ModifyFilesRequest.Types.Condition()
                    {
                        StoreVersionMustEqual = StoreVersionMustEqual,
                    };
                }
            }
        }
    }
}