// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

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

        private File(string path, byte[] contents)
        {
            this.path = path;
            this.contents = contents;
        }

        public File(Api.Cloud.V1.Store.File file)
        {
            R = file;
            path = file.Path;
            contents = file.Contents.ToByteArray();
        }

        public static File NewInstance(string path, byte[] contents)
        {
            return new File(path, contents);
        }

        public Api.Cloud.V1.Store.File ToFile()
        {
            return new Api.Cloud.V1.Store.File
            {
                Path = Path,
                Contents = Google.Protobuf.ByteString.CopyFrom(Contents)
            };
        }
    }
}
