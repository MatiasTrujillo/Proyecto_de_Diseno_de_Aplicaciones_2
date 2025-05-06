using Simulator.Domain.Exception;

namespace Simulator.Domain;

public class LocalVariable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Type? LocalVariableType { get; set; }
    public virtual InvokedMethod? InvokedMethod { get; set; }

    public LocalVariable()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        LocalVariableType = new Primitive();
        InvokedMethod = null;
    }

    public LocalVariable(string name, Type localVariableType)
    {
        ValidateParameter(name);
        Id = Guid.NewGuid();
        Name = name;
        LocalVariableType = localVariableType;
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
