using DTO;

namespace Simulator.IBussinesLogic;

public interface IMethodService
{
    void CreateMethod(MethodDTO method);
    void AssociateMethodWithClass(Guid methodId, Guid classId);
    void DeleteMethod(Guid id);
}
