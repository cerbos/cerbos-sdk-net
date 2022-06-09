using Cerbos.Sdk.Builders;
using NUnit.Framework;

namespace Cerbos.Sdk.UnitTests.Builders;

public class PrincipalTest
{
    private const string Id = "john"; 
    private const string PolicyVersion = "20210210"; 
    private readonly AttributeValue _lonelyAttr = AttributeValue.StringValue("lonely"); 
    private readonly AttributeValue _boolAttr = AttributeValue.BoolValue(true); 
    private readonly AttributeValue _stringAttr = AttributeValue.StringValue("GB"); 
    private readonly AttributeValue _nullAttr = AttributeValue.NullValue(); 
    private readonly AttributeValue _doubleAttr = AttributeValue.DoubleValue(1.32);
    private readonly string[] _roles = new[] { "employee", "manager", "admin" };
    
    [Test]
    public void Test()
    {
        var principal = Principal.NewInstance(Id, _roles[0], _roles[1])
            .WithPolicyVersion(PolicyVersion)
            .WithAttribute("lonelyAttr", _lonelyAttr)
            .WithAttributes(new()
                {
                    {"boolAttr", _boolAttr},
                    {"stringAttr", _stringAttr},
                    {"nullAttr", _nullAttr},
                    {"doubleAttr", _doubleAttr},
                }
            )
            .WithRoles(_roles[2])
            .ToPrincipal();
        
        Assert.That(principal.Id, Is.EqualTo(Id));
        Assert.That(principal.PolicyVersion, Is.EqualTo(PolicyVersion));
        
        Assert.That(principal.Attr["lonelyAttr"], Is.EqualTo(_lonelyAttr.ToValue()));
        Assert.That(principal.Attr["stringAttr"], Is.EqualTo(_stringAttr.ToValue()));
        Assert.That(principal.Attr["boolAttr"], Is.EqualTo(_boolAttr.ToValue()));
        Assert.That(principal.Attr["nullAttr"], Is.EqualTo(_nullAttr.ToValue()));
        Assert.That(principal.Attr["doubleAttr"], Is.EqualTo(_doubleAttr.ToValue()));

        Assert.That(principal.Roles.Count, Is.EqualTo(_roles.Length));
        Assert.That(principal.Roles[0], Is.EqualTo(_roles[0]));
        Assert.That(principal.Roles[1], Is.EqualTo(_roles[1]));
        Assert.That(principal.Roles[2], Is.EqualTo(_roles[2]));
    }
}