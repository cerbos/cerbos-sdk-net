// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using System.Text;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

public class FileTest
{
    private const string PathToPolicy = "/path/to/policy.yaml";
    private const string Content = "content";

    [Test]
    public void WithContentsAndPath()
    {
        var contentBytes = Encoding.UTF8.GetBytes(Content);
        var file = Sdk.Cloud.V1.Store.File.NewInstance(PathToPolicy, contentBytes).ToFile();

        Assert.That(file.Contents.ToArray(), Is.EqualTo(contentBytes));
        Assert.That(file.Path, Is.EqualTo(PathToPolicy));
    }
}