using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class AttributeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Accessibility Accessibility { get; set; }
    public Type Type { get; set; } = null;

    public Attribute ToEntity()
    {
        return new Attribute(Id, Name, Accessibility, Type);
    }

    public static AttributeDto? FromEntity(Attribute? anAttribute)
    {
        return new AttributeDto()
        {
            Id = anAttribute.Id,
            Name = anAttribute.Name,
            Accessibility = anAttribute.Accessibility,
            Type = anAttribute.Type
        };
    }
}
