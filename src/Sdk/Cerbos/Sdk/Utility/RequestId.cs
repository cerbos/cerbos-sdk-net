// Copyright 2021-2024 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Cerbos.Sdk.Utility
{
    public static class RequestId {
        public static string Generate() {
            return Guid.NewGuid().ToString();
        }
    }
}