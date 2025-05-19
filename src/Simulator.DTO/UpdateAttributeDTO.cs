namespace Simulator.DTO;

public class UpdateAttributeDTO
{
    public string? Name { get; set; } = null;
    public string? Accessibility { get; set; } = null;
    public Guid? IdReferenceType { get; set; } = null;
    public Guid? IdInstanceType { get; set; } = null;
    public string? Stereotype { get; set; } = null;
}
