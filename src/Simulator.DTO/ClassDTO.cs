using Simulator.Domain;

namespace Simulator.DTO;

public class ClassDTO
{
    public string Name { get; set; } = string.Empty;
    public KeyWord? Stereotype { get; set; } = null;
    public ClassDTO? HirarchyClass { get; set; } = null;
}
