using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface IClassService
{
    void CreateClass(ClassDTO classDto);
    void UpdateNameClass(Guid id, string name);
    void UpdateClassStereotype(Guid id, string stereotype);
    void DeleteClass(Guid id);
}
