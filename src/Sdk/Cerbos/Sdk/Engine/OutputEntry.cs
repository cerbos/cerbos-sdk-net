// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Cerbos.Sdk.Engine
{
    public sealed class OutputEntry
    {
        private Api.V1.Engine.OutputEntry OE { get; }

        public Api.V1.Engine.OutputEntry Raw => OE;

        public string Src => OE.Src;

        public Google.Protobuf.WellKnownTypes.Value Val => OE.Val;

        public string Action => OE.Action;

        public string Error => OE.Error;

        public OutputEntry(Api.V1.Engine.OutputEntry outputEntry)
        {
            OE = outputEntry;
        }
    }

    public sealed class OutputEntryEvaluationException : Exception
    {
        private OutputEntryEvaluationException() { }

        private OutputEntryEvaluationException(string message) : base(message) { }

        public static void FromOutputEntry(OutputEntry outputEntry)
        {
            if (!string.IsNullOrEmpty(outputEntry.Error))
            {
                throw new OutputEntryEvaluationException(outputEntry.Error);
            }

            if (outputEntry.Val.HasNullValue)
            {
                throw new OutputEntryEvaluationException("Output evaluation error");
            }
        }
    }

    public sealed class OutputEntryNotFoundException : Exception
    {
        private OutputEntryNotFoundException() { }

        private OutputEntryNotFoundException(string message) : base(message) { }

        public static OutputEntryNotFoundException Action(string action)
        {
            return new OutputEntryNotFoundException($"Failed to find output entry with its action set to '{action}'");
        }

        public static OutputEntryNotFoundException Src(string src)
        {
            return new OutputEntryNotFoundException($"Failed to find output entry with its src set to '{src}'");
        }
    }
}