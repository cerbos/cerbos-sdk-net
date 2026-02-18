// Copyright 2021-2026 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Builder;
using Google.Protobuf.WellKnownTypes;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builder;

public class RequestContextTest
{
    [Test]
    public void TestWithAnnotation()
    {
        var rc = RequestContext.NewInstance();
        rc.WithAnnotation("boolAnnotation", AttributeValue.BoolValue(true));
        rc.WithAnnotation("doubleAnnotation", AttributeValue.DoubleValue(1.32));
        rc.WithAnnotation("nullAnnotation", AttributeValue.NullValue());
        rc.WithAnnotation("stringAnnotation", AttributeValue.StringValue("GB"));
        rc.WithAnnotation("listAnnotation", AttributeValue.ListValue([AttributeValue.BoolValue(true), AttributeValue.StringValue("GB")]));
        rc.WithAnnotation("mapAnnotation", AttributeValue.MapValue(new Dictionary<string, AttributeValue> { { "boolAttr", AttributeValue.BoolValue(true) } }));
        var requestContext = rc.ToRequestContext();

        Assert.That(requestContext.Annotations["boolAnnotation"].BoolValue, Is.EqualTo(true));
        Assert.That(requestContext.Annotations["doubleAnnotation"].NumberValue, Is.EqualTo(1.32));
        Assert.That(requestContext.Annotations["nullAnnotation"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(requestContext.Annotations["stringAnnotation"].StringValue, Is.EqualTo("GB"));

        Assert.That(requestContext.Annotations["listAnnotation"].ListValue.Values.Count, Is.EqualTo(2));
        Assert.That(requestContext.Annotations["listAnnotation"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(requestContext.Annotations["listAnnotation"].ListValue.Values[1].StringValue, Is.EqualTo("GB"));

        Assert.That(requestContext.Annotations["mapAnnotation"].StructValue.Fields.Count, Is.EqualTo(1));
        Assert.That(requestContext.Annotations["mapAnnotation"].StructValue.Fields["boolAttr"].BoolValue, Is.EqualTo(true));
    }

    [Test]
    public void TestWithAnnotations()
    {
        var requestContext = RequestContext.WithAnnotations(new Dictionary<string, AttributeValue>
        {
            { "boolAnnotation", AttributeValue.BoolValue(true) },
            { "doubleAnnotation", AttributeValue.DoubleValue(1.32) },
            { "nullAnnotation", AttributeValue.NullValue() },
            { "stringAnnotation", AttributeValue.StringValue("GB") },
            { "listAnnotation", AttributeValue.ListValue([AttributeValue.BoolValue(true), AttributeValue.StringValue("GB")]) },
            { "mapAnnotation", AttributeValue.MapValue(new Dictionary<string, AttributeValue> { { "boolAttr", AttributeValue.BoolValue(true) } }) },
        }).ToRequestContext();

        Assert.That(requestContext.Annotations["boolAnnotation"].BoolValue, Is.EqualTo(true));
        Assert.That(requestContext.Annotations["doubleAnnotation"].NumberValue, Is.EqualTo(1.32));
        Assert.That(requestContext.Annotations["nullAnnotation"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(requestContext.Annotations["stringAnnotation"].StringValue, Is.EqualTo("GB"));

        Assert.That(requestContext.Annotations["listAnnotation"].ListValue.Values.Count, Is.EqualTo(2));
        Assert.That(requestContext.Annotations["listAnnotation"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(requestContext.Annotations["listAnnotation"].ListValue.Values[1].StringValue, Is.EqualTo("GB"));

        Assert.That(requestContext.Annotations["mapAnnotation"].StructValue.Fields.Count, Is.EqualTo(1));
        Assert.That(requestContext.Annotations["mapAnnotation"].StructValue.Fields["boolAttr"].BoolValue, Is.EqualTo(true));
    }
}