using System;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class NameSpaceService(
    INameSpaceRepository nameSpaceRepository,
    IClassRepository classRepository
    ) : INameSpaceService
{
    public Guid CreateNameSpace(NameSpaceDTO nameSpaceDto)
    {
        var nameSpace = nameSpaceDto.ToEntity();
        nameSpaceRepository.Add(nameSpace);
        return nameSpace.Id;
    }

    public void AsociateToClass(Guid id, Guid idClass)
    {
        var nameSpace = nameSpaceRepository.Find(id);
        var classEntity = classRepository.Find(idClass);
        classEntity.NameSpace = nameSpace;
        classRepository.Update(classEntity);
    }

    public void AsociateToInterface(Guid id, Guid idInterface)
    {
    }
}
