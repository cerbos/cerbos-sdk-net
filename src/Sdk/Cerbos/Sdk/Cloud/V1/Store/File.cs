// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class File
    {
        private byte[] contents;
        private string path;

        private Api.Cloud.V1.Store.File R { get; set; }
        public byte[] Contents {
            get
            {
                return contents;
            }
        }
        public string Path
        {
            get
            {
                return path;
            }
         }
        public Api.Cloud.V1.Store.File Raw => R;

        private File() {}

        public File(Api.Cloud.V1.Store.File file)
        {
            R = file;
            path = file.Path;
            contents = file.Contents.ToByteArray();
        }

        public static File NewInstance()
        {
            return new File();
        }

        public File WithContents(byte[] contents)
        {
            this.contents = contents;
            if (R == null)
            {
                R = new Api.Cloud.V1.Store.File()
                {
                    Contents = Google.Protobuf.ByteString.CopyFrom(this.contents)
                };
            }
            else
            {
                R.Contents = Google.Protobuf.ByteString.CopyFrom(this.contents);
            }

            return this;
        }

        public File WithPath(string path)
        {
            this.path = path;
            if (R == null)
            {
                R = new Api.Cloud.V1.Store.File()
                {
                    Path = this.path
                };
            }
            else
            {
                R.Path = this.path;
            }

            return this;
        }

        public Api.Cloud.V1.Store.File ToFile()
        {
            if (Contents == null)
            {
                throw new Exception("Contents must be specified");
            }

            if (string.IsNullOrEmpty(Path))
            {
                throw new Exception("Path must be specified");
            }

            return new Api.Cloud.V1.Store.File
            {
                Path = Path,
                Contents = Google.Protobuf.ByteString.CopyFrom(Contents)
            };
        }
    }
}
