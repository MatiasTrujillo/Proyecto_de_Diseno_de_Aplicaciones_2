using System;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class NameSpaceService(
    INameSpaceRepository nameSpaceRepository,
    IClassRepository classRepository,
    IInterfaceRepository interfaceRepository
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
        ValidateNoCircularReference(id, idFather);

        var nameSpace = nameSpaceRepository.Find(id);
        var father = nameSpaceRepository.Find(idFather);

        nameSpace.Father = father;
        father.AddChild(nameSpace); // Composite: Agrega como hijo también
        nameSpaceRepository.Update(father);
        nameSpaceRepository.Update(nameSpace);
    }

    public void AssociateNameSpaceToClass(Guid nameSpaceId, Guid classId)
    {
        ValidateNameSpaceExistence(nameSpaceId);

        var nameSpace = nameSpaceRepository.Find(nameSpaceId);
        var classEntity = classRepository.Find(classId);

        if(classEntity == null)
        {
            throw new NameSpaceException("Class does not exist");
        }

        classEntity.NameSpace = nameSpace;
        classRepository.Update(classEntity);
    }

    public void AssociateNameSpaceToInterface(Guid nameSpaceId, Guid interfaceId)
    {
        ValidateNameSpaceExistence(nameSpaceId);

        var nameSpace = nameSpaceRepository.Find(nameSpaceId);
        var interfaceEntity = interfaceRepository.Find(interfaceId);

        if(interfaceEntity == null)
        {
            throw new NameSpaceException("Interface does not exist");
        }

        interfaceEntity.NameSpace = nameSpace;
        interfaceRepository.Update(interfaceEntity);
    }

    public void RemoveNameSpace(Guid id)
    {
        var nameSpace = nameSpaceRepository.Find(id);
        if(nameSpace == null)
        {
            throw new NameSpaceException("NameSpace does not exist");
        }

        if(nameSpace.Father != null)
        {
            var father = nameSpace.Father;
            father.RemoveChild(nameSpace);
            nameSpaceRepository.Update(father);
        }

        nameSpaceRepository.Delete(nameSpace);
    }

    public void AddChildToNameSpace(Guid parentId, Guid childId)
    {
        var nc = nameSpaceRepository.FindAllNamespaceComponents();
        var nameSpace = nc.Select(nct => nct)
            .FirstOrDefault(ns => ns.Id == childId);
        ValidateNameSpaceComponentExistence(nameSpace);
        ValidateNameSpaceExistence(parentId);
        ValidateDiferentNameSpaces(parentId, childId);
        ValidateNoCircularReference(parentId, childId);

        var parent = nameSpaceRepository.Find(parentId);
        var child = nameSpaceRepository.Find(childId);

        parent.AddChild(child);
        child.Father = parent;
        nameSpaceRepository.Update(parent);
        nameSpaceRepository.Update(child);
    }

    public void RemoveChildFromNameSpace(Guid parentId, Guid childId)
    {
        var nc = nameSpaceRepository.FindAllNamespaceComponents();
        var nameSpace = nc.Select(nct => nct)
            .FirstOrDefault(ns => ns.Id == childId);
        ValidateNameSpaceExistence(parentId);
        ValidateNameSpaceComponentExistence(nameSpace);

        var parent = nameSpaceRepository.Find(parentId);

        var child = nameSpaceRepository.Find(childId);

        if(!parent.Childs.Contains(child))
        {
            throw new NameSpaceException("Child is not part of the specified parent");
        }

        parent.RemoveChild(child);
        child.Father = null;

        nameSpaceRepository.Update(parent);
        nameSpaceRepository.Update(child);
    }

    private void ValidateUniqueNameSpace(string name)
    {
        var nameSpaces = nameSpaceRepository.FindAll();
        foreach(var ns in nameSpaces)
        {
            if(ns.Name == name)
            {
                throw new NameSpaceException("NameSpace already exists");
            }
        }
    }

    private void ValidateNameSpaceComponentExistence(NamespaceComponent? nameSpace)
    {
        if(nameSpace == null)
        {
            throw new NameSpaceException("NameSpace does not exist");
        }
    }

    private void ValidateNameSpaceExistence(Guid id)
    {
        var nc = nameSpaceRepository.Find(id);
        if(nc == null)
        {
            throw new NameSpaceException("NameSpace does not exist");
        }
    }

    private void ValidateDiferentNameSpaces(Guid id, Guid idOther)
    {
        if(id == idOther)
        {
            throw new NameSpaceException("Cannot relate a NameSpace with itself");
        }
    }

    private void ValidateNoCircularReference(Guid id, Guid potentialParentId)
    {
        var current = nameSpaceRepository.Find(potentialParentId);
        while(current != null)
        {
            if(current.Id == id)
            {
                throw new NameSpaceException("Circular reference detected");
            }

            current = current.Father;
        }
    }
}
