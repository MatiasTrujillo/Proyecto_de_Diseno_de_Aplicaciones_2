namespace Simulator.Domain;

public class LocalVariable
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    /*
    TODO:
    public Type LocalVariableType { get; set; }
     */

    public LocalVariable()
	{
        Id = Guid.NewGuid();
        Name = string.Empty;
	}

    public LocalVariable(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
