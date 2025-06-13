using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface IParameterService
{
    Guid CreateParameter(ParameterDTO parameter);
    void SetInvokeMethodToParameter(Guid id, Guid idInvokeGuid);
    List<ParameterDTO> GetAllParameters();
}
