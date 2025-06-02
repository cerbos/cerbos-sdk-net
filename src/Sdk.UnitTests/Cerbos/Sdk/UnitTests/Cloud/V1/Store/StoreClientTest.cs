// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Cloud.V1.Store;
using Cerbos.Sdk.Cloud.V1;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

public class StoreClientTest
{
    private string StoreId { get; set; }

    private IStoreClient StoreClient { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var apiEndpoint = Environment.GetEnvironmentVariable("CERBOS_HUB_API_ENDPOINT");
        if (apiEndpoint == null)
        {
            Assert.Ignore("Skipping the test, CERBOS_HUB_API_ENDPOINT environment variable is not set!");
        }

        var clientId = Environment.GetEnvironmentVariable("CERBOS_HUB_CLIENT_ID");
        if (clientId == null)
        {
            Assert.Ignore("Skipping the test, CERBOS_HUB_CLIENT_ID environment variable is not set!");
        }

        var clientSecret = Environment.GetEnvironmentVariable("CERBOS_HUB_CLIENT_SECRET");
        if (clientSecret == null)
        {
            Assert.Ignore("Skipping the test, CERBOS_HUB_CLIENT_SECRET environment variable is not set!");
        }

        var storeId = Environment.GetEnvironmentVariable("CERBOS_HUB_STORE_ID");
        if (storeId == null)
        {
            Assert.Ignore("Skipping the test, CERBOS_HUB_STORE_ID environment variable is not set!");
        }
        StoreId = storeId;

        var hubClient = HubClientBuilder.ForTarget(apiEndpoint).
            WithCredentials(clientId, clientSecret).
            Build();

        StoreClient = hubClient.StoreClient;
    }
}
