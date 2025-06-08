using Simulator.Domain;
using Simulator.DTO;
using Type = Simulator.Domain.Type;

namespace Simulator.IBussinesLogic;

public interface IClassService
{
    Guid CreateClass(ClassDTO classDto);
    void UpdateClass(Guid id, UpdateClassDTO uClass);
    void DeleteClass(Guid id);
    List<Type> GetTypes();
    List<Class> GetClasses();
    ClassDTO GetClassId(Guid id);
}
