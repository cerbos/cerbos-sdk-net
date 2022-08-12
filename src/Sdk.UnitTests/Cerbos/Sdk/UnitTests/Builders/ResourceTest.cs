using Cerbos.Sdk.Builders;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builders;

public class ResourceTest
{
    private const string Kind = "leave_request"; 
    private const string Id = "XX125"; 
    private const string PolicyVersion = "20210210"; 
    private readonly AttributeValue _lonelyAttr = AttributeValue.StringValue("lonely"); 
    private readonly AttributeValue _boolAttr = AttributeValue.BoolValue(true); 
    private readonly AttributeValue _stringAttr = AttributeValue.StringValue("GB"); 
    private readonly AttributeValue _nullAttr = AttributeValue.NullValue(); 
    private readonly AttributeValue _doubleAttr = AttributeValue.DoubleValue(1.32);
    private readonly AttributeValue _listAttr = AttributeValue.ListValue(new AttributeValue[2]{AttributeValue.BoolValue(true), AttributeValue.StringValue("GB")});
    private readonly AttributeValue _mapAttr = AttributeValue.MapValue(new Dictionary<string, AttributeValue>()
    {
        {"boolAttr", AttributeValue.BoolValue(true)},
        {"stringAttr", AttributeValue.StringValue("GB")}
    });
    
    [Test]
    public void Test()
    {
        var resource = Resource.NewInstance(Kind, Id)
            .WithPolicyVersion(PolicyVersion)
            .WithAttribute("lonelyAttr", _lonelyAttr)
            .WithAttributes(new()
                {
                    {"boolAttr", _boolAttr},
                    {"stringAttr", _stringAttr},
                    {"nullAttr", _nullAttr},
                    {"doubleAttr", _doubleAttr},
                    {"listAttr", _listAttr},
                    {"mapAttr", _mapAttr}
                }
            )
            .ToResource();
        
        Assert.That(resource.Kind, Is.EqualTo(Kind));
        Assert.That(resource.Id, Is.EqualTo(Id));
        Assert.That(resource.PolicyVersion, Is.EqualTo(PolicyVersion));
        
        Assert.That(resource.Attr["lonelyAttr"], Is.EqualTo(_lonelyAttr.ToValue()));
        Assert.That(resource.Attr["stringAttr"], Is.EqualTo(_stringAttr.ToValue()));
        Assert.That(resource.Attr["boolAttr"], Is.EqualTo(_boolAttr.ToValue()));
        Assert.That(resource.Attr["nullAttr"], Is.EqualTo(_nullAttr.ToValue()));
        Assert.That(resource.Attr["doubleAttr"], Is.EqualTo(_doubleAttr.ToValue()));
        Assert.That(resource.Attr["listAttr"], Is.EqualTo(_listAttr.ToValue()));
        Assert.That(resource.Attr["mapAttr"], Is.EqualTo(_mapAttr.ToValue()));
    }
}