namespace Simulator.Domain;

public class Method
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Accessibility Accessibility { get; set; }
    public List<Method> Methods { get; set; }
    /*
    TODO List:
    public Type ReturnType { get; set; }
    public KeyWord? Stereotype { get; set; }
    public List<Parameters> Parameters { get; set; }
    public List<LocalVariable> LocalVariables { get; set; }
    */

    public Method()
	{
        Id = Guid.NewGuid();
        Name = string.Empty;
        Accessibility = Accessibility.Public;
        Methods = new List<Method>();
    }

    public Method(Guid id, string name, Accessibility accessibility)
    {
        Id = id;
        Name = name;
        Accessibility = accessibility;
        Methods = new List<Method>();
    }
}
