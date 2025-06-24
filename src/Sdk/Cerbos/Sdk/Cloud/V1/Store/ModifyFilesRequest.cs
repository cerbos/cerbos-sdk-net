// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class ModifyFilesRequest
    {
        private string StoreId { get; set; }
        private Types.Condition Condition { get; set; }
        private List<FileOp> Operations { get; set; }
        private ChangeDetails ChangeDetails { get; set; }

        private ModifyFilesRequest(string storeId, FileOp[] operations)
        {
            StoreId = storeId;
            Operations = new List<FileOp>(operations);
        }

        public static ModifyFilesRequest NewInstance(string storeId, FileOp[] operations)
        {
            return new ModifyFilesRequest(storeId, operations);
        }

        public ModifyFilesRequest WithChangeDetails(ChangeDetails changeDetails)
        {
            ChangeDetails = changeDetails;
            return this;
        }
        
        public ModifyFilesRequest WithCondition(Types.Condition condition)
        {
            Condition = condition;
            return this;
        }

        public Api.Cloud.V1.Store.ModifyFilesRequest ToModifyFilesRequest()
        {
            var request = new Api.Cloud.V1.Store.ModifyFilesRequest
            {
                StoreId = StoreId,
                Operations = { Operations.Select((op) => { return op.ToFileOp(); }) }
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

                private Condition(long storeVersionMustEqual)
                {
                    StoreVersionMustEqual = storeVersionMustEqual;
                }

                public static Condition NewInstance(long storeVersionMustEqual)
                {
                    return new Condition(storeVersionMustEqual);
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