namespace Simulator.Transformer.Abstractions;

public class TransformerForExporter(Guid referenceId, Guid objectToCreateId, Guid methodId)
{
    public TransformerForExporter()
        : this(Guid.Empty, Guid.Empty, Guid.Empty)
    {
    }

    public Guid ReferenceId { get; set; } = referenceId;
    public Guid ObjectToCreateId { get; set; } = objectToCreateId;
    public Guid MethodId { get; set; } = methodId;
}
