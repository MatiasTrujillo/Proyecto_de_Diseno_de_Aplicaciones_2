using Simulator.Domain;

namespace Simulator.DTO;

public class InterfaceDTO
{
    public string Name { get; set; } = string.Empty;

    public Interface ToEntity()
    {
        var anInterface = new Interface(Name);
        return anInterface;
    }
}
