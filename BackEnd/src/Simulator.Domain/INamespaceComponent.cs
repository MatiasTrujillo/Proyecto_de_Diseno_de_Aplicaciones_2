namespace Simulator.Domain;

public abstract class INamespaceComponent
{
    public abstract Guid Id { get; set; }

    public INamespaceComponent()
    {
        Id = Guid.NewGuid();
    }
}
