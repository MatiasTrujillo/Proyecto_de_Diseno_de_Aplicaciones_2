namespace Simulator.Domain;

public class Attribute
{
    public Guid Id { get; set; }
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
        ValidateAttributes(name);
        Id = id;
        Name = name;
        Accessibility = accessibility;
    }

    private void ValidateAttributes(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty");
        }

        if(name.Length > 50)
        {
            throw new ArgumentException("Name cannot exceed 50 characters");
        }
    }
}
