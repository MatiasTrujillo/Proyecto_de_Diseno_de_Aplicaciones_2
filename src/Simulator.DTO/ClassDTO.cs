using Simulator.Domain;

namespace Simulator.DTO;

public class ClassDTO
{
    public string Name { get; set; } = string.Empty;
    public KeyWord? Stereotype { get; set; } = null;
    public ClassDTO? HirarchyClass { get; set; } = null;
    public List<AttributeDto> Attributes { get; set; } = new List<AttributeDto>();
    public List<MethodDTO> Methods { get; set; } = new List<MethodDTO>();

    public Class ToEntity()
    {
        var hierarchy = HirarchyClass?.ToEntity();
        var attributes = Attributes.Select(attr => attr.ToEntity()).ToList();
        var methods = Methods.Select(method => method.ToEntity()).ToList();

        var aClass = new Class(Stereotype, hierarchy, attributes, methods, Name);

        return aClass;
    }
}
