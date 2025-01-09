// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Builder;
using Google.Protobuf.WellKnownTypes;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builder;

public class ResourceTest
{
    private AttributeValue[]? _listAttr;
    private Dictionary<string, AttributeValue>? _mapAttr;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _listAttr = new []
        {
            AttributeValue.BoolValue(true),
            AttributeValue.DoubleValue(1.2),
            AttributeValue.NullValue(),
            AttributeValue.DoubleValue(2),
            AttributeValue.StringValue("GB")
        };
        _mapAttr = new Dictionary<string, AttributeValue>
        {
            { "boolAttr", AttributeValue.BoolValue(true) },
            { "doubleAttr", AttributeValue.DoubleValue(1.2) },
            { "nullAttr", AttributeValue.NullValue() },
            { "intAttr", AttributeValue.DoubleValue(2) },
            { "listAttr", AttributeValue.ListValue(_listAttr) },
            { "stringAttr", AttributeValue.StringValue("GB") }
        };
    }

    [Test]
    public void TestWithAttribute()
    {
        var resource = Resource.NewInstance("leave_request", "XX125")
            .WithAttribute("boolAttr", AttributeValue.BoolValue(true))
            .WithAttribute("doubleAttr", AttributeValue.DoubleValue(1.2))
            .WithAttribute("nullAttr", AttributeValue.NullValue())
            .WithAttribute("intAttr", AttributeValue.DoubleValue(2))
            .WithAttribute("listAttr", AttributeValue.ListValue(_listAttr))
            .WithAttribute("stringAttr", AttributeValue.StringValue("GB"))
            .WithAttribute("mapAttr", AttributeValue.MapValue(_mapAttr))
            .ToResource();

        Assert.That(resource.Attr.Count, Is.EqualTo(7));
        Assert.That(resource.Attr["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(resource.Attr["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(resource.Attr["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resource.Attr["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(resource.Attr["stringAttr"].StringValue, Is.EqualTo("GB"));
        
        Assert.That(resource.Attr["listAttr"].ListValue.Values.Count, Is.EqualTo(5));
        Assert.That(resource.Attr["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(resource.Attr["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(resource.Attr["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resource.Attr["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(resource.Attr["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields.Count, Is.EqualTo(6));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values.Count, Is.EqualTo(5));

        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["stringAttr"].StringValue, Is.EqualTo("GB"));
    }
    
    [Test]
    public void TestWithAttributes()
    {
        var resource = Resource.NewInstance("leave_request", "XX125")
            .WithAttributes(
                new Dictionary<string, AttributeValue>
                {
                    { "boolAttr", AttributeValue.BoolValue(true) },
                    { "doubleAttr", AttributeValue.DoubleValue(1.2) },
                    { "nullAttr", AttributeValue.NullValue() },
                    { "intAttr", AttributeValue.DoubleValue(2) },
                }
            )
            .WithAttributes(
                new Dictionary<string, AttributeValue>
                {
                    { "listAttr", AttributeValue.ListValue(_listAttr) },
                    { "stringAttr", AttributeValue.StringValue("GB") },
                    { "mapAttr", AttributeValue.MapValue(_mapAttr) },
                }
            )
            .ToResource();
        
        Assert.That(resource.Attr.Count, Is.EqualTo(7));
        Assert.That(resource.Attr["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(resource.Attr["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(resource.Attr["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resource.Attr["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(resource.Attr["stringAttr"].StringValue, Is.EqualTo("GB"));
        
        Assert.That(resource.Attr["listAttr"].ListValue.Values.Count, Is.EqualTo(5));
        Assert.That(resource.Attr["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(resource.Attr["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(resource.Attr["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resource.Attr["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(resource.Attr["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields.Count, Is.EqualTo(6));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values.Count, Is.EqualTo(5));

        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        Assert.That(resource.Attr["mapAttr"].StructValue.Fields["stringAttr"].StringValue, Is.EqualTo("GB"));
    }
    
    [Test]
    public void TestConstructor()
    {
        var resource = Resource.NewInstance("leave_request", "XX125").ToResource();
        Assert.That(resource.Id, Is.EqualTo("XX125"));
        Assert.That(resource.Kind, Is.EqualTo("leave_request"));
    }
    
    [Test]
    public void TestWithId()
    {
        var resource = Resource.NewInstance("leave_request", "XX125").WithId("XX225").ToResource();
        Assert.That(resource.Id, Is.EqualTo("XX225"));
    }
    
    [Test]
    public void TestWithKind()
    {
        var resource = Resource.NewInstance("leave_request", "XX125").WithKind("purchase_order").ToResource();
        Assert.That(resource.Kind, Is.EqualTo("purchase_order"));
    }
    
    [Test]
    public void TestWithPolicyVersion()
    {
        var resource = Resource.NewInstance("leave_request", "XX125").WithPolicyVersion("20210210").ToResource();
        Assert.That(resource.PolicyVersion, Is.EqualTo("20210210"));
    }
    
    [Test]
    public void TestWithScope()
    {
        var resource = Resource.NewInstance("leave_request", "XX125").WithScope("acme").ToResource();
        Assert.That(resource.Scope, Is.EqualTo("acme"));
    }
}