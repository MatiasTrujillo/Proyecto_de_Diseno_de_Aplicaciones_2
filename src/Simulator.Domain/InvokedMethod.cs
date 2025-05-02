namespace Simulator.Domain;

public class InvokedMethod
{
    public Guid Id { get; set; }
    public Method Method { get; set; } = null!;
    public string ReferenceName { get; set; } = null!;
}
