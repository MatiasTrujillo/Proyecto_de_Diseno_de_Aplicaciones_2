using Simulator.Domain;
using Type = Simulator.Domain.Type;

public class TypeDTO
{
    public string Name { get; set; } = string.Empty;
    public string Kind { get; set; } = string.Empty;

    public Type ToEntity()
    {
        return Kind.ToLower() switch
        {
            "class" => new Class(null, null, null!, null!, Name),
            "interface" => new Interface(Name),
            "primitive" => new Primitive(),
            _ => throw new InvalidOperationException($"Unknown type kind: {Kind}")
        };
    }
}
