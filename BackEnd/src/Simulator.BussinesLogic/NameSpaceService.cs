using System;
using Simulator.BussinesLogic.Exception;
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
        ValidateNameSpaceExistence(id);
        ValidateNameSpaceExistence(idFather);
        ValidateDiferentNameSpaces(id, idFather);
        var nameSpace = nameSpaceRepository.Find(id);
        var father = nameSpaceRepository.Find(idFather);
        nameSpace.Father = father;
        nameSpaceRepository.Update(nameSpace);
    }

    public void AsociateToClass(Guid id, Guid idClass)
    {
        ValidateNameSpaceExistence(id);
        ValidateClassExistence(idClass);
        var nameSpace = nameSpaceRepository.Find(id);
        var classEntity = classRepository.Find(idClass);
        ValidateClassDontHaveNameSpace(classEntity);
        classEntity.NameSpace = nameSpace;
        classRepository.Update(classEntity);
    }

    private void ValidateClassDontHaveNameSpace(Class idClass)
    {
        if(idClass.NameSpace != null)
        {
            throw new NameSpaceException("Class already has a NameSpace");
        }
    }

    public void AsociateToInterface(Guid id, Guid idInterface)
    {
    }

    private void ValidateUniqueNameSpace(string nameSpace)
    {
        var nameSpaces = nameSpaceRepository.FindAll();
        foreach(var ns in nameSpaces)
        {
            if(ns.Name == nameSpace)
            {
                throw new NameSpaceException("NameSpace already exists");
            }
        }
    }

    private void ValidateClassExistence(Guid idClass)
    {
        var classEntity = classRepository.Find(idClass);
        if(classEntity == null)
        {
            throw new NameSpaceException("Class does not exist");
        }
    }

    private void ValidateNameSpaceExistence(Guid id)
    {
        var nameSpace = nameSpaceRepository.Find(id);
        if(nameSpace == null)
        {
            throw new NameSpaceException("NameSpace does not exist");
        }
    }

    private void ValidateDiferentNameSpaces(Guid id, Guid idFather)
    {
        if(id == idFather)
        {
            throw new NameSpaceException("Cannot add the same NameSpace as a father");
        }
    }
}
