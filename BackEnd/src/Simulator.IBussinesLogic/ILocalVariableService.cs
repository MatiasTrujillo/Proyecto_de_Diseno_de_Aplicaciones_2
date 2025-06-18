using Simulator.Domain;

namespace Simulator.IBussinesLogic;

public interface ILocalVariableService
{
    Guid CreateLocalVariable(LocalVariable localVariable);
    void SetInvokeMethodToLocalVariable(Guid id, Guid idInvokeGuid);
    List<LocalVariable> GetAllLocalVariables();
}
