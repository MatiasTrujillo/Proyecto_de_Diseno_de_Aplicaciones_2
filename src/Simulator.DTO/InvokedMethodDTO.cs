using Simulator.Domain;

namespace Simulator.DTO;

public class InvokedMethodDTO
{
    public MethodDTO Method { get; set; } = null!;
    public string ReferenceName { get; set; } = null!;

    public InvokedMethod ToEntity()
    {
        var method = Method?.ToEntity() ?? new Method();
        return new InvokedMethod
        {
            Method = method,
            ReferenceName = ReferenceName
        };
    }
}
