namespace Simulator.Domain;

public class MethodSignature
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Type? ReturnType { get; set; }
    public virtual List<Parameter>? Parameters { get; set; }

    public MethodSignature()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        ReturnType = new Primitive();
    }

    public MethodSignature(string name, Type? returnType)
    {
        Id = Guid.NewGuid();
        Name = name;
        ReturnType = returnType;
        Parameters = [];
    }

    public MethodSignature(string name, Type? returnType, List<Parameter> parameters)
    {
        Id = Guid.NewGuid();
        Name = name;
        ReturnType = returnType;
        Parameters = parameters;
    }
}
