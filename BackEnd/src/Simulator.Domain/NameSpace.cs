namespace Simulator.Domain;

public class NameSpace : NamespaceComponent
{
    public string Name { get; set; }
    public virtual List<NamespaceComponent> Childs { get; set; }

    public NameSpace()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Father = null;
        Childs = [];
    }

    public NameSpace(string name, NameSpace? father)
    {
        Id = Guid.NewGuid();
        Name = name;
        Father = father;
        Childs = [];
    }

    public void AddChild(NamespaceComponent component)
    {
        Childs.Add(component);
    }

    public void RemoveChild(NamespaceComponent component)
    {
        Childs.Remove(component);
    }
}
