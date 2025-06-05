using Simulator.Domain.Exception;

namespace Simulator.Domain;

public class Class : Type
{
    public KeyWord? Stereotype { get; set; }
    public virtual List<Type>? Parents { get; set; }
    public virtual List<Attribute>? Attributes { get; set; }

    public Class()
        : base(string.Empty)
    {
        Methods = [];
        Stereotype = null;
        Parents = [];
        Attributes = [];
    }

    public Class(KeyWord? stereotype, List<Type>? hierarchyClass, List<Attribute> attributes, List<Method> methods, string name)
        : base(name)
    {
        ValidateName(name);
        Name = name;
        Stereotype = stereotype;
        Parents = hierarchyClass;
        Attributes = attributes;
        Methods = methods;
    }

    private void ValidateName(string name)
    {
        if(name == string.Empty)
        {
            throw new ClassException("The class name cannot be empty.");
        }

        if(!char.IsUpper(name[0]))
        {
            throw new ClassException("The class name must start with an uppercase letter.");
        }

        if(name.Contains(' '))
        {
            throw new ClassException("The class name must not contain spaces.");
        }
    }
}
