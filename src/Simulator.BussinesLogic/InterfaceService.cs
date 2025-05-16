using System;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class InterfaceService(
    IInterfaceRepository interfaceRepository) : IInterfaceService
{
    private readonly IInterfaceRepository _interfaceRepository = interfaceRepository;

    public Guid CreateInterface(InterfaceDTO interfaceDto)
    {
        var anInterface = interfaceDto.ToEntity();
        _interfaceRepository.Add(anInterface);
        return anInterface.Id;
    }

    public void DeleteInterface(Guid id)
    {
        throw new NotImplementedException();
    }
}
