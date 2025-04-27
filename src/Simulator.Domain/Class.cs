namespace Simulator.Domain;

public class Class : Type
{
    public string Name { get; set; }
    public KeyWord? Stereotype { get; set; }
    public Class? HierarchyClass { get; set; }
    public List<Attribute> Attributes { get; set; }

    public List<Method> Methods { get; set; }

    public Class()
    {
        Name = string.Empty;
        Stereotype = null;
        HierarchyClass = null;
        Attributes = [];
        Methods = [];
    }

    public Class(string name, KeyWord? stereotype, Class? hierarchyClass, List<Attribute> attributes, List<Method> method)
    {
        Name = name;
        Stereotype = stereotype;
        HierarchyClass = hierarchyClass;
        Attributes = attributes;
        Methods = method;
    }
}
