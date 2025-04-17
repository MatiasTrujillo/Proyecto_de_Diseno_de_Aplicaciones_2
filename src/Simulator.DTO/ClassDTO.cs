using Simulator.Domain;

namespace Simulator.DTO;

public class ClassDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public KeyWord? Stereotype { get; set; } = null;
    public ClassDTO? HirarchyClass { get; set; } = null;
    public List<AttributeDto> Attributes { get; set; } = [];

    // public List<MethodDTO> Method { get; set; } = new();

    public Class ToEntity()
    {
        var aClass = new Class { Id = Id, Name = Name, Stereotype = Stereotype };

        if(HirarchyClass != null)
        {
            aClass.HierarchyClass = HirarchyClass.ToEntity();
        }
        else
        {
            aClass.HierarchyClass = null;
        }

        foreach(var attr in Attributes)
        {
            aClass.Attributes.Add(attr.ToEntity());
        }

        return aClass;
    }
}
