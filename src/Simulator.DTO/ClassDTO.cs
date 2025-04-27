using Simulator.Domain;

namespace Simulator.DTO;

public class ClassDTO
{
    public string Name { get; set; } = string.Empty;
    public KeyWord? Stereotype { get; set; } = null;
    public TypeDTO? HierarchyClass { get; set; } = null;
    public List<AttributeDto> Attributes { get; set; } = [];
    public List<MethodDTO> Methods { get; set; } = [];

    public Class ToEntity()
    {
        var hierarchy = HierarchyClass?.ToEntity();
        var attributes = Attributes.Select(attr => attr.ToEntity()).ToList();
        var methods = Methods.Select(method => method.ToEntity()).ToList();

        var aClass = new Class(Stereotype, hierarchy, attributes, methods, Name);

        return aClass;
    }
}
