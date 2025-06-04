using Simulator.Domain.Exception;

namespace Simulator.Domain;

public class Parameter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Type? ParameterType { get; set; }
    public virtual List<InvokedMethod>? InvokedMethods { get; set; }

    public Parameter()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        ParameterType = new Primitive();
        InvokedMethods = [];
    }

    public Parameter(string name, Type parameterType)
    {
        ValidateParameter(name);
        Id = Guid.NewGuid();
        Name = name;
        ParameterType = parameterType;
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
