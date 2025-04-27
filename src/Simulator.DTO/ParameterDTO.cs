using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class ParameterDTO
{
    public string Name { get; set; } = string.Empty;
    public TypeDTO ParameterType { get; set; } = new TypeDTO();
    public Parameter ToEntity()
    {
        Type parameterType = ParameterType.ToEntity();
        return new Parameter(Name, parameterType);
    }
}
