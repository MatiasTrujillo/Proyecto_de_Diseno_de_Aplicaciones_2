namespace Simulator.Domain;

public class Attribute
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public Accessibility Accessibility { get; set; }

    public Attribute()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Accessibility = Accessibility.Public;
    }

    public Attribute(Guid id, string name, Accessibility accessibility)
    {
        Id = id;
        Name = name;
        Accessibility = accessibility;
    }
}
