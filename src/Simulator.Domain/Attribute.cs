using Simulator.Domain.Exception;

namespace Simulator.Domain;

public class Attribute
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Accessibility Accessibility { get; set; }
    public virtual Type? Type { get; set; }
    public virtual InvokedMethod? InvokedMethod { get; set; }
    public KeyWord Stereotype { get; set; }

    public Attribute()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Accessibility = Accessibility.Public;
        Type = new Primitive(PrimitiveType.Int);
        InvokedMethod = null;
    }

    public Attribute(string name, Accessibility accessibility, Type type)
    {
        ValidateAttributes(name);
        Id = Guid.NewGuid();
        Name = name;
        Accessibility = accessibility;
        Type = type;
        InvokedMethod = null;
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
