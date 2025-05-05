namespace Simulator.Domain;

public class Parameter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Type? ParameterType { get; set; }
    public virtual InvokedMethod? InvokedMethod { get; set; }

    public Parameter()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        ParameterType = new Primitive();
        InvokedMethod = null;
    }

    public Parameter(string name, Type parameterType)
    {
        Id = Guid.NewGuid();
        Name = name;
        ParameterType = parameterType;
        InvokedMethod = null;
    }
}
