namespace Simulator.Domain;

public class Attribute(Guid id, string name, Accessibility accessibility)
{
    public Guid Id { get; set; } = id;

    public string Name { get; set; } = name;

    public Accessibility Accessibility { get; set; } = accessibility;
}
