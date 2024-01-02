// Copyright 2021-2024 Zenauth Ltd.
// SPDX-License-Identifier: Apache-2.0

using Cerbos.Sdk.Builder;
using Google.Protobuf.WellKnownTypes;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builder;

public class AttributeValueTest
{
    [Test]
    public void TestBoolAttribute()
    {
        var boolAttr = AttributeValue.BoolValue(true).ToValue();
        Assert.That(boolAttr.BoolValue, Is.EqualTo(true));
    }

    [Test]
    public void TestDoubleAttribute()
    {
        var doubleAttr = AttributeValue.DoubleValue(1.32).ToValue();
        Assert.That(doubleAttr.NumberValue, Is.EqualTo(1.32));
    }
    
    [Test]
    public void TestNullAttribute()
    {
        var nullAttr = AttributeValue.NullValue().ToValue();
        Assert.That(nullAttr.NullValue, Is.EqualTo(NullValue.NullValue));
    }

    [Test]
    public void TestStringAttribute()
    {
        var stringAttr = AttributeValue.StringValue("GB").ToValue();
        Assert.That(stringAttr.StringValue, Is.EqualTo("GB"));
    }
    
    [Test]
    public void TestListAttribute()
    {
        var listAttr = AttributeValue.ListValue(new AttributeValue[2]{AttributeValue.BoolValue(true), AttributeValue.StringValue("GB")}).ToValue();
        Assert.That(listAttr.ListValue.Values.Count, Is.EqualTo(2));
        Assert.That(listAttr.ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(listAttr.ListValue.Values[1].StringValue, Is.EqualTo("GB"));
    }
    
    [Test]
    public void TestMapAttribute()
    {
        var mapAttr = AttributeValue.MapValue(new Dictionary<string, AttributeValue>
        {
            {"boolAttr", AttributeValue.BoolValue(true)},
            {"stringAttr", AttributeValue.StringValue("GB")}
        }).ToValue();
        Assert.That(mapAttr.StructValue.Fields.Count, Is.EqualTo(2));
        Assert.That(mapAttr.StructValue.Fields["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(mapAttr.StructValue.Fields["stringAttr"].StringValue, Is.EqualTo("GB"));
    }
}