using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.DTO;

public class AttributeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Accessibility Accessibility { get; set; }

    public Attribute ToEntity()
    {
        return new Attribute(Id, Name, Accessibility);
    }
}
