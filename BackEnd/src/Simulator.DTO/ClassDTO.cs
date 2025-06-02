using Simulator.Domain;

namespace Simulator.DTO;

public class ClassDTO
{
    public string Name { get; set; } = string.Empty;
    public KeyWord? Stereotype { get; set; } = null;
    public string? IdClass { get; set; } = null;

    public Class ToEntity()
    {
        var aClass = new Class(Stereotype, null, null, null, Name);
        return aClass;
    }
}
