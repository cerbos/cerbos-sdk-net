// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System;
using Cerbos.Sdk.Cloud.V1.Store;

namespace Cerbos.Sdk.Cloud.V1
{
    public interface IHubClient
    {
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
        private readonly IStoreClient storeClient;

        public HubClient(
            IStoreClient storeClient = null
        )
        {
            this.storeClient = storeClient;
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