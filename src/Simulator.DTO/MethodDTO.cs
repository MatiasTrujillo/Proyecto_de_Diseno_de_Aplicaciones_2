using Simulator.Domain;

namespace Simulator.DTO;

public class MethodDTO
{
    public string Name { get; set; } = string.Empty;
    public Accessibility Accessibility { get; set; }
    public KeyWord? Stereotype { get; set; }
    public TypeDTO? ReturnType { get; set; } = new TypeDTO();

    public Method ToEntity()
    {
        var returnType = ReturnType?.ToEntity() ?? new Primitive();
        return new Method(Name, Accessibility, Stereotype, [], [], [], returnType);
    }
}
