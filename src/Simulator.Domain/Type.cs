namespace Simulator.Domain;

public class Type
{
    public Guid Id { get; set; }

    public Type() { }

    public Type(Guid id)
    {
        Id = id;
    }
}
