// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class FileOp
    {
        private Api.Cloud.V1.Store.FileOp.OpOneofCase OneOf = Api.Cloud.V1.Store.FileOp.OpOneofCase.None;
        private File AddOrUpdate_ { get; set; }
        private string Delete_ { get; set; }

        private FileOp() { }

        public static FileOp AddOrUpdate(File addOrUpdate)
        {
            return new FileOp()
            {
                AddOrUpdate_ = addOrUpdate,
                OneOf = Api.Cloud.V1.Store.FileOp.OpOneofCase.AddOrUpdate
            };
        }

        public static FileOp Delete(string delete)
        {
            return new FileOp()
            {
                Delete_ = delete,
                OneOf = Api.Cloud.V1.Store.FileOp.OpOneofCase.Delete
            };
        }

        public Api.Cloud.V1.Store.FileOp ToFileOp()
        {
            if (OneOf == Api.Cloud.V1.Store.FileOp.OpOneofCase.AddOrUpdate)
            {
                if (AddOrUpdate_ == null)
                {
                    throw new Exception("Specify non-null value for addOrUpdate operation");
                }

                return new Api.Cloud.V1.Store.FileOp
                {
                    AddOrUpdate = AddOrUpdate_.ToFile()
                };
            }
            else if (OneOf == Api.Cloud.V1.Store.FileOp.OpOneofCase.Delete)
            {
                return new Api.Cloud.V1.Store.FileOp
                {
                    Delete = Delete_
                };
            }
            else
            {
                throw new Exception("Either addOrUpdate or delete operation must be specified");
            }
        }
    }
}