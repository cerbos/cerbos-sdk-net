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
        if (
            string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CERBOS_HUB_API_ENDPOINT"))
            || string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CERBOS_HUB_CLIENT_ID"))
            || string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CERBOS_HUB_CLIENT_SECRET"))
        )
        {
            Assert.Ignore("Skipping the test because CERBOS_HUB_API_ENDPOINT, CERBOS_HUB_CLIENT_ID or CERBOS_HUB_CLIENT_SECRET environment variables must be specified");
        }

        var storeId = Environment.GetEnvironmentVariable("CERBOS_HUB_STORE_ID");
        if (storeId == null)
        {
            Assert.Ignore("Skipping the test because CERBOS_HUB_STORE_ID environment variable must be specified");
        }
        StoreId = storeId;

        var hubClient = HubClientBuilder.FromEnv().Build();

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
            GetFilesRequest.NewInstance(StoreId, [ExpectedFiles[0]])
        );

        Assert.That(response.Files[0].Path, Is.EqualTo(ExpectedFiles[0]));
    }

    [Test]
    public async Task GetFilesAsync()
    {
        var response = await StoreClient.GetFilesAsync(
            GetFilesRequest.NewInstance(StoreId, [ExpectedFiles[0]])
        );

        Assert.That(response.Files[0].Path, Is.EqualTo(ExpectedFiles[0]));
    }

    [Test]
    public void ListFiles()
    {
        var response = StoreClient.ListFiles(
            ListFilesRequest.NewInstance(StoreId)
        );

        Assert.That(response.Files, Is.EqualTo(ExpectedFiles));
    }

    [Test]
    public async Task ListFilesAsync()
    {
        var response = await StoreClient.ListFilesAsync(
            ListFilesRequest.NewInstance(StoreId)
        );

        Assert.That(response.Files, Is.EqualTo(ExpectedFiles));
    }

    [Test]
    public void ModifyFiles()
    {
        var initialStoreVersion = StoreClient.ListFiles(
            ListFilesRequest.NewInstance(StoreId)
        ).StoreVersion;

        var fileContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToTemporaryPolicyFile));
        var response = StoreClient.ModifyFiles(
            ModifyFilesRequest.WithChangeDetails(
                StoreId,
                ChangeDetails.Internal("cerbos-sdk-net/ModifyFiles/Op=AddOrUpdate", ChangeDetails.Types.Uploader.NewInstance("cerbos-sdk-net"), ChangeDetails.Types.Internal.NewInstance("sdk")),
                FileOp.AddOrUpdate(File.NewInstance("temporary_policies/temporary.yaml", fileContents))
            )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 1));

        response = StoreClient.ModifyFiles(
            ModifyFilesRequest.WithChangeDetails(
                StoreId,
                ChangeDetails.Internal("cerbos-sdk-net/ModifyFiles/Op=Delete", ChangeDetails.Types.Uploader.NewInstance("cerbos-sdk-net"), ChangeDetails.Types.Internal.NewInstance("sdk")),
                FileOp.Delete("temporary_policies/temporary.yaml")
            )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 2));
    }

    [Test]
    public async Task ModifyFilesAsync()
    {
        var initialStoreVersion = (await StoreClient.ListFilesAsync(
            ListFilesRequest.NewInstance(StoreId)
        )).StoreVersion;

        var fileContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToTemporaryPolicyFile));
        var response = await StoreClient.ModifyFilesAsync(
            ModifyFilesRequest.WithChangeDetails(
                StoreId,
                ChangeDetails.Internal("cerbos-sdk-net/ModifyFilesAsync/Op=AddOrUpdate", ChangeDetails.Types.Uploader.NewInstance("cerbos-sdk-net"), ChangeDetails.Types.Internal.NewInstance("sdk")),
                FileOp.AddOrUpdate(File.NewInstance("temporary_policies/temporary.yaml", fileContents))
            )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 1));

        response = await StoreClient.ModifyFilesAsync(
            ModifyFilesRequest.WithChangeDetails(
                StoreId,
                ChangeDetails.Internal("cerbos-sdk-net/ModifyFilesAsync/Op=Delete", ChangeDetails.Types.Uploader.NewInstance("cerbos-sdk-net"), ChangeDetails.Types.Internal.NewInstance("sdk")),
                FileOp.Delete("temporary_policies/temporary.yaml")
            )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 2));
    }

    [Test]
    public void ReplaceFiles()
    {
        var initialStoreVersion = StoreClient.ListFiles(
            ListFilesRequest.NewInstance(StoreId)
        ).StoreVersion;

        var temporaryContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToTemporaryContents));
        var response = StoreClient.ReplaceFiles(
            ReplaceFilesRequest.WithZippedContents(
                StoreId,
                temporaryContents,
                null,
                ChangeDetails.Internal("cerbos-sdk-net/ReplaceFiles/With=temporary.zip", ChangeDetails.Types.Uploader.NewInstance("cerbos-sdk-net"), ChangeDetails.Types.Internal.NewInstance("sdk"))
            )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 1));

        var storeContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToStoreContents));
        response = StoreClient.ReplaceFiles(
            ReplaceFilesRequest.WithZippedContents(
                StoreId,
                storeContents,
                null,
                ChangeDetails.Internal("cerbos-sdk-net/ReplaceFiles/With=store.zip", ChangeDetails.Types.Uploader.NewInstance("cerbos-sdk-net"), ChangeDetails.Types.Internal.NewInstance("sdk"))
            )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 2));
    }

    [Test]
    public async Task ReplaceFilesAsync()
    {
        var initialStoreVersion = (await StoreClient.ListFilesAsync(
            ListFilesRequest.NewInstance(StoreId)
        )).StoreVersion;

        var temporaryContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToTemporaryContents));
        var response = StoreClient.ReplaceFiles(
            ReplaceFilesRequest.WithZippedContents(
                StoreId,
                temporaryContents,
                null,
                ChangeDetails.Internal("cerbos-sdk-net/ReplaceFilesAsync/With=temporary.zip", ChangeDetails.Types.Uploader.NewInstance("cerbos-sdk-net"), ChangeDetails.Types.Internal.NewInstance("sdk"))
            )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 1));

        var storeContents = System.IO.File.ReadAllBytes(Path.GetFullPath(PathToStoreContents));
        response = StoreClient.ReplaceFiles(
            ReplaceFilesRequest.WithZippedContents(
                StoreId,
                storeContents,
                null,
                ChangeDetails.Internal("cerbos-sdk-net/ReplaceFilesAsync/With=store.zip", ChangeDetails.Types.Uploader.NewInstance("cerbos-sdk-net"), ChangeDetails.Types.Internal.NewInstance("sdk"))
            )
        );

        Assert.That(response.NewStoreVersion, Is.EqualTo(initialStoreVersion + 2));

        Assert.Catch<OperationDiscardedException>(() =>
        {
            response = StoreClient.ReplaceFiles(
                ReplaceFilesRequest.WithZippedContents(
                    StoreId,
                    storeContents,
                    null,
                    ChangeDetails.Internal("cerbos-sdk-net/ReplaceFilesAsync/With=store.zip", ChangeDetails.Types.Uploader.NewInstance("cerbos-sdk-net"), ChangeDetails.Types.Internal.NewInstance("sdk"))
                )
            );
        });
    }
}
