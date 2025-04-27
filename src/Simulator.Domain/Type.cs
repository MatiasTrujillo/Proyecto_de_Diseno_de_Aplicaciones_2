namespace Simulator.Domain;

public class Type
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Type()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
    }

    public Type(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}
