using Simulator.Domain;

namespace Simulator.DTO;

public class NameSpaceDTO
{
    public string Name { get; set; } = string.Empty;

    public NameSpace ToEntity()
    {
        return new NameSpace(Name, null);
    }
}
