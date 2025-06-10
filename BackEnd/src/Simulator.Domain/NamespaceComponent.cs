using Simulator.Domain;

public abstract class NamespaceComponent
{
    public Guid Id { get; set; }
    public virtual NameSpace? Father { get; set; }
    public string Name { get; set; } = string.Empty;

    public Guid? NameSpaceId { get; set; }
    public virtual NameSpace? NameSpace { get; set; }

    protected NamespaceComponent()
    {
        Id = Guid.NewGuid();
    }
}
