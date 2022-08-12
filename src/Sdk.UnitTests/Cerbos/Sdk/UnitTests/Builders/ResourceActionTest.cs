using Cerbos.Sdk.Builders;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builders;

public class ResourceActionTest
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
    private readonly string[] _actions = new[] { "action:first", "action:second", "action:third" };

    [Test]
    public void Test()
    {
        var resourceEntry = ResourceAction.NewInstance(Kind, Id)
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
            .WithActions(_actions)
            .ToResourceEntry();
        
        Assert.That(resourceEntry.Resource.Kind, Is.EqualTo(Kind));
        Assert.That(resourceEntry.Resource.Id, Is.EqualTo(Id));
        Assert.That(resourceEntry.Resource.PolicyVersion, Is.EqualTo(PolicyVersion));
        
        Assert.That(resourceEntry.Resource.Attr["lonelyAttr"], Is.EqualTo(_lonelyAttr.ToValue()));
        Assert.That(resourceEntry.Resource.Attr["stringAttr"], Is.EqualTo(_stringAttr.ToValue()));
        Assert.That(resourceEntry.Resource.Attr["boolAttr"], Is.EqualTo(_boolAttr.ToValue()));
        Assert.That(resourceEntry.Resource.Attr["nullAttr"], Is.EqualTo(_nullAttr.ToValue()));
        Assert.That(resourceEntry.Resource.Attr["doubleAttr"], Is.EqualTo(_doubleAttr.ToValue()));
        Assert.That(resourceEntry.Resource.Attr["listAttr"], Is.EqualTo(_listAttr.ToValue()));
        Assert.That(resourceEntry.Resource.Attr["mapAttr"], Is.EqualTo(_mapAttr.ToValue()));
        
        Assert.That(resourceEntry.Actions.Count, Is.EqualTo(_actions.Length));
        Assert.That(resourceEntry.Actions[0], Is.EqualTo(_actions[0]));
        Assert.That(resourceEntry.Actions[1], Is.EqualTo(_actions[1]));
        Assert.That(resourceEntry.Actions[2], Is.EqualTo(_actions[2]));
    }
}