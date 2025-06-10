namespace Simulator.Domain;

public abstract class Type : NamespaceComponent
{
    public virtual List<Method>? Methods { get; set; }

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
