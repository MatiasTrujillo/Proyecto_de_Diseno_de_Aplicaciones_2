using Simulator.Domain;

namespace Simulator.DTO;

public class MethodDTO
{
    public string? Id { get; set; } = null;
    public string Name { get; set; } = string.Empty;
    public Accessibility Accessibility { get; set; } = Accessibility.Public;
    public KeyWord? Stereotype { get; set; } = null;
    public TypeDTO? ReturnType { get; set; } = null;
    public bool IsOverride { get; set; }

    public Method ToEntity()
    {
        var returnType = ReturnType?.ToEntity() ?? null;
        return new Method(Name, Accessibility, Stereotype, [], [], [], returnType, IsOverride);
    }
}
