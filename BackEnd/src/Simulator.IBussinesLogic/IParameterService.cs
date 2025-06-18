using Simulator.Domain;

namespace Simulator.IBussinesLogic;

public interface IParameterService
{
    Guid CreateParameter(Parameter parameter);
    void SetInvokeMethodToParameter(Guid id, Guid idInvokeGuid);
    List<Parameter> GetAllParameters();
}
