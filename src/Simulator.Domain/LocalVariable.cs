using Simulator.Domain.Exception;

namespace Simulator.Domain;

public class LocalVariable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Type? LocalVariableReferenceType { get; set; }

    // public virtual Type? LocalVariableInstanceType { get; set; }
    public virtual InvokedMethod? InvokedMethod { get; set; }

    public LocalVariable()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        LocalVariableReferenceType = new Primitive();
        InvokedMethod = null;
    }

    public LocalVariable(string name, Type localVariableReferenceType)
    {
        ValidateParameter(name);
        Id = Guid.NewGuid();
        Name = name;
        LocalVariableReferenceType = localVariableReferenceType;
        InvokedMethod = null;
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
