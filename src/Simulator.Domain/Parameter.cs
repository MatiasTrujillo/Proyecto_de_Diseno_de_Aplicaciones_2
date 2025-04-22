namespace Simulator.Domain;

public class Parameter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Type ParameterType { get; set; }

    public Parameter()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        ParameterType = new Primitive();
    }

    public Parameter(Guid id, string name, Type parameterType)
    {
        Id = id;
        Name = name;
        ParameterType = parameterType;
    }
}
