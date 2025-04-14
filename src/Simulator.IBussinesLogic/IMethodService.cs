using DTO;

namespace Simulator.IBussinesLogic;

public interface IMethodService
{
    void CreateMethod(MethodDTO method);
    void DeleteMethod(Guid id);
}
