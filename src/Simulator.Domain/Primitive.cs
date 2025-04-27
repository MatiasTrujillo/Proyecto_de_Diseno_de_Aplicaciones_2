namespace Simulator.Domain;

public class Primitive : Type
{
    public static PrimitiveType PrimitiveType { get; set; }

    public Primitive()
    {
        PrimitiveType = PrimitiveType.Int;
    }

    public Primitive(PrimitiveType primitiveType)
    {
        PrimitiveType = primitiveType;
    }
}
