namespace Simulator.Domain;

public class Method
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Accessibility Accessibility { get; set; }
    public List<Method> Methods { get; set; }
    public List<Parameter> Parameters { get; set; }
    public List<LocalVariable> LocalVariables { get; set; }
    public KeyWord? Stereotype { get; set; }
    public Type ReturnType { get; set; }

    public Method()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Accessibility = Accessibility.Public;
        Methods = [];
        Parameters = [];
        LocalVariables = [];
        Stereotype = null;
        ReturnType = new Primitive();
    }

    public Method(string name, Accessibility accessibility, KeyWord? stereotype, List<Method> methods,
        List<Parameter> parameters,
        List<LocalVariable> localVariables, Type returnType)
    {
        Id = Guid.NewGuid();
        Name = name;
        Accessibility = accessibility;
        Methods = methods;
        Parameters = parameters;
        LocalVariables = localVariables;
        Stereotype = stereotype;
        ReturnType = returnType;
    }

    public override bool Equals(object? obj)
    {
        return obj is Method other && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
