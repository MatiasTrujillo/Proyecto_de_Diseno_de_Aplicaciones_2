using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface IClassService
{
    Guid CreateClass(ClassDTO classDto);
    void UpdateClass(Guid id, UpdateClassDTO uClass);
    void DeleteClass(Guid id);
}
