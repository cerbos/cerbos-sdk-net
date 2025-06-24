// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

public class FileFilterTest
{
    private const string Something = "something";

    [Test]
    public void WithPath()
    {
        var stringMatch = Sdk.Cloud.V1.Store.StringMatch.NewInstance().
            WithEquals(Something);

        var fileFilter = Sdk.Cloud.V1.Store.FileFilter.NewInstance().
            WithPath(stringMatch).
            ToFileFilter();

        Assert.That(fileFilter.Path.Equals_, Is.EqualTo(stringMatch.ToStringMatch().Equals_));
    }

    [Test]
    public void Optional()
    {
        var fileFilter = Sdk.Cloud.V1.Store.FileFilter.NewInstance().
            ToFileFilter();

        Assert.That(fileFilter.Path, Is.EqualTo(null));
    }
}