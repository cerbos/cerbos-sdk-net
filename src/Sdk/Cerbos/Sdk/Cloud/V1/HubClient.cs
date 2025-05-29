// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using Cerbos.Sdk.Cloud.V1.ApiKey;
using Cerbos.Sdk.Cloud.V1.Store;

namespace Cerbos.Sdk.Cloud.V1
{
    public interface IHubClient
    {
        IApiKeyClient ApiKeyClient
        {
            get;
        }

        IStoreClient StoreClient
        {
            get;
        }
    }

    /// <summary>
    /// HubClient provides a client implementation that communicates with Cerbos Hub.
    /// </summary>
    public sealed class HubClient : IHubClient
    {
        private readonly IApiKeyClient apiKeyClient;
        private readonly IStoreClient storeClient;

        public HubClient(
            IApiKeyClient apiKeyClient = null,
            IStoreClient storeClient = null
        )
        {
            this.apiKeyClient = apiKeyClient;
            this.storeClient = storeClient;
        }

        public IApiKeyClient ApiKeyClient
        {
            get
            {
                if (apiKeyClient == null)
                {
                    throw new Exception("ApiKeyClient is null");
                }

                return apiKeyClient;
            }
        }

        public IStoreClient StoreClient
        {
            get
            {
                if (storeClient == null)
                {
                    throw new Exception("StoreClient is null");
                }

                return storeClient;
            }
        }
    }
}