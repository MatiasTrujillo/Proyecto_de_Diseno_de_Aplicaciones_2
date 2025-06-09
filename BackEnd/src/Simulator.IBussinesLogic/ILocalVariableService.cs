using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface ILocalVariableService
{
    Guid CreateLocalVariable(LocalVariableDTO localVariable);
    void SetInvokeMethodToLocalVariable(Guid id, Guid idInvokeGuid);
}
