namespace Simulator.Domain;

public class Primitive : Type
{
    public PrimitiveType PrimitiveType { get; set; }

    public Primitive()
        : base(string.Empty)
    {
        PrimitiveType = PrimitiveType.Int;
    }

    public Primitive(PrimitiveType primitiveType)
        : base(string.Empty)
    {
        Name = primitiveType.ToString();
        PrimitiveType = primitiveType;
    }
}
