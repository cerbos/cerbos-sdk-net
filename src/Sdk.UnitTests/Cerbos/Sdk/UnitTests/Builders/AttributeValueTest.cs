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
    
    [Test]
    public void Test()
    {
        Assert.That(_boolAttr.ToValue().BoolValue, Is.EqualTo(true));
        Assert.That(_doubleAttr.ToValue().NumberValue, Is.EqualTo(1.32));
        Assert.That(_nullAttr.ToValue().NullValue, Is.EqualTo(NullValue.NullValue));
        Assert.That(_stringAttr.ToValue().StringValue, Is.EqualTo("GB"));
    }
}