using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public static class TypeExtensions
{
    public static TypeDTO ToDTO(this Type type)
    {
        var kind = type switch
        {
            Class => "class",
            Interface => "interface",
            Primitive => "primitive",
            _ => "unknown"
        };

        return new TypeDTO
        {
            Name = type.Name,
            Kind = kind
        };
    }
}
