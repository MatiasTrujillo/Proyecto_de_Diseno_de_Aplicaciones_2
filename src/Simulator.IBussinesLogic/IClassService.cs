using DTO;

namespace Simulator.IBussinesLogic;

public interface IClassService
{
    void CreateClass(ClassDTO classDto);
    void UpdateClass(Guid id, ClassDTO classDto);
    void DeleteClass(Guid id);
}
