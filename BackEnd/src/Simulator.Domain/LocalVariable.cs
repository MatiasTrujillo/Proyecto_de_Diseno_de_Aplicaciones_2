using Simulator.Domain.Exception;

namespace Simulator.Domain;

public class LocalVariable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Type? ReferenceType { get; set; }
    public virtual Type? InstanceType { get; set; }
    public virtual List<InvokedMethod>? InvokedMethods { get; set; }

    public LocalVariable()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        ReferenceType = new Primitive();
        InvokedMethods = [];
    }

    public LocalVariable(string name, Type referenceType)
    {
        ValidateParameter(name);
        Id = Guid.NewGuid();
        Name = name;
        ReferenceType = referenceType;
        InvokedMethods = [];
    }

    public LocalVariable(string name, Type referenceType, Type instanceType)
    {
        ValidateParameter(name);
        Id = Guid.NewGuid();
        Name = name;
        ReferenceType = referenceType;
        InstanceType = instanceType;
        InvokedMethods = [];
    }

    private static readonly HashSet<string> ReservedKeywords =
    [
        "base",
        "this",
        "Object"
    ];

    private void ValidateParameter(string name)
    {
        if(ReservedKeywords.Contains(name))
        {
            throw new LocalVariableException($"The name is a reserved keyword and is not allowed.");
        }
    }
}
