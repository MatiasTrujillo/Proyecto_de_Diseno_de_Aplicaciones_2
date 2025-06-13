using Simulator.Domain;
using Type = Simulator.Domain.Type;

public class TypeDTO
{
    public Guid? Id { get; set; } = null;
    public string Name { get; set; } = string.Empty;
    public string Kind { get; set; } = string.Empty;

    public Type ToEntity()
    {
        var kind = Kind.ToLower();
        if(kind.EndsWith("proxy"))
        {
            kind = kind.Replace("proxy", string.Empty);
        }

        return kind switch
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
