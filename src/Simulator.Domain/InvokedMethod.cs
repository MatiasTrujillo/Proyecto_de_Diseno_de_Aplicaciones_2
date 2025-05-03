namespace Simulator.Domain;

public class InvokedMethod
{
    public Guid Id { get; set; }
    public virtual Method Method { get; set; } = null!;
    public Guid MethodId { get; set; }
    public string ReferenceName { get; set; } = null!;

    public InvokedMethod()
    {
        Id = Guid.NewGuid();
        Method = null;
        MethodId = Guid.Empty;
        ReferenceName = string.Empty;
    }
}
