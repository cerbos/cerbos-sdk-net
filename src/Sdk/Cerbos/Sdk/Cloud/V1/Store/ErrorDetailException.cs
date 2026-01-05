// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using Cerbos.Api.Cloud.V1.Store;
using Grpc.Core;

namespace Cerbos.Sdk.Cloud.V1.Store
{
    public static class ErrorDetailException
    {
        private const string GrpcStatusDetailsBinTrailer = "grpc-status-details-bin";

        public static void FromTrailers(string message, Metadata trailers)
        {
            var statusBytes = trailers.Get(GrpcStatusDetailsBinTrailer).ValueBytes;
            if (statusBytes == null)
            {
                return;
            }

            var status = Google.Rpc.Status.Parser.ParseFrom(statusBytes);
            var details = status.Details[0];
            if (details == null)
            {
                return;
            }

            if (details.Is(ErrDetailCannotModifyGitConnectedStore.Descriptor))
            {
                ErrDetailCannotModifyGitConnectedStore detail;
                if (details.TryUnpack(out detail))
                {
                    throw new CannotModifyGitConnectedStoreException(message, detail);
                }
            }
            else if (details.Is(ErrDetailConditionUnsatisfied.Descriptor))
            {
                ErrDetailConditionUnsatisfied detail;
                if (details.TryUnpack(out detail))
                {
                    throw new ConditionUnsatisfiedException(message, detail);
                }
            }
            else if (details.Is(ErrDetailNoUsableFiles.Descriptor))
            {
                ErrDetailNoUsableFiles detail;
                if (details.TryUnpack(out detail))
                {
                    throw new NoUsableFilesException(message, detail);
                }
            }
            else if (details.Is(ErrDetailOperationDiscarded.Descriptor))
            {
                ErrDetailOperationDiscarded detail;
                if (details.TryUnpack(out detail))
                {
                    throw new OperationDiscardedException(message, detail);
                }
            }
            else if (details.Is(ErrDetailValidationFailure.Descriptor))
            {
                ErrDetailValidationFailure detail;
                if (details.TryUnpack(out detail))
                {
                    throw new ValidationFailureException(message, detail);
                }
            }

            return;
        }
    }

    public sealed class CannotModifyGitConnectedStoreException : Exception
    {
        public ErrDetailCannotModifyGitConnectedStore Detail { get; set; }

        public CannotModifyGitConnectedStoreException(string message, ErrDetailCannotModifyGitConnectedStore detail) : base(message)
        {
            Detail = detail;
        }
    }

    public sealed class ConditionUnsatisfiedException : Exception
    {
        public ErrDetailConditionUnsatisfied Detail { get; set; }

        public ConditionUnsatisfiedException(string message, ErrDetailConditionUnsatisfied detail) : base(message)
        {
            Detail = detail;
        }
    }

    public sealed class NoUsableFilesException : Exception
    {
        public ErrDetailNoUsableFiles Detail { get; set; }

        public NoUsableFilesException(string message, ErrDetailNoUsableFiles detail) : base(message)
        {
            Detail = detail;
        }
    }

    public sealed class OperationDiscardedException : Exception
    {
        public ErrDetailOperationDiscarded Detail { get; set; }

        public OperationDiscardedException(string message, ErrDetailOperationDiscarded detail) : base(message)
        {
            Detail = detail;
        }
    }

    public sealed class ValidationFailureException : Exception
    {
        public ErrDetailValidationFailure Detail { get; set; }

        public ValidationFailureException(string message, ErrDetailValidationFailure detail) : base(message)
        {
            Detail = detail;
        }
    }
}
