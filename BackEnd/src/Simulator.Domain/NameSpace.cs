namespace Simulator.Domain;

public class NameSpace : INamespaceComponent
{
    public override Guid Id { get; set; }
    public string Name { get; set; }
    public virtual NameSpace? Father { get; set; }

    public NameSpace()
    {
        Name = string.Empty;
        Father = null;
    }

    public NameSpace(string name, NameSpace? father)
    {
        Id = Guid.NewGuid();
        Name = name;
        Father = father;
    }
}
