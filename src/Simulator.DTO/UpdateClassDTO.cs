namespace Simulator.DTO;

public class UpdateClassDTO
{
    public string? Name { get; set; } = string.Empty;
    public string? Stereotype { get; set; } = string.Empty;
    public Guid? IdClass { get; set; } = null;
}
