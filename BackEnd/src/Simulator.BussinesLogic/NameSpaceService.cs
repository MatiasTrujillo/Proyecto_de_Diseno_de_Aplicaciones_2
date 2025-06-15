using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class NameSpaceService(
    INameSpaceRepository nameSpaceRepository,
    IClassRepository classRepository,
    IInterfaceRepository interfaceRepository
) : INameSpaceService
{
    public Guid CreateNameSpace(NameSpace nameSpace)
    {
        ValidateUniqueNameSpace(nameSpace.Name);
        nameSpaceRepository.Add(nameSpace);
        return nameSpace.Id;
    }

    public void AddFatherToNameSpace(Guid id, Guid idFather)
    {
        var nc = nameSpaceRepository.FindAllNamespaceComponents();
        var nameSpace2 = nc.Select(nct => nct)
            .FirstOrDefault(ns => ns.Id == id);
        ValidateNameSpaceComponentExistence(nameSpace2);

        ValidateDiferentNameSpaces(id, idFather);
        ValidateNoCircularReference(id, idFather);
        ValidateNameSpaceExistence(idFather);
        var father = nameSpaceRepository.Find(idFather);
        ValidateAsociateNameSapce(id);

        nameSpace2.Father = father;
        father.AddChild(nameSpace2);
        nameSpaceRepository.Update(father);
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

        var existsInChilds = nameSpace.Childs.Any(child => child.Name == classEntity.Name);

        ValidateChilds(existsInChilds, classEntity);

        classEntity.NameSpace = nameSpace;
        classRepository.Update(classEntity);
    }

    private static void ValidateChilds(bool existsInChilds, Class classEntity)
    {
        if(!existsInChilds)
        {
            throw new NameSpaceException($"The namespace does not contain a child with the name '{classEntity.Name}'");
        }
    }

    public void AssociateNameSpaceToInterface(Guid nameSpaceId, Guid interfaceId)
    {
        ValidateNameSpaceExistence(nameSpaceId);

        var nameSpace = nameSpaceRepository.Find(nameSpaceId);
        var interfaceEntity = interfaceRepository.Find(interfaceId);

        ValidateInterface(interfaceEntity);

        var existsInChilds = nameSpace.Childs.Any(child => child.Name == interfaceEntity.Name);

        ValidateChildsInterface(existsInChilds, interfaceEntity);

        interfaceEntity.NameSpace = nameSpace;
        interfaceRepository.Update(interfaceEntity);
    }

    private static void ValidateChildsInterface(bool existsInChilds, Interface interfaceEntity)
    {
        if(!existsInChilds)
        {
            throw new NameSpaceException($"The namespace does not contain a child with the name '{interfaceEntity.Name}'");
        }
    }

    private static void ValidateInterface(Interface interfaceEntity)
    {
        if(interfaceEntity == null)
        {
            throw new NameSpaceException("Interface does not exist");
        }
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
        ValidateAsociateNameSapce(parentId);

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

        ValidateAsociateNameSapce(parentId);

        if(!parent.Childs.Contains(child))
        {
            throw new NameSpaceException("Child is not part of the specified parent");
        }

        parent.RemoveChild(child);
        child.Father = null;

        nameSpaceRepository.Update(parent);
        nameSpaceRepository.Update(child);
    }

    private void ValidateAsociateNameSapce(Guid parentId)
    {
        var clasesAsociadas = classRepository.FindAll().Any(c => c.NameSpace != null && c.NameSpace.Id == parentId);
        if (clasesAsociadas)
        {
            throw new NameSpaceException("No modifications allowed: the namespace is associated with a class.");
        }

        var interfacesAsociadas = interfaceRepository.FindAll().Any(i => i.NameSpace != null && i.NameSpace.Id == parentId);
        if (interfacesAsociadas)
        {
            throw new NameSpaceException("No modifications allowed: the namespace is associated with a class.");
        }
    }

    public List<NameSpace> GetAllNameSpace()
    {
        var nameSpaces = nameSpaceRepository.FindAll();
        return nameSpaces.ToList();
    }

    public List<NamespaceComponent> GetAllNamespaceComponents()
    {
        var components = nameSpaceRepository.FindAllNamespaceComponents()
            .Where(ns => !(ns is Primitive));
        return components.ToList();
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
