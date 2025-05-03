using Simulator.Domain;

namespace Simulator.DTO;

public class InvokedMethodDTO
{
    public Guid MethodId { get; set; }
    public string ReferenceName { get; set; } = null!;

    public InvokedMethod ToEntity()
    {
        return new InvokedMethod(MethodId, ReferenceName);
    }
}
