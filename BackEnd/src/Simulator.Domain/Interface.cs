namespace Simulator.Domain;

public class Interface : Type
{
    public virtual List<MethodSignature> MethodSignatures { get; set; }

    public Interface(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        MethodSignatures = [];
    }

    public Interface(string name, List<MethodSignature> methodSignatures)
    {
        Id = Guid.NewGuid();
        Name = name;
        MethodSignatures = methodSignatures;
    }

    public Interface()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        MethodSignatures = [];
    }

    public override Guid Id { get; set; }
}
