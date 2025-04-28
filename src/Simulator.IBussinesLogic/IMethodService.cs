using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface IMethodService
{
    Guid CreateMethod(MethodDTO method);
    void AssociateMethodWithClass(Guid methodId, Guid classId);
    void DeleteMethod(Guid id);
    void AddAMethod(Guid id, Guid idToAdd);
    void AddLocalVariableToMethod(Guid methodId, LocalVariableDTO localVariable);
    void AddParameterToMethod(Guid methodId, ParameterDTO parameter);
}
