// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public sealed class FileFilter
    {
        private StringMatch Path { get; set; }

        private FileFilter() { }

        public static FileFilter PathContains(string contains)
        {
            return new FileFilter()
            {
                Path = StringMatch.Contains(contains)
            };
        }

        public static FileFilter PathEquals(string equals)
        {
            return new FileFilter()
            {
                Path = StringMatch.Equals(equals)
            };
        }

        public static FileFilter PathIn(StringMatch.Types.InList inList)
        {
            if (inList == null)
            {
                throw new Exception("Specify non-null value for path in");
            }

            return new FileFilter()
            {
                Path = StringMatch.In(inList)
            };
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