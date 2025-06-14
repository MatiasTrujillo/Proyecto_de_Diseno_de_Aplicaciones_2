using Simulator.Domain;
using Simulator.UpdateDTO;
using Type = Simulator.Domain.Type;

namespace Simulator.IBussinesLogic;

public interface IClassService
{
    Guid CreateClass(Class classDto);
    void UpdateClass(Guid id, UpdateClassDTO uClass);
    void DeleteClass(Guid id);
    List<Type> GetTypes();
    List<Class> GetClasses();
}
