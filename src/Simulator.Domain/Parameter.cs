namespace Simulator.Domain;

public class Parameter
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    /*
    TODO:
    public Type ParameterType { get; set; }
     */

    public Parameter()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
    }

    public Parameter(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

