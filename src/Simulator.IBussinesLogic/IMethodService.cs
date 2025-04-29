using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface IMethodService
{
    Guid CreateMethod(MethodDTO method);
    Guid CreateParameter(ParameterDTO parameter);
    Guid CreateLocalVariable(LocalVariableDTO localVariable);
    void AssociateMethodWithClass(Guid methodId, Guid classId);
    void DeleteMethod(Guid id);
    void AddAMethod(Guid id, Guid idToAdd);
    void AddLocalVariableToMethod(Guid methodId, Guid localVariableId);
    void AddParameterToMethod(Guid methodId, Guid parameterId);
}
