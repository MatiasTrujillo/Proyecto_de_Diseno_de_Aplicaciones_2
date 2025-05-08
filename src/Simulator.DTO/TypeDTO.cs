using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class TypeDTO
{
    public string Name { get; set; } = string.Empty;
    public Type ToEntity()
    {
        return new Type(Name);
    }
}
