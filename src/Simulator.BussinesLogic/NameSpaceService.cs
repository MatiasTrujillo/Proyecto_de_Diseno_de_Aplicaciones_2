using System;
using Simulator.Domain;
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
        ValidateUniqueNameSpace(nameSpace.Name);
        nameSpaceRepository.Add(nameSpace);
        return nameSpace.Id;
    }

    public void AddFatherToNameSpace(Guid id, Guid idFather)
    {
        var nameSpace = nameSpaceRepository.Find(id);
        var father = nameSpaceRepository.Find(idFather);
        nameSpace.Father = father;
        nameSpaceRepository.Update(nameSpace);
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

    private void ValidateUniqueNameSpace(string nameSpace)
    {
        var nameSpaces = nameSpaceRepository.FindAll();
        foreach (var ns in nameSpaces)
        {
            if (ns.Name == nameSpace)
            {
                throw new System.Exception("NameSpace already exists");
            }
        }
    }
}
