// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class FileFilter
    {
        private StringMatch Path { get; set; }

        private FileFilter() { }

        public static FileFilter NewInstance()
        {
            return new FileFilter();
        }

        public FileFilter WithPath(StringMatch path)
        {
            Path = path;
            return this;
        }

        public Api.Cloud.V1.Store.FileFilter ToFileFilter()
        {
            var request = new Api.Cloud.V1.Store.FileFilter();

            if (Path != null)
            {
                request.Path = Path.ToStringMatch();
            }

            return request;
        }
    }
}