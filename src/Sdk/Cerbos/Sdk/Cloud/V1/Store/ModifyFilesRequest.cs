// Copyright 2021-2026 Zenauth Ltd.
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

        private ModifyFilesRequest(
            string storeId,
            Types.Condition condition = null,
            ChangeDetails changeDetails = null,
            params FileOp[] operations
        )
        {
            StoreId = storeId;
            Operations = new List<FileOp>(operations);
            Condition = condition;
            ChangeDetails = changeDetails;
        }

        public static ModifyFilesRequest NewInstance(
            string storeId,
            Types.Condition condition = null,
            ChangeDetails changeDetails = null,
            params FileOp[] operations
        )
        {
            return new ModifyFilesRequest(storeId, condition, changeDetails, operations);
        }

        public static ModifyFilesRequest WithChangeDetails(
            string storeId,
            ChangeDetails changeDetails,
            params FileOp[] operations
        )
        {
            return new ModifyFilesRequest(storeId, null, changeDetails, operations);
        }

        public static ModifyFilesRequest WithCondition(
            string storeId,
            Types.Condition condition,
            params FileOp[] operations
        )
        {
            return new ModifyFilesRequest(storeId, condition, null, operations);
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