// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class FileOp
    {
        private Api.Cloud.V1.Store.FileOp.OpOneofCase OneOf = Api.Cloud.V1.Store.FileOp.OpOneofCase.None;
        private File AddOrUpdate { get; set; }
        private string Delete { get; set; }
    
        private FileOp() {}
        
        public static FileOp NewInstance()
        {
            return new FileOp();
        }

        public FileOp WithAddOrUpdate(File addOrUpdate)
        {
            AddOrUpdate = addOrUpdate;
            OneOf = Api.Cloud.V1.Store.FileOp.OpOneofCase.AddOrUpdate;
            return this;
        }

        public FileOp WithDelete(string delete)
        {
            Delete = delete;
            OneOf = Api.Cloud.V1.Store.FileOp.OpOneofCase.Delete;
            return this;
        }
        
        public Api.Cloud.V1.Store.FileOp ToFileOp()
        {
            if (OneOf == Api.Cloud.V1.Store.FileOp.OpOneofCase.AddOrUpdate)
            {
                if (AddOrUpdate == null)
                {
                    throw new Exception("Specify non-null value for addOrUpdate operation");
                }

                return new Api.Cloud.V1.Store.FileOp
                {
                    AddOrUpdate = AddOrUpdate.ToFile()
                };
            }
            else if (OneOf == Api.Cloud.V1.Store.FileOp.OpOneofCase.Delete)
            {
                return new Api.Cloud.V1.Store.FileOp
                {
                    Delete = Delete
                };
            }
            else
            {
                throw new Exception("Either addOrUpdate or delete operation must be specified");
            }
        }
    }
}