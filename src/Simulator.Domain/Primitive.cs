namespace Simulator.Domain;

public class Primitive(PrimitiveType primitiveType) : Type
{
    public PrimitiveType PrimitiveType { get; set; } = primitiveType;
}
