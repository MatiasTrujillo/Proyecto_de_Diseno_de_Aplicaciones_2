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
        ReturnType = new Void();
    }

    public Method(Guid id, string? name, Accessibility accessibility, KeyWord? stereotype)
    {
        Id = id;
        Name = name;
        Accessibility = accessibility;
        Methods = [];
        Parameters = [];
        LocalVariables = [];
        Stereotype = stereotype;
        ReturnType = new Void();
    }

    public Method(Guid id, string? name, Accessibility accessibility, KeyWord? stereotype, List<Method> methods, List<Parameter> parameters,
        List<LocalVariable> localVariables, Type returnType)
    {
        Id = id;
        Name = name;
        Accessibility = accessibility;
        Methods = methods;
        Parameters = parameters;
        LocalVariables = localVariables;
        Stereotype = stereotype;
        ReturnType = returnType;
    }
}
