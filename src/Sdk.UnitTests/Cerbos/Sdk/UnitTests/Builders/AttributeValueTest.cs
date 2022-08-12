using Cerbos.Sdk.Builders;
using Google.Protobuf.WellKnownTypes;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builders;

public class AttributeValueTest
{
    private readonly AttributeValue _boolAttr = AttributeValue.BoolValue(true);
    private readonly AttributeValue _doubleAttr = AttributeValue.DoubleValue(1.32);
    private readonly AttributeValue _nullAttr = AttributeValue.NullValue();
    private readonly AttributeValue _stringAttr = AttributeValue.StringValue("GB");
    private readonly AttributeValue _listAttr = AttributeValue.ListValue(new AttributeValue[2]{AttributeValue.BoolValue(true), AttributeValue.StringValue("GB")});
    private readonly AttributeValue _mapAttr = AttributeValue.MapValue(new Dictionary<string, AttributeValue>()
    {
        {"boolAttr", AttributeValue.BoolValue(true)},
        {"stringAttr", AttributeValue.StringValue("GB")}
    });
    
    [Test]
    public void Test()
    {
        Assert.That(_boolAttr.ToValue().BoolValue, Is.EqualTo(true));
        Assert.That(_doubleAttr.ToValue().NumberValue, Is.EqualTo(1.32));
        Assert.That(_nullAttr.ToValue().NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(_stringAttr.ToValue().StringValue, Is.EqualTo("GB"));
        Assert.That(_listAttr.ToValue().ListValue.Values.Count, Is.EqualTo(2));
        Assert.That(_listAttr.ToValue().ListValue.Values[0].BoolValue, Is.EqualTo(true));
        Assert.That(_listAttr.ToValue().ListValue.Values[1].StringValue, Is.EqualTo("GB"));
        Assert.That(_mapAttr.ToValue().StructValue.Fields.Count, Is.EqualTo(2));
        Assert.That(_mapAttr.ToValue().StructValue.Fields["boolAttr"].BoolValue, Is.EqualTo(true));
        Assert.That(_mapAttr.ToValue().StructValue.Fields["stringAttr"].StringValue, Is.EqualTo("GB"));
    }
}