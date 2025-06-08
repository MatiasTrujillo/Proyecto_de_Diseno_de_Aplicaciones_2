using Simulator.Domain.Exception;

namespace Simulator.Domain;

public class Attribute
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Accessibility Accessibility { get; set; }
    public virtual Type? ReferenceType { get; set; }
    public virtual Type? InstanceType { get; set; }
    public virtual List<InvokedMethod>? InvokedMethods { get; set; }
    public KeyWord? Stereotype { get; set; }

    public Attribute()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Accessibility = Accessibility.Public;
        ReferenceType = new Primitive(PrimitiveType.Int);
        InvokedMethods = [];
    }

    public Attribute(string name, Accessibility accessibility, Type referenceType)
    {
        ValidateAttributes(name);
        Id = Guid.NewGuid();
        Name = name;
        Accessibility = accessibility;
        ReferenceType = referenceType;
        InvokedMethods = [];
    }

    public Attribute(string name, Accessibility accessibility, Type referenceType, KeyWord? stereotype)
    {
        ValidateAttributes(name);
        Id = Guid.NewGuid();
        Name = name;
        Accessibility = accessibility;
        ReferenceType = referenceType;
        InvokedMethods = [];
        Stereotype = stereotype;
    }

    public Attribute(string name, Accessibility accessibility, Type referenceType, Type instanceType, KeyWord? stereotype)
    {
        ValidateAttributes(name);
        Id = Guid.NewGuid();
        Name = name;
        Accessibility = accessibility;
        ReferenceType = referenceType;
        InstanceType = instanceType;
        InvokedMethods = [];
        Stereotype = stereotype;
    }

    private static readonly HashSet<string> ReservedKeywords =
    [
        "base",
        "this",
        "Object"
    ];

    private void ValidateAttributes(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new AttributeException("Name cannot be null or empty");
        }

        if(name.Length > 50)
        {
            throw new AttributeException("Name cannot exceed 50 characters");
        }

        if(ReservedKeywords.Contains(name))
        {
            throw new AttributeException($"The name is a reserved keyword and is not allowed.");
        }
    }
}
