// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests
{
    [TestFixture]
    public class CerbosAdminClientTest : CerbosTest
    {
        [Test]
        public void AddOrUpdatePolicy()
        {
            var addOrUpdatePolicyRequest = AddOrUpdatePolicyRequest.NewInstance()
                .WithJson("""{"apiVersion":"api.cerbos.dev/v1","resourcePolicy":{"version":"default","resource":"add_or_update_test_policy"}}""");

            Assert.That((Func<Response.AddOrUpdatePolicyResponse>)(() => _clientAdmin.AddOrUpdatePolicy(addOrUpdatePolicyRequest, _metadata)), Throws.Nothing);

            var deletePolicyRequest = DeletePolicyRequest.NewInstance("resource.add_or_update_test_policy.vdefault");

            var have = _clientAdmin.DeletePolicy(deletePolicyRequest, _metadata);
            Assert.That(have.DeletedPolicies, Is.EqualTo(1));
        }

        [Test]
        public void DisablePolicy()
        {
            var request = DisablePolicyRequest.NewInstance("resource.leave_request.vstaging");

            var have = _clientAdmin.DisablePolicy(request, _metadata).DisabledPolicies;
            Assert.That(have, Is.EqualTo(1));
        }

        [Test]
        public void EnablePolicy()
        {
            var request = EnablePolicyRequest.NewInstance("resource.leave_request.vstaging");

            var have = _clientAdmin.EnablePolicy(request, _metadata).EnabledPolicies;
            Assert.That(have, Is.EqualTo(1));
        }

        [Test]
        public void GetPolicy()
        {
            var request = GetPolicyRequest.NewInstance("resource.leave_request.vstaging");

            var have = _clientAdmin.GetPolicy(request, _metadata).Policies;
            Assert.That(have.Count, Is.EqualTo(1));
            Assert.That(have[0].Raw.ResourcePolicy.Resource, Is.EqualTo("leave_request"));
            Assert.That(have[0].Raw.ResourcePolicy.Version, Is.EqualTo("staging"));
        }

        [Test]
        public void ListPolicies()
        {
            var request = ListPoliciesRequest.NewInstance("resource.leave_request.v20210210", "resource.leave_request.vstaging");

            var have = _clientAdmin.ListPolicies(request, _metadata).PolicyIds;
            Assert.That(have, Is.EqualTo(new List<string> { "resource.leave_request.v20210210", "resource.leave_request.vstaging" }));
        }

        [Test]
        public void PurgeStoreRevisions()
        {
            var request = PurgeStoreRevisionsRequest.NewInstance(0);
            var have = _clientAdmin.PurgeStoreRevisions(request, _metadata).AffectedRows;
            Assert.That(have, Is.EqualTo(19));
        }
    }
}
