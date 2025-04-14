using Simulator.Domain;

namespace DTO;

public class MethodDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Accessibility Accessibility { get; set; }
    public KeyWord? Stereotype { get; set; }
}
