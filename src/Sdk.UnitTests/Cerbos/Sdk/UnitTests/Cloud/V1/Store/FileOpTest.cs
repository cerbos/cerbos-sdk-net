// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Text;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

public class FileOpTest
{
    private const string PathToPolicy = "/path/to/policy.yaml";
    private const string Content = "content";

    [Test]
    public void OpAddOrUpdate()
    {   
        var file = Sdk.Cloud.V1.Store.File.NewInstance().
            WithContents(Encoding.UTF8.GetBytes(Content)).
            WithPath(PathToPolicy);
        var fileOp = Sdk.Cloud.V1.Store.FileOp.
            NewInstance().
            OpAddOrUpdate(file).
            ToFileOp();

        Assert.That(fileOp.AddOrUpdate, Is.EqualTo(file.ToFile()));
    }

    [Test]
    public void OpDelete()
    {
        var fileOp = Sdk.Cloud.V1.Store.FileOp.
            NewInstance().
            OpDelete(PathToPolicy).
            ToFileOp();

        Assert.That(fileOp.Delete, Is.EqualTo(PathToPolicy));
    }
}