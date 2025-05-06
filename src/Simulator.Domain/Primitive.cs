namespace Simulator.Domain;

public class Primitive : Type
{
    public PrimitiveType PrimitiveType { get; set; }
    public virtual List<Method> Methods { get; } = new();

    public Primitive()
        : base(string.Empty)
    {
        PrimitiveType = PrimitiveType.Int;
    }

    public Primitive(PrimitiveType primitiveType)
        : base(string.Empty)
    {
        PrimitiveType = primitiveType;
    }
}
