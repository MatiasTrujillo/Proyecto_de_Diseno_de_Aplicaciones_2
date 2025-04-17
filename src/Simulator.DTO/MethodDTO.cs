using Simulator.Domain;

namespace Simulator.DTO;

public class MethodDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Accessibility Accessibility { get; set; }
    public KeyWord? Stereotype { get; set; }

    public Method ToEntity()
    {
        return new Method(Id, Name, Accessibility, Stereotype);
    }
}
