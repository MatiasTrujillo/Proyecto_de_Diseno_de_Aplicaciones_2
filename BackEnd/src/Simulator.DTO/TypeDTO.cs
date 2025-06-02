using Simulator.Domain;
using Type = Simulator.Domain.Type;

public class TypeDTO
{
    public Guid? IdClass { get; set; } = null;
    public string Name { get; set; } = string.Empty;
    public string Kind { get; set; } = string.Empty;

    public Type ToEntity()
    {
        return Kind.ToLower() switch
        {
            "class" => new Class(null, null, null!, null!, Name),
            "interface" => new Interface(Name),
            "primitive" => new Primitive(ParsePrimitiveType(Name)),
            _ => throw new InvalidOperationException($"Unknown type kind: {Kind}")
        };
    }

    private PrimitiveType ParsePrimitiveType(string name)
    {
        if(Enum.TryParse<PrimitiveType>(name, ignoreCase: true, out var result))
        {
            return result;
        }

        throw new InvalidOperationException($"Invalid primitive type name: {name}");
    }
}
