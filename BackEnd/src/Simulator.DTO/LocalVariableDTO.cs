using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class LocalVariableDTO
{
    public string Name { get; set; } = string.Empty;
    public TypeDTO? ReferenceType { get; set; } = null;
    public TypeDTO? InstanceType { get; set; } = null;

    public LocalVariable ToEntity()
    {
        if(ReferenceType == null || InstanceType == null)
        {
            throw new ArgumentNullException(nameof(Type), "The Type property cannot be null.");
        }

        return new LocalVariable(Name, ReferenceType.ToEntity(), InstanceType.ToEntity());
    }
}
