namespace Simulator.Domain;

public class Attribute
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Accessibility Accessibility { get; set; }
    public Type Type { get; set; }

    public Attribute()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Accessibility = Accessibility.Public;
        Type = new Primitive(PrimitiveType.Int);
    }

    public Attribute(string name, Accessibility accessibility, Type type)
    {
        ValidateAttributes(name);
        Id = Guid.NewGuid();
        Name = name;
        Accessibility = accessibility;
        Type = type;
    }

    private void ValidateAttributes(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty");
        }

        if(name.Length > 50)
        {
            throw new ArgumentException("Name cannot exceed 50 characters");
        }
    }
}
