using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class ParameterDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public Type ParameterType { get; set; } = new Primitive();
    public Parameter ToEntity()
    {
        return new Parameter(Name, ParameterType);
    }
}
