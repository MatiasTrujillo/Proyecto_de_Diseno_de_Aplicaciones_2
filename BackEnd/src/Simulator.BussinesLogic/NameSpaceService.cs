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

    public void AddChildToNameSpace(Guid parentId, Guid childId)
    {
        var nc = nameSpaceRepository.FindAllNamespaceComponents();
        var nameSpace = nc.Select(nct => nct)
            .FirstOrDefault(ns => ns.Id == childId);
        ValidateNameSpaceComponentExistence(nameSpace);
        if(nameSpace != null && nameSpace.Name == "Object")
        {
            throw new NameSpaceException("A namespace cannot have the name 'Object'.");
        }

        ValidateNameSpaceExistence(parentId);
        ValidateDiferentNameSpaces(parentId, childId);
        ValidateNoCircularReference(parentId, childId);
        var parent = nameSpaceRepository.Find(parentId);

        ValidateAsociateNameSapce(parentId, nameSpace.Name);

        if(nameSpace != null)
        {
            parent.AddChild(nameSpace);
            nameSpace.Father = parent;
        }

        nameSpaceRepository.Update(parent);
        UpdateNameSpaceComponent(nameSpace);
    }

    public void RemoveChildFromNameSpace(Guid parentId, Guid childId)
    {
        var nc = nameSpaceRepository.FindAllNamespaceComponents();
        var nameSpace = nc.Select(nct => nct)
            .FirstOrDefault(ns => ns.Id == childId);
        ValidateNameSpaceExistence(parentId);
        ValidateNameSpaceComponentExistence(nameSpace);

        var parent = nameSpaceRepository.Find(parentId);

        if(nameSpace != null && !parent.Childs.Contains(nameSpace))
        {
            throw new NameSpaceException("Child is not part of the specified parent");
        }

        if(nameSpace != null)
        {
            parent.RemoveChild(nameSpace);
            nameSpace.Father = null;
        }

        nameSpaceRepository.Update(parent);
        UpdateNameSpaceComponent(nameSpace);
    }

    private void ValidateAsociateNameSapce(Guid parentId, string? name = null)
    {
        var parent = nameSpaceRepository.Find(parentId);
        var existeHijoConMismoNombre = parent.Childs.Any(child => name != null && child.Name == name);
        if(existeHijoConMismoNombre)
        {
            throw new NameSpaceException("This namespace already contains a child with the same name.");
        }
    }

    public List<NameSpace> GetAllNameSpace()
    {
        var nameSpaces = nameSpaceRepository.FindAll();
        return nameSpaces.OrderBy(a => a.Name)
            .ToList();
    }

    public List<NamespaceComponent> GetAllNamespaceComponents()
    {
        var components = nameSpaceRepository.FindAllNamespaceComponents()
            .Where(ns => !(ns is Primitive));
        return components.OrderBy(a => a.Name)
            .ToList();
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
            throw new NameSpaceNotFoundException("NameSpace does not exist");
        }
    }

    private void ValidateNameSpaceExistence(Guid id)
    {
        var nc = nameSpaceRepository.Find(id);
        if(nc == null)
        {
            throw new NameSpaceNotFoundException("NameSpace does not exist");
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

    private void UpdateNameSpaceComponent(NamespaceComponent? nameSpace)
    {
        if(nameSpace is NameSpace ns)
        {
            nameSpaceRepository.Update(ns);
        }
        else if(nameSpace is Class c)
        {
            classRepository.Update(c);
        }
        else if(nameSpace is Interface i)
        {
            interfaceRepository.Update(i);
        }
    }
}
