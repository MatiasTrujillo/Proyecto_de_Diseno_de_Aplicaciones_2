using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class AttributeDto
{
    public string Name { get; set; } = string.Empty;
    public Accessibility Accessibility { get; set; }
    public Type Type { get; set; } = null;

    public Attribute ToEntity()
    {
        return new Attribute(Name, Accessibility, Type);
    }

    public static AttributeDto? FromEntity(Attribute? anAttribute)
    {
        return new AttributeDto()
        {
            Name = anAttribute.Name,
            Accessibility = anAttribute.Accessibility,
            Type = anAttribute.Type
        };
    }
}
