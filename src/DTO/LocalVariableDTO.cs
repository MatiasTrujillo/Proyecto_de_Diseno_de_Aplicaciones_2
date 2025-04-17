using Simulator.Domain;

namespace DTO;

public class LocalVariableDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public LocalVariable ToEntity()
    {
        return new LocalVariable(Id, Name);
    }
}
