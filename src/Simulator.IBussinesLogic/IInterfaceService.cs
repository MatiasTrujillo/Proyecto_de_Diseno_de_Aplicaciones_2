using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface IInterfaceService
{
    Guid CreateInterface(InterfaceDTO interfaceDto);
    void DeleteInterface(Guid id);

    // void UpdateInterface(Guid id, UpdateInterfaceDTO uInterfaceDto);
}
