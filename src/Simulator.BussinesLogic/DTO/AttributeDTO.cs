using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.BussinesLogic.DTO;

public class AttributeDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Accessibility Accessibility { get; set; }

    public Attribute ToEntity()
    {
        return new Attribute
        {
            Id = Id,
            Name = Name,
            Accessibility = Accessibility
        };
    }
}
