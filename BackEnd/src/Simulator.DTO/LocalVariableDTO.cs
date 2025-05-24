using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class LocalVariableDTO
{
    public string Name { get; set; } = string.Empty;
    public TypeDTO? LocalVariableType { get; set; } = null;
    public LocalVariable ToEntity()
    {
        if(LocalVariableType == null)
        {
            throw new ArgumentNullException(nameof(Type), "The Type property cannot be null.");
        }

        Type localVariableType = LocalVariableType.ToEntity();
        return new LocalVariable(Name, localVariableType);
    }
}
