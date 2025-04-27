using Simulator.Domain.Exception;

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
        ValidatName(name);
        Name = name;
        Stereotype = stereotype;
        HierarchyClass = hierarchyClass;
        Attributes = attributes;
        Methods = method;
    }

    private void ValidatName(string name)
    {
        if (name == string.Empty)
        {
            throw new ClassException("The class name cannot be empty.");
        }

        if (!char.IsUpper(name[0]))
        {
            throw new ClassException("The class name must start with an uppercase letter.");
        }

        if (name.Contains(' '))
        {
            throw new ClassException("The class name must not contain spaces.");
        }
    }
}
