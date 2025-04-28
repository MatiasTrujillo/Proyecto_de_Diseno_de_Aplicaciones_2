using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface IClassService
{
    Guid CreateClass(ClassDTO classDto);
    void UpdateNameClass(Guid id, string name);
    void UpdateClassStereotype(Guid id, string stereotype);
    void UpdateClassInherits(Guid id, Guid idClass);
    void DeleteClass(Guid id);
}
