// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Request;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Request;

public class AddOrUpdateSchemaRequestTest
{
    private readonly string id = "test_schema.json";
    private readonly string definition = """{"$schema":"https://json-schema.org/draft/2020-12/schema","type":"object","properties":{"department":{"type":"string","enum":["engineering"]}},"required":["department"]}""";

    [Test]
    public void TestWith()
    {
        var schema = Schema.Schema.NewInstance(id, definition);

        var request = AddOrUpdateSchemaRequest.NewInstance()
            .With(schema)
            .ToAddOrUpdateSchemaRequest();

        Assert.That(request.Schemas[0].Id, Is.EqualTo(id));
        Assert.That(request.Schemas[0].Definition.ToStringUtf8(), Is.EqualTo(definition));
    }

    [Test]
    public void TestWithJson()
    {
        var request = AddOrUpdateSchemaRequest.NewInstance()
            .WithJson(id, definition)
            .ToAddOrUpdateSchemaRequest();

        Assert.That(request.Schemas[0].Id, Is.EqualTo(id));
        Assert.That(request.Schemas[0].Definition.ToStringUtf8(), Is.EqualTo(definition));
    }
}
