using DTO;

namespace Simulator.IBussinesLogic;

public interface IMethodService
{
    void CreateMethod(MethodDTO method);
    void AsociateMethod(Guid idClass, MethodDTO method);
    void UpdateMethod(Guid id, MethodDTO method);
    void DeleteMethod(Guid id);
}
