using Simulator.Domain;

namespace Simulator.DTO;

public class InvokedMethodDTO
{
    public Guid MethodId { get; set; } = Guid.Empty;
    public string ReferenceName { get; set; } = string.Empty;

    public InvokedMethod ToEntity()
    {
        return new InvokedMethod(MethodId, ReferenceName);
    }
}
