namespace Simulator.Domain;

public class Class
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public KeyWord? Stereotype { get; set; }
    public Class? HierarchyClass { get; set; }
    public List<Attribute> Attributes { get; set; } = new();

    // public List<Method> Method { get; set; } = new();

    public Class()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Stereotype = null;
        HierarchyClass = null;
        Attributes = new List<Attribute>();
    }

    public Class(Guid id, string name, KeyWord? stereotype, Class? hierarchyClass, List<Attribute> attributes)
    {
        Id = id;
        Name = name;
        Stereotype = stereotype;
        HierarchyClass = hierarchyClass;
        Attributes = attributes;
    }
}
