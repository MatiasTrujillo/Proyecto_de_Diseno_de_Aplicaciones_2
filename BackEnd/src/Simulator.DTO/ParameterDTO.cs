using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class ParameterDTO
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public TypeDTO? ReferenceType { get; set; } = null;
    public TypeDTO? InstanceType { get; set; } = null;

    public Parameter ToEntity()
    {
        if(ReferenceType == null || InstanceType == null)
        {
            throw new ArgumentNullException(nameof(ReferenceType), "The Type property cannot be null.");
        }

        return new Parameter(Name, ReferenceType.ToEntity(), InstanceType.ToEntity());
    }
}
