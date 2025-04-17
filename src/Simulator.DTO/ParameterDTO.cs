using Simulator.Domain;

namespace Simulator.DTO;

public class ParameterDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public Parameter ToEntity()
    {
        return new Parameter(Id, Name);
    }
}
