// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Cerbos.Sdk.Utility
{
    public static class Rpc
    {
        private static ProtoValidate.Validator _validator;

        public static T Call<T>(IMessage request, Func<T> op)
        {
            Validate(request);
            return op();
        }

        public static async Task<T> CallAsync<T>(IMessage request, Func<Task<T>> op)
        {
            Validate(request);
            return await op();
        }

        private static void Validate(IMessage message)
        {
            if (_validator == null)
            {
                _validator = new ProtoValidate.Validator(new ProtoValidate.ValidatorOptions() { PreLoadDescriptors = true });
            }

            var validationResult = _validator.Validate(message, false);
            if (!validationResult.IsSuccess)
            {
                throw new ArgumentException(validationResult.ToString());
            }
        }
    }
}
