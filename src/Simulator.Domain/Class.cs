namespace Simulator.Domain;

public class Class
{
    public string Name { get; set; }
    public KeyWord? Stereotype { get; set; }
    public Class? HierarchyClass { get; set; }
    public List<Attribute> Attributes { get; set; } = new();

    // public List<Method> Method { get; set; } = new();

    public Class()
    {
        Name = string.Empty;
        Stereotype = null;
        HierarchyClass = null;
        Attributes = new List<Attribute>();
    }

    public Class(string name, KeyWord? stereotype, Class? hierarchyClass, List<Attribute> attributes)
    {
        Name = name;
        Stereotype = stereotype;
        HierarchyClass = hierarchyClass;
        Attributes = attributes;
    }
}
