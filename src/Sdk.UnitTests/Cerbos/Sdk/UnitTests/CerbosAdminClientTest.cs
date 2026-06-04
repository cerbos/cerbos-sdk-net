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
        public void AddOrUpdateSchema()
        {
            var addOrUpdateSchemaRequest = AddOrUpdateSchemaRequest.NewInstance()
                .WithJson(
                    "test_schema.json",
                    """{"$schema":"https://json-schema.org/draft/2020-12/schema","type":"object","properties":{"department":{"type":"string","enum":["engineering"]}},"required":["department"]}"""
                );

            Assert.That((Func<Response.AddOrUpdateSchemaResponse>)(() => _clientAdmin.AddOrUpdateSchema(addOrUpdateSchemaRequest, _metadata)), Throws.Nothing);

            var deleteSchemaRequest = DeleteSchemaRequest.NewInstance("test_schema.json");

            var have = _clientAdmin.DeleteSchema(deleteSchemaRequest, _metadata);
            Assert.That(have.DeletedSchemas, Is.EqualTo(1));
        }

        [Test]
        public void ArgumentException()
        {
            var request = ListPoliciesRequest.NewInstance("");
            Assert.That((Func<Response.ListPoliciesResponse>)(() => _clientAdmin.ListPolicies(request, _metadata)), Throws.ArgumentException);
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

            Assert.That(have[0].ApiVersion, Is.EqualTo("api.cerbos.dev/v1"));
            Assert.That(have[0].Description, Is.EqualTo(""));
            Assert.That(have[0].Disabled, Is.EqualTo(false));
            Assert.That(have[0].OneOf, Is.EqualTo(Api.V1.Policy.Policy.PolicyTypeOneofCase.ResourcePolicy));
            Assert.That(have[0].Kind, Is.EqualTo(Api.V1.Policy.Kind.Resource));
            Assert.That(have[0].ResourcePolicy.Resource, Is.EqualTo("leave_request"));
            Assert.That(have[0].ResourcePolicy.Version, Is.EqualTo("staging"));
        }

        [Test]
        public void GetSchema()
        {
            var request = GetSchemaRequest.NewInstance("resources/leave_request.json");

            var have = _clientAdmin.GetSchema(request, _metadata).Schemas;
            Assert.That(have.Count, Is.EqualTo(1));
            Assert.That(have[0].Id, Is.EqualTo("resources/leave_request.json"));
            Assert.That(have[0].Definition, Is.EqualTo("""{"$schema":"https://json-schema.org/draft/2020-12/schema","type":"object","properties":{"department":{"type":"string","enum":["marketing","engineering"]},"geography":{"type":"string"},"team":{"type":"string"},"id":{"type":"string"},"owner":{"type":"string"},"status":{"type":"string"},"dev_record":{"type":"boolean"}},"required":["department","geography","team","id"]}"""));
        }

        [Test]
        public void InspectPolicies()
        {
            var request = InspectPoliciesRequest.NewInstance("resource.leave_request.v20210210");

            var have = _clientAdmin.InspectPolicies(request, _metadata).Results;
            Assert.That(have.Count, Is.EqualTo(1));
            Assert.That(have["resource.leave_request.v20210210"].Actions, Is.EqualTo(new List<string> { "*", "approve", "create", "defer", "delete", "view", "view:*", "view:public" }));

            Assert.That(have["resource.leave_request.v20210210"].Attributes.Count, Is.EqualTo(4));
            Assert.That(have["resource.leave_request.v20210210"].Attributes[0].Kind, Is.EqualTo(Api.V1.Response.InspectPoliciesResponse.Types.Attribute.Types.Kind.ResourceAttribute));
            Assert.That(have["resource.leave_request.v20210210"].Attributes[0].Name, Is.EqualTo("geography"));

            Assert.That(have["resource.leave_request.v20210210"].Constants.Count, Is.Zero);

            Assert.That(have["resource.leave_request.v20210210"].DerivedRoles.Count, Is.EqualTo(4));
            Assert.That(have["resource.leave_request.v20210210"].DerivedRoles[0].Name, Is.EqualTo("any_employee"));
            Assert.That(have["resource.leave_request.v20210210"].DerivedRoles[0].Kind, Is.EqualTo(Api.V1.Response.InspectPoliciesResponse.Types.DerivedRole.Types.Kind.Imported));
            Assert.That(have["resource.leave_request.v20210210"].DerivedRoles[0].Source, Is.EqualTo("derived_roles.beta"));

            Assert.That(have["resource.leave_request.v20210210"].PolicyId, Is.EqualTo("resource.leave_request.v20210210"));

            Assert.That(have["resource.leave_request.v20210210"].Variables.Count, Is.EqualTo(2));
            Assert.That(have["resource.leave_request.v20210210"].Variables[0].Name, Is.EqualTo("pending_approval"));
            Assert.That(have["resource.leave_request.v20210210"].Variables[0].Value, Is.EqualTo("null"));
            Assert.That(have["resource.leave_request.v20210210"].Variables[0].Kind, Is.EqualTo(Api.V1.Response.InspectPoliciesResponse.Types.Variable.Types.Kind.Undefined));
            Assert.That(have["resource.leave_request.v20210210"].Variables[0].Source, Is.EqualTo(""));
            Assert.That(have["resource.leave_request.v20210210"].Variables[0].Used, Is.True);
        }

        [Test]
        public void ListPolicies()
        {
            var request = ListPoliciesRequest.NewInstance("resource.leave_request.v20210210", "resource.leave_request.vstaging");

            var have = _clientAdmin.ListPolicies(request, _metadata).PolicyIds;
            Assert.That(have, Is.EqualTo(new List<string> { "resource.leave_request.v20210210", "resource.leave_request.vstaging" }));
        }

        [Test]
        public void ListSchemas()
        {
            var request = ListSchemasRequest.NewInstance();

            var have = _clientAdmin.ListSchemas(request, _metadata).SchemaIds;
            Assert.That(have, Is.EqualTo(new List<string> { "principal.json", "resources/leave_request.json" }));
        }

        [Test]
        public void PurgeStoreRevisions()
        {
            var request = PurgeStoreRevisionsRequest.NewInstance(0);
            var have = _clientAdmin.PurgeStoreRevisions(request, _metadata).AffectedRows;
            Assert.That(have, Is.EqualTo(19));
        }

        [Test]
        public void ReloadStore()
        {
            var request = ReloadStoreRequest.NewInstance(true);
            Assert.That((Func<Response.ReloadStoreResponse>)(() => _clientAdmin.ReloadStore(request, _metadata)), Throws.Nothing);
        }
    }
}
