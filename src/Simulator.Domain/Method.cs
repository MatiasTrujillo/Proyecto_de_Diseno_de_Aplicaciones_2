using Simulator.Domain.Exception;

namespace Simulator.Domain;

public class Method
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Accessibility Accessibility { get; set; }
    public virtual List<Method>? Methods { get; set; }
    public virtual List<Parameter>? Parameters { get; set; }
    public virtual List<LocalVariable>? LocalVariables { get; set; }
    public KeyWord? Stereotype { get; set; }
    public virtual Type? ReturnType { get; set; }

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
        ValidateMethodName(name);
        Id = Guid.NewGuid();
        Name = name;
        Accessibility = accessibility;
        Methods = methods;
        Parameters = parameters;
        LocalVariables = localVariables;
        Stereotype = stereotype;
        ReturnType = returnType;
    }

    private void ValidateMethodName(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new MethodException("Method name cannot be empty.");
        }

        if(name.Length > 50)
        {
            throw new MethodException("Method name cannot be longer than 50 characters.");
        }

        if(name.Contains(' '))
        {
            throw new MethodException("Method name cannot contain spaces.");
        }
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
