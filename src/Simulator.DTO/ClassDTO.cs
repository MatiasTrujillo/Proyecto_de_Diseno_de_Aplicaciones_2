using Simulator.Domain;

namespace Simulator.DTO;

public class ClassDTO
{
    public string Name { get; set; } = string.Empty;
    public KeyWord? Stereotype { get; set; } = null;
    public NameSpaceDTO? NameSpace { get; set; } = null;

    public Class ToEntity()
    {
        if(NameSpace == null)
        {
            throw new ArgumentNullException(nameof(NameSpace), "The Type property cannot be null.");
        }

        var nameSpace = NameSpace.ToEntity();
        var aClass = new Class(Stereotype, null, null, null, Name, nameSpace);
        return aClass;
    }
}
