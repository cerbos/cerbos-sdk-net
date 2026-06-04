// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class InspectPoliciesRequestTest
{
    [Test]
    public void TestNewInstance()
    {
        var request = InspectPoliciesRequest.NewInstance("leave_request.yaml").ToInspectPoliciesRequest();
        Assert.That(request.PolicyId, Has.Member("leave_request.yaml"));
    }

    [Test]
    public void TestWithIncludeDisabled()
    {
        var request = InspectPoliciesRequest.NewInstance().WithIncludeDisabled(true).ToInspectPoliciesRequest();
        Assert.That(request.IncludeDisabled, Is.True);
    }

    [Test]
    public void TestWithNameRegexp()
    {
        var request = InspectPoliciesRequest.NewInstance().WithNameRegexp("^foo").ToInspectPoliciesRequest();
        Assert.That(request.NameRegexp, Is.EqualTo("^foo"));
    }

    [Test]
    public void TestWithScopeRegexp()
    {
        var request = InspectPoliciesRequest.NewInstance().WithScopeRegexp("^foo").ToInspectPoliciesRequest();
        Assert.That(request.ScopeRegexp, Is.EqualTo("^foo"));
    }

    [Test]
    public void TestWithVersionRegexp()
    {
        var request = InspectPoliciesRequest.NewInstance().WithVersionRegexp("^foo").ToInspectPoliciesRequest();
        Assert.That(request.VersionRegexp, Is.EqualTo("^foo"));
    }
}
