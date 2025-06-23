// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Cloud.V1.Store;
using Cerbos.Sdk.Cloud.V1;
using NUnit.Framework;
using File = Cerbos.Sdk.Cloud.V1.Store.File;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

[TestFixture]
public class StoreClientTest
{
    private string StoreId { get; set; }

    private IStoreClient StoreClient { get; set; }

    private List<string> ExpectedFiles { get; set; }

    private const string PathToTemporaryPolicyFile = "./../../../res/cloud/v1/temporary.yaml";

    private const string PathToStoreContents = "./../../../res/cloud/v1/store.zip";
    private const string PathToTemporaryContents = "./../../../res/cloud/v1/temporary.zip";

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

        var hubClient = HubClientBuilder.
            ForTarget(apiEndpoint).
            WithCredentials(clientId, clientSecret).
            Build();

        StoreClient = hubClient.StoreClient;

        ExpectedFiles =
        [
            "_schemas/principal.json",
            "_schemas/resources/leave_request.json",
            "_schemas/resources/purchase_order.json",
            "_schemas/resources/salary_record.json",

            "derived_roles/common_roles.yaml",
            "derived_roles/derived_roles_01.yaml",
            "derived_roles/derived_roles_02.yaml",
            "derived_roles/derived_roles_03.yaml",
            "derived_roles/derived_roles_04.yaml",
            "derived_roles/derived_roles_05.yaml",

            "export_constants/export_constants_01.yaml",

            "export_variables/export_variables_01.yaml",

            "principal_policies/policy_01.yaml",
            "principal_policies/policy_02.yaml",
            "principal_policies/policy_02_acme.hr.yaml",
            "principal_policies/policy_02_acme.sales.yaml",
            "principal_policies/policy_02_acme.yaml",
            "principal_policies/policy_03.yaml",
            "principal_policies/policy_04.yaml",
            "principal_policies/policy_05.yaml",
            "principal_policies/policy_06.yaml",

            "resource_policies/disabled_policy_01.yaml",
            "resource_policies/policy_01.yaml",
            "resource_policies/policy_02.yaml",
            "resource_policies/policy_03.yaml",
            "resource_policies/policy_04.yaml",
            "resource_policies/policy_04_test.yaml",
            "resource_policies/policy_05.yaml",
            "resource_policies/policy_05_acme.hr.uk.brighton.kemptown.yaml",
            "resource_policies/policy_05_acme.hr.uk.brighton.yaml",
            "resource_policies/policy_05_acme.hr.uk.london.yaml",
            "resource_policies/policy_05_acme.hr.uk.yaml",
            "resource_policies/policy_05_acme.hr.yaml",
            "resource_policies/policy_05_acme.yaml",
            "resource_policies/policy_06.yaml",
            "resource_policies/policy_07.yaml",
            "resource_policies/policy_07_acme.yaml",
            "resource_policies/policy_08.yaml",
            "resource_policies/policy_09.yaml",
            "resource_policies/policy_10.yaml",
            "resource_policies/policy_11.yaml",
            "resource_policies/policy_12.yaml",
            "resource_policies/policy_13.yaml",
            "resource_policies/policy_14.yaml",
            "resource_policies/policy_15.yaml",
            "resource_policies/policy_16.yaml",
            "resource_policies/policy_17.acme.sales.yaml",
            "resource_policies/policy_17.acme.yaml",
            "resource_policies/policy_17.yaml",
            "resource_policies/policy_18.yaml",

            "role_policies/policy_01_acme.hr.uk.brighton.yaml",
            "role_policies/policy_02_acme.hr.uk.brighton.yaml",
            "role_policies/policy_03_acme.hr.uk.yaml",
            "role_policies/policy_04_acme.hr.uk.yaml",
            "role_policies/policy_05_acme.hr.uk.london.yaml",
            "role_policies/policy_06_acme.hr.uk.brighton.kemptown.yaml",

            "tests/policy_04_test.yaml",
            "tests/policy_05_test.yaml",
        ];
    }

    [Test]
    public void GetFiles()
    {
        var response = StoreClient.GetFiles(
            GetFilesRequest.NewInstance().
                WithStoreId(StoreId).
                WithFiles(ExpectedFiles[0])
        );

        Assert.That(response.Files[0].Path, Is.EqualTo(ExpectedFiles[0]));
    }

    [Test]
    public async Task GetFilesAsync()
    {
        var response = await StoreClient.GetFilesAsync(
            GetFilesRequest.NewInstance().
                WithStoreId(StoreId).
                WithFiles(ExpectedFiles[0])
        );

        Assert.That(response.Files[0].Path, Is.EqualTo(ExpectedFiles[0]));
    }

    [Test]
    public void ListFiles()
    {
        var response = StoreClient.ListFiles(
            ListFilesRequest.NewInstance().
                WithStoreId(StoreId)
        );

        Assert.That(response.Files, Is.EqualTo(ExpectedFiles));
    }

    [Test]
    public async Task ListFilesAsync()
    {
        var response = await StoreClient.ListFilesAsync(
            ListFilesRequest.NewInstance().
                WithStoreId(StoreId)
        );

        Assert.That(response.Files, Is.EqualTo(ExpectedFiles));
    }

    [Test]
    public void ModifyFiles()
    {
        var initialStoreVersion = StoreClient.ListFiles(
            ListFilesRequest.NewInstance().
                WithStoreId(StoreId)
        ).StoreVersion;

        var fileContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToTemporaryPolicyFile));
        var response = StoreClient.ModifyFiles(
            ModifyFilesRequest.NewInstance().
                WithStoreId(StoreId).
                WithChangeDetails(
                    ChangeDetails.NewInstance().
                        WithDescription("cerbos-sdk-net/ModifyFiles/Op=AddOrUpdate").
                        WithInternal(ChangeDetails.Types.Internal.NewInstance().WithSource("sdk")).
                        WithUploader(ChangeDetails.Types.Uploader.NewInstance().WithName("cerbos-sdk-net"))
                ).
                WithOperations(
                    FileOp.NewInstance().
                        WithAddOrUpdate(
                            File.NewInstance().
                                WithPath("temporary_policies/temporary.yaml").
                                WithContents(fileContents)
                        )
                )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 1));

        response = StoreClient.ModifyFiles(
            ModifyFilesRequest.NewInstance().
                WithStoreId(StoreId).
                WithChangeDetails(
                    ChangeDetails.NewInstance().
                        WithDescription("cerbos-sdk-net/ModifyFiles/Op=Delete").
                        WithInternal(ChangeDetails.Types.Internal.NewInstance().WithSource("sdk")).
                        WithUploader(ChangeDetails.Types.Uploader.NewInstance().WithName("cerbos-sdk-net"))
                ).
                WithOperations(
                    FileOp.NewInstance().WithDelete("temporary_policies/temporary.yaml")
                )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 2));
    }

    [Test]
    public async Task ModifyFilesAsync()
    {
        var initialStoreVersion = (await StoreClient.ListFilesAsync(
            ListFilesRequest.NewInstance().
                WithStoreId(StoreId)
        )).StoreVersion;

        var fileContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToTemporaryPolicyFile));
        var response = await StoreClient.ModifyFilesAsync(
            ModifyFilesRequest.NewInstance().
                WithStoreId(StoreId).
                WithChangeDetails(
                    ChangeDetails.NewInstance().
                        WithDescription("cerbos-sdk-net/ModifyFiles/Op=AddOrUpdate").
                        WithInternal(ChangeDetails.Types.Internal.NewInstance().WithSource("sdk")).
                        WithUploader(ChangeDetails.Types.Uploader.NewInstance().WithName("cerbos-sdk-net"))
                ).
                WithOperations(
                    FileOp.NewInstance().
                        WithAddOrUpdate(
                            File.NewInstance().
                                WithPath("temporary_policies/temporary.yaml").
                                WithContents(fileContents)
                        )
                )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 1));

        response = await StoreClient.ModifyFilesAsync(
            ModifyFilesRequest.NewInstance().
                WithStoreId(StoreId).
                WithChangeDetails(
                    ChangeDetails.NewInstance().
                        WithDescription("cerbos-sdk-net/ModifyFiles/Op=Delete").
                        WithInternal(ChangeDetails.Types.Internal.NewInstance().WithSource("sdk")).
                        WithUploader(ChangeDetails.Types.Uploader.NewInstance().WithName("cerbos-sdk-net"))
                ).
                WithOperations(
                    FileOp.NewInstance().WithDelete("temporary_policies/temporary.yaml")
                )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 2));
    }

    [Test]
    public void ReplaceFiles()
    {
        var initialStoreVersion = StoreClient.ListFiles(
            ListFilesRequest.NewInstance().
                WithStoreId(StoreId)
        ).StoreVersion;

        var temporaryContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToTemporaryContents));
        var response = StoreClient.ReplaceFiles(
            ReplaceFilesRequest.NewInstance().
                WithStoreId(StoreId).
                WithChangeDetails(
                    ChangeDetails.NewInstance().
                        WithDescription("cerbos-sdk-net/ReplaceFiles/With=temporary.zip").
                        WithInternal(ChangeDetails.Types.Internal.NewInstance().WithSource("sdk")).
                        WithUploader(ChangeDetails.Types.Uploader.NewInstance().WithName("cerbos-sdk-net"))
                ).
                WithZippedContents(
                    temporaryContents
                )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 1));

        var storeContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToStoreContents));
        response = StoreClient.ReplaceFiles(
            ReplaceFilesRequest.NewInstance().
                WithStoreId(StoreId).
                WithChangeDetails(
                    ChangeDetails.NewInstance().
                        WithDescription("cerbos-sdk-net/ReplaceFiles/With=store.zip").
                        WithInternal(ChangeDetails.Types.Internal.NewInstance().WithSource("sdk")).
                        WithUploader(ChangeDetails.Types.Uploader.NewInstance().WithName("cerbos-sdk-net"))
                ).
                WithZippedContents(
                    storeContents
                )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 2));
    }

    [Test]
    public async Task ReplaceFilesAsync()
    {
        var initialStoreVersion = (await StoreClient.ListFilesAsync(
            ListFilesRequest.NewInstance().
                WithStoreId(StoreId)
        )).StoreVersion;

        var temporaryContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToTemporaryContents));
        var response = await StoreClient.ReplaceFilesAsync(
            ReplaceFilesRequest.NewInstance().
                WithStoreId(StoreId).
                WithChangeDetails(
                    ChangeDetails.NewInstance().
                        WithDescription("cerbos-sdk-net/ReplaceFiles/With=temporary.zip").
                        WithInternal(ChangeDetails.Types.Internal.NewInstance().WithSource("sdk")).
                        WithUploader(ChangeDetails.Types.Uploader.NewInstance().WithName("cerbos-sdk-net"))
                ).
                WithZippedContents(
                    temporaryContents
                )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 1));

        var storeContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToStoreContents));
        response = await StoreClient.ReplaceFilesAsync(
            ReplaceFilesRequest.NewInstance().
                WithStoreId(StoreId).
                WithChangeDetails(
                    ChangeDetails.NewInstance().
                        WithDescription("cerbos-sdk-net/ReplaceFiles/With=store.zip").
                        WithInternal(ChangeDetails.Types.Internal.NewInstance().WithSource("sdk")).
                        WithUploader(ChangeDetails.Types.Uploader.NewInstance().WithName("cerbos-sdk-net"))
                ).
                WithZippedContents(
                    storeContents
                )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 2));
    }
}
