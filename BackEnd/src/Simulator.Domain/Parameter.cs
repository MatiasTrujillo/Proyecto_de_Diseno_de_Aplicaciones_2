using Simulator.Domain.Exception;

namespace Simulator.Domain;

public class Parameter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Type? ReferenceType { get; set; }
    public virtual Type? InstanceType { get; set; }
    public virtual InvokedMethod? InvokedMethod { get; set; }

    public Parameter()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        ReferenceType = new Primitive();
        InvokedMethod = null;
    }

    public Parameter(string name, Type referenceType)
    {
        ValidateParameter(name);
        Id = Guid.NewGuid();
        Name = name;
        ReferenceType = referenceType;
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
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ParameterException("Name cannot be null or empty");
        }

        if(ReservedKeywords.Contains(name))
        {
            throw new ParameterException($"The name is a reserved keyword and is not allowed.");
        }
    }
}
