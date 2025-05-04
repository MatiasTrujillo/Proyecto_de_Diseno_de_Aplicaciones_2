namespace Simulator.Domain;

public class InvokedMethod
{
    public Guid Id { get; set; }
    public Guid MethodId { get; set; }
    public string ReferenceName { get; set; } = null!;

    public InvokedMethod()
    {
        Id = Guid.NewGuid();
        MethodId = Guid.Empty;
        ReferenceName = string.Empty;
    }

    public InvokedMethod(Guid methodId, string referenceName)
    {
        Id = Guid.NewGuid();
        MethodId = methodId;
        ReferenceName = referenceName;
    }
}
