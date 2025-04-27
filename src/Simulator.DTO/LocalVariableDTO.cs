using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class LocalVariableDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public Type LocalVariableType { get; set; } = new Primitive();
    public LocalVariable ToEntity()
    {
        return new LocalVariable(Name, LocalVariableType);
    }
}
