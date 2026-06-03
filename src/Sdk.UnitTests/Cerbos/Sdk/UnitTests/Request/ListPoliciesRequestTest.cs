// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class ListPoliciesRequestTest
{
    [Test]
    public void TestWithIncludeDisabled()
    {
        var request = ListPoliciesRequest.NewInstance().WithIncludeDisabled(true).ToListPoliciesRequest();
        Assert.That(request.IncludeDisabled, Is.True);
    }

    [Test]
    public void TestWithNameRegexp()
    {
        var request = ListPoliciesRequest.NewInstance().WithNameRegexp("^foo").ToListPoliciesRequest();
        Assert.That(request.NameRegexp, Is.EqualTo("^foo"));
    }

    [Test]
    public void TestWithScopeRegexp()
    {
        var request = ListPoliciesRequest.NewInstance().WithScopeRegexp("^foo").ToListPoliciesRequest();
        Assert.That(request.ScopeRegexp, Is.EqualTo("^foo"));
    }

    [Test]
    public void TestWithVersionRegexp()
    {
        var request = ListPoliciesRequest.NewInstance().WithVersionRegexp("^foo").ToListPoliciesRequest();
        Assert.That(request.VersionRegexp, Is.EqualTo("^foo"));
    }

    [Test]
    public void TestWithPolicyId()
    {
        var request = ListPoliciesRequest.NewInstance("leave_request.yaml").ToListPoliciesRequest();
        Assert.That(request.PolicyId, Has.Member("leave_request.yaml"));
    }
}
