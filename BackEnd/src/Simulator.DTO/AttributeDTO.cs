using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.DTO;

public class AttributeDto
{
    public string Name { get; set; } = string.Empty;
    public Accessibility Accessibility { get; set; } = Accessibility.Public;
    public TypeDTO? ReferenceType { get; set; } = null;
    public TypeDTO? InstanceType { get; set; } = null;
    public KeyWord? Stereotype { get; set; } = null;

    public Attribute ToEntity()
    {
        if(ReferenceType == null || InstanceType == null)
        {
            throw new ArgumentNullException(nameof(ReferenceType), "The Type property cannot be null.");
        }

        return new Attribute(Name, Accessibility, ReferenceType.ToEntity(), InstanceType.ToEntity(), Stereotype);
    }
}
