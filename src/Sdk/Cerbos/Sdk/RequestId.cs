using System;

namespace Cerbos.Sdk
{
    public static class RequestId {
        public static string Generate() {
            return Guid.NewGuid().ToString();
        }
    }
}