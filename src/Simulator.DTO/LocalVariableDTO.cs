using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class LocalVariableDTO
{
    public string Name { get; set; } = string.Empty;
    public TypeDTO LocalVariableType { get; set; } = new TypeDTO();
    public LocalVariable ToEntity()
    {
        Type localVariableType = LocalVariableType.ToEntity();
        return new LocalVariable(Name, localVariableType);
    }
}
