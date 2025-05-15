using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class AttributeDto
{
    public string Name { get; set; } = string.Empty;
    public Accessibility Accessibility { get; set; } = Accessibility.Public;
    public TypeDTO? Type { get; set; } = null;
    public KeyWord Stereotype { get; set; }

    public Attribute ToEntity()
    {
        if(Type == null)
        {
            throw new ArgumentNullException(nameof(Type), "The Type property cannot be null.");
        }

        return new Attribute(Name, Accessibility, Type.ToEntity());
    }
}
