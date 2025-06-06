namespace Simulator.Domain;

public class Interface : Type
{
    public virtual List<MethodSignature> MethodSignatures { get; set; }

    public Interface(string name)
    {
        Name = name;
        MethodSignatures = [];
    }

    public Interface(string name, List<MethodSignature> methodSignatures)
    {
        Name = name;
        MethodSignatures = methodSignatures;
    }

    public Interface()
    {
        Name = string.Empty;
        MethodSignatures = [];
    }
}
