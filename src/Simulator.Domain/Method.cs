namespace Simulator.Domain;

public class Method
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Accessibility Accessibility { get; set; }
    public List<Method> Methods { get; set; }
    public List<Parameter> Parameters { get; set; }
    public List<LocalVariable> LocalVariables { get; set; }
    public KeyWord? Stereotype { get; set; }
    /*
    TODO List:
    public Type ReturnType { get; set; }
    */

    public Method()
	{
        Id = Guid.NewGuid();
        Name = string.Empty;
        Accessibility = Accessibility.Public;
        Methods = new List<Method>();
        Parameters = new List<Parameter>();
        LocalVariables = new List<LocalVariable>();
        Stereotype = null;
    }

    public Method(Guid id, string name, Accessibility accessibility, KeyWord? stereotype)
    {
        Id = id;
        Name = name;
        Accessibility = accessibility;
        Methods = new List<Method>();
        Parameters = new List<Parameter>();
        LocalVariables = new List<LocalVariable>();
        Stereotype = stereotype;
    }
}
