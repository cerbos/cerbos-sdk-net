// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

public class FileFilterTest
{
    private const string Something = "something";

    [Test]
    public void PathContains()
    {
        var fileFilter = Sdk.Cloud.V1.Store.FileFilter.PathContains(Something).ToFileFilter();

        Assert.That(fileFilter.Path.Contains, Is.EqualTo(Something));
    }

    [Test]
    public void PathEquals()
    {
        var fileFilter = Sdk.Cloud.V1.Store.FileFilter.PathEquals(Something).ToFileFilter();

        Assert.That(fileFilter.Path.Equals_, Is.EqualTo(Something));
    }

    [Test]
    public void PathIn()
    {
        var inList = Sdk.Cloud.V1.Store.StringMatch.Types.InList.NewInstance().WithValues(Something, Something);
        var fileFilter = Sdk.Cloud.V1.Store.FileFilter.PathIn(inList).ToFileFilter();

        Assert.That(fileFilter.Path.In.Values[0], Is.EqualTo(Something));
        Assert.That(fileFilter.Path.In.Values[1], Is.EqualTo(Something));
    }
}