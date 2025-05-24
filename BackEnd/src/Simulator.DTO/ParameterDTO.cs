using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class ParameterDTO
{
    public string Name { get; set; } = string.Empty;
    public TypeDTO? ParameterType { get; set; } = new TypeDTO();
    public Parameter ToEntity()
    {
        if(ParameterType == null)
        {
            throw new ArgumentNullException(nameof(Type), "The Type property cannot be null.");
        }

        Type parameterType = ParameterType.ToEntity();
        return new Parameter(Name, parameterType);
    }
}
