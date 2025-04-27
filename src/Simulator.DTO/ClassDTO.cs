using Simulator.Domain;

namespace Simulator.DTO;

public class ClassDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public KeyWord? Stereotype { get; set; } = null;
    public ClassDTO? HirarchyClass { get; set; } = null;
    public List<AttributeDto> Attributes { get; set; } = [];

    // public List<MethodDTO> Method { get; set; } = new() ;

    public Class ToEntity()
    {
        var hierarchy = HirarchyClass?.ToEntity();
        var attributes = Attributes.Select(attr => attr.ToEntity()).ToList();

        var aClass = new Class(Name, Stereotype, hierarchy, attributes, []);
        aClass.Id = Id;

        return aClass;
    }
}
