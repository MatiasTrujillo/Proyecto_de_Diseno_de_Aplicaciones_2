namespace Simulator.Domain;

public class Interface : Type
{
    public virtual List<MethodSignature> MethodSignatures { get; set; }
    public virtual NameSpace NameSpace { get; set; }

    public Interface(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        MethodSignatures = [];
        NameSpace = new NameSpace();
    }

    public Interface(string name, List<MethodSignature> methodSignatures)
    {
        Id = Guid.NewGuid();
        Name = name;
        MethodSignatures = methodSignatures;
        NameSpace = new NameSpace();
    }

    public Interface(string name, List<MethodSignature> methodSignatures, NameSpace nameSpace)
    {
        Id = Guid.NewGuid();
        Name = name;
        MethodSignatures = methodSignatures;
        NameSpace = nameSpace;
    }
}
