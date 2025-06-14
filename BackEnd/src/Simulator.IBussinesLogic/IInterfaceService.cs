using Simulator.Domain;
using Simulator.UpdateDTO;

namespace Simulator.IBussinesLogic;

public interface IInterfaceService
{
    Guid CreateInterface(Interface interfaceDto);
    void DeleteInterface(Guid id);
    void UpdateInterface(Guid id, UpdateInterfaceDTO uInterfaceDto);
    List<Interface> GetInterfaces();
}
