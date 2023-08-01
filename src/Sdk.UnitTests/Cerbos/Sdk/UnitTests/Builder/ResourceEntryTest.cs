// Copyright 2021-2023 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Builder;
using Google.Protobuf.WellKnownTypes;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builder;

public class ResourceEntryTest
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
    public void TestWithAction()
    {
        var resourceEntry = ResourceEntry.NewInstance("leave_request", "XX125")
            .WithActions("approve", "create")
            .WithActions("defer")
            .ToResourceEntry();
        
        Assert.That(resourceEntry.Actions.Count, Is.EqualTo(3));
        Assert.That(resourceEntry.Actions[0], Is.EqualTo("approve"));
        Assert.That(resourceEntry.Actions[1], Is.EqualTo("create"));
        Assert.That(resourceEntry.Actions[2], Is.EqualTo("defer"));
    }

    [Test]
    public void TestWithAttribute()
    {
        var resourceEntry = ResourceEntry.NewInstance("leave_request", "XX125")
            .WithAttribute("boolAttr", AttributeValue.BoolValue(true))
            .WithAttribute("doubleAttr", AttributeValue.DoubleValue(1.2))
            .WithAttribute("nullAttr", AttributeValue.NullValue())
            .WithAttribute("intAttr", AttributeValue.DoubleValue(2))
            .WithAttribute("listAttr", AttributeValue.ListValue(_listAttr))
            .WithAttribute("stringAttr", AttributeValue.StringValue("GB"))
            .WithAttribute("mapAttr", AttributeValue.MapValue(_mapAttr))
            .ToResourceEntry();

        Assert.That(resourceEntry.Resource.Attr.Count, Is.EqualTo(7));
        Assert.That(resourceEntry.Resource.Attr["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(resourceEntry.Resource.Attr["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(resourceEntry.Resource.Attr["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resourceEntry.Resource.Attr["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(resourceEntry.Resource.Attr["stringAttr"].StringValue, Is.EqualTo("GB"));
        
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values.Count, Is.EqualTo(5));
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields.Count, Is.EqualTo(6));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values.Count, Is.EqualTo(5));

        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["stringAttr"].StringValue, Is.EqualTo("GB"));
    }
    
    [Test]
    public void TestWithAttributes()
    {
        var resourceEntry = ResourceEntry.NewInstance("leave_request", "XX125")
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
            .ToResourceEntry();
        
        Assert.That(resourceEntry.Resource.Attr.Count, Is.EqualTo(7));
        Assert.That(resourceEntry.Resource.Attr["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(resourceEntry.Resource.Attr["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(resourceEntry.Resource.Attr["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resourceEntry.Resource.Attr["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(resourceEntry.Resource.Attr["stringAttr"].StringValue, Is.EqualTo("GB"));
        
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values.Count, Is.EqualTo(5));
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(resourceEntry.Resource.Attr["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields.Count, Is.EqualTo(6));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values.Count, Is.EqualTo(5));

        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"].StructValue.Fields["stringAttr"].StringValue, Is.EqualTo("GB"));
    }
    
    [Test]
    public void TestConstructor()
    {
        var resourceEntry = ResourceEntry.NewInstance("leave_request", "XX125").ToResourceEntry();
        Assert.That(resourceEntry.Resource.Id, Is.EqualTo("XX125"));
        Assert.That(resourceEntry.Resource.Kind, Is.EqualTo("leave_request"));
    }
    
    [Test]
    public void TestWithId()
    {
        var resourceEntry = ResourceEntry.NewInstance("leave_request", "XX125").WithId("XX225").ToResourceEntry();
        Assert.That(resourceEntry.Resource.Id, Is.EqualTo("XX225"));
    }
    
    [Test]
    public void TestWithKind()
    {
        var resourceEntry = ResourceEntry.NewInstance("leave_request", "XX125").WithKind("purchase_order").ToResourceEntry();
        Assert.That(resourceEntry.Resource.Kind, Is.EqualTo("purchase_order"));
    }
    
    [Test]
    public void TestWithPolicyVersion()
    {
        var resourceEntry = ResourceEntry.NewInstance("leave_request", "XX125").WithPolicyVersion("20210210").ToResourceEntry();
        Assert.That(resourceEntry.Resource.PolicyVersion, Is.EqualTo("20210210"));
    }
    
    [Test]
    public void TestWithScope()
    {
        var resourceEntry = ResourceEntry.NewInstance("leave_request", "XX125").WithScope("acme").ToResourceEntry();
        Assert.That(resourceEntry.Resource.Scope, Is.EqualTo("acme"));
    }
}