// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Cloud.V1;

public class StringMatchTest
{
    private const string Something = "something";

    [Test]
    public void StringMatchEquals()
    {
        var stringMatch = Sdk.Cloud.V1.Store.StringMatch.NewInstance().
            WithEquals(Something)
            .ToStringMatch();

        Assert.That(stringMatch.Equals_, Is.EqualTo(Something));
    }

    [Test]
    public void StringMatchIn()
    {
        var inList = Sdk.Cloud.V1.Store.StringMatch.Types.InList.NewInstance()
            .WithValues(Something, Something);
        var stringMatch = Sdk.Cloud.V1.Store.StringMatch.NewInstance().
            WithIn(inList)
            .ToStringMatch();

        Assert.That(stringMatch.In, Is.EqualTo(inList.ToInList()));
    }

    [Test]
    public void StringMatchLike()
    {
        var stringMatch = Sdk.Cloud.V1.Store.StringMatch.NewInstance().
            WithContains(Something)
            .ToStringMatch();

        Assert.That(stringMatch.Contains, Is.EqualTo(Something));
    }

    public static class Types
    {
        public sealed class InListTest
        {
            [Test]
            public void StringMatchLike()
            {
                var inList = Sdk.Cloud.V1.Store.StringMatch.Types.InList.NewInstance().
                    WithValues(Something, Something).
                    ToInList();

                Assert.That(inList.Values.ToList(), Is.EqualTo(new List<string>{Something, Something}));
            }
        }
    }
}