// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Cerbos.Sdk.Utility
{
    public static class Rpc
    {
        public static T Call<T>(IMessage request, Func<T> op)
        {
            return op();
        }

        public static async Task<T> CallAsync<T>(IMessage request, Func<Task<T>> op)
        {
            return await op();
        }
    }
}
