// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Grpc.Core;

namespace Cerbos.Sdk
{
    /// <summary>
    /// CerbosClient provides a client implementation that communicates with the PDP.
    /// </summary>
    public sealed class CerbosAdminClient : ICerbosAdminClient
    {
        private Api.V1.Svc.CerbosAdminService.CerbosAdminServiceClient CerbosAdminServiceClient { get; }
        private readonly Metadata _metadata;

        public CerbosAdminClient(
            Api.V1.Svc.CerbosAdminService.CerbosAdminServiceClient cerbosAdminServiceClient,
            Metadata metadata = null
        )
        {
            CerbosAdminServiceClient = cerbosAdminServiceClient;
            _metadata = metadata;
        }
    }
}
