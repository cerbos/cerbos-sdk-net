// Copyright 2021-2025 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Builder;
using Google.Protobuf.WellKnownTypes;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builder;

public class PrincipalTest
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
        var principal = Principal.NewInstance("john")
            .WithAttribute("boolAttr", AttributeValue.BoolValue(true))
            .WithAttribute("doubleAttr", AttributeValue.DoubleValue(1.2))
            .WithAttribute("intAttr", AttributeValue.DoubleValue(2))
            .WithAttribute("listAttr", AttributeValue.ListValue(_listAttr))
            .WithAttribute("mapAttr", AttributeValue.MapValue(_mapAttr))
            .WithAttribute("nullAttr", AttributeValue.NullValue())
            .WithAttribute("stringAttr", AttributeValue.StringValue("GB"))
            .ToPrincipal();

        Assert.That(principal.Attr.Count, Is.EqualTo(7));
        Assert.That(principal.Attr["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(principal.Attr["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(principal.Attr["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(principal.Attr["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(principal.Attr["stringAttr"].StringValue, Is.EqualTo("GB"));
        
        Assert.That(principal.Attr["listAttr"].ListValue.Values.Count, Is.EqualTo(5));
        Assert.That(principal.Attr["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(principal.Attr["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(principal.Attr["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(principal.Attr["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(principal.Attr["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields.Count, Is.EqualTo(6));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values.Count, Is.EqualTo(5));

        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["stringAttr"].StringValue, Is.EqualTo("GB"));
    }

    [Test]
    public void TestAttributes()
    {
        var principal = Principal.NewInstance("john")
            .WithAttributes(
                new Dictionary<string, AttributeValue>
                {
                    { "boolAttr", AttributeValue.BoolValue(true) },
                    { "doubleAttr", AttributeValue.DoubleValue(1.2) },
                    { "intAttr", AttributeValue.DoubleValue(2) },
                    { "nullAttr", AttributeValue.NullValue() },
                }
            )
            .WithAttributes(
                new Dictionary<string, AttributeValue>
                {
                    { "listAttr", AttributeValue.ListValue(_listAttr) },
                    { "mapAttr", AttributeValue.MapValue(_mapAttr) },
                    { "stringAttr", AttributeValue.StringValue("GB") }
                }
            )
            .ToPrincipal();
        
        Assert.That(principal.Attr.Count, Is.EqualTo(7));
        Assert.That(principal.Attr["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(principal.Attr["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(principal.Attr["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(principal.Attr["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(principal.Attr["stringAttr"].StringValue, Is.EqualTo("GB"));
        
        Assert.That(principal.Attr["listAttr"].ListValue.Values.Count, Is.EqualTo(5));
        Assert.That(principal.Attr["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(principal.Attr["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(principal.Attr["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(principal.Attr["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(principal.Attr["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields.Count, Is.EqualTo(6));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values.Count, Is.EqualTo(5));

        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["doubleAttr"].NumberValue, Is.EqualTo(1.2));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["nullAttr"].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["intAttr"].NumberValue, Is.EqualTo(2));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[1].NumberValue, Is.EqualTo(1.2));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[2].NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[3].NumberValue, Is.EqualTo(2));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["listAttr"].ListValue.Values[4].StringValue, Is.EqualTo("GB"));
        Assert.That(principal.Attr["mapAttr"].StructValue.Fields["stringAttr"].StringValue, Is.EqualTo("GB"));
    }

    [Test]
    public void TestConstructor()
    {
        var principal = Principal.NewInstance("john").ToPrincipal();
        Assert.That(principal.Id, Is.EqualTo("john"));
    }
    
    [Test]
    public void TestWithId()
    {
        var principal = Principal.NewInstance("john").WithId("alex").ToPrincipal();
        Assert.That(principal.Id, Is.EqualTo("alex"));
    }

    [Test]
    public void TestWithPolicyVersion()
    {
        var principal = Principal.NewInstance("john").WithPolicyVersion("20210210").ToPrincipal();
        Assert.That(principal.PolicyVersion, Is.EqualTo("20210210"));
    }

    [Test]
    public void TestWithRoles()
    {
        var principal = Principal.NewInstance("john").WithRoles("employee", "manager").WithRoles("admin", "user").ToPrincipal();
        Assert.That(principal.Roles.Count, Is.EqualTo(4));
        Assert.That(principal.Roles[0], Is.EqualTo("employee"));
        Assert.That(principal.Roles[1], Is.EqualTo("manager"));
        Assert.That(principal.Roles[2], Is.EqualTo("admin"));
        Assert.That(principal.Roles[3], Is.EqualTo("user"));
    }
    
    [Test]
    public void TestWithScope()
    {
        var principal = Principal.NewInstance("john").WithScope("acme").ToPrincipal();
        Assert.That(principal.Scope, Is.EqualTo("acme"));
    }
}