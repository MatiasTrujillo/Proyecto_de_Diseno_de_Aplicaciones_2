using System;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
namespace Simulator.BussinesLogic;

public class ClassService(IClassRepository classRepository, IAttributeRepository attributeRepository, IMethodRepository methodRepository) : IClassService
{
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IMethodRepository _methodRepository = methodRepository;

    public Guid CreateClass(ClassDTO classDto)
    {
        var aClass = classDto.ToEntity();
        ValidateClassNameIsUnique(aClass.Name);
        _classRepository.Add(aClass);
        return aClass.Id;
    }

    public void DeleteClass(Guid id)
    {
        ValidateClassIdExists(id);
        ValidateClassIsNotInUse(id);
        _classRepository.Delete(id);
    }

    public void UpdateClass(Guid id, UpdateClassDTO uClass)
    {
        ValidateClassIdExists(id);

        var classT = _classRepository.Find(id);

        if (!string.IsNullOrWhiteSpace(uClass.Name))
        {
            ValidateName(uClass.Name);
            if (uClass.Name != classT.Name)
            {
                ValidateClassNameIsUnique(uClass.Name);
                classT.Name = uClass.Name;
            }
        }

        if (!string.IsNullOrWhiteSpace(uClass.Stereotype))
        {
            ValidateStereotype(uClass.Stereotype, classT);
        }

        if (uClass.IdClass.HasValue)
        {
            if (uClass.IdClass.Value != Guid.Empty)
            {
                if (classT.HierarchyClass == null || classT.HierarchyClass.Id != uClass.IdClass.Value)
                {
                    ValidateClassIdExists(uClass.IdClass.Value);
                    ValidateClassIsNotInUse(id);
                    var heritageClass = _classRepository.Find(uClass.IdClass.Value);
                    classT.HierarchyClass = heritageClass;
                }
            }
            else
            {
                classT.HierarchyClass = null;
            }
        }

        _classRepository.Update(classT);
    }

    private static void ValidateStereotype(string stereotype, Class classT)
    {
        if(stereotype.ToLower() == "abstract")
        {
            classT.Stereotype = KeyWord.Abstract;
        }
        else if(stereotype.ToLower() == "sealed")
        {
            classT.Stereotype = KeyWord.Sealed;
        }
        else
        {
            throw new InvalidClassException("The stereotype is not valid");
        }
    }

    private void ValidateClassIsNotInUse(Guid id)
    {
        var classT = _classRepository.Find(id);
        var classes = _classRepository.FindAll();
        var attributeT = _attributeRepository.FindAll();
        var methods = _methodRepository.FindAll();
        if(classes.Any(c => c.HierarchyClass != null && c.HierarchyClass.Id == id) ||
           attributeT.Any(a => a.Type == classT) ||
           methods.Any(m => m.ReturnType == classT))
        {
            throw new ClassInUseException("Class is associated with another class, attribute or method");
        }
    }

    private void ValidateClassNameIsUnique(string name)
    {
        var classes = _classRepository.FindAll();

        if(classes.Any(classT => classT.Name == name))
        {
            throw new InvalidClassException("There is already a class with this name");
        }
    }

    private void ValidateClassIdExists(Guid id)
    {
        var classes = _classRepository.FindAll();

        if(!classes.Exists(classT => classT.Id == id))
        {
            throw new ClassNotFoundException("There is no class with this id");
        }
    }

    private void ValidateName(string name)
    {
        if(name == string.Empty)
        {
            throw new InvalidClassException("The class name cannot be empty.");
        }

        if(!char.IsUpper(name[0]))
        {
            throw new InvalidClassException("The class name must start with an uppercase letter.");
        }

        if(name.Contains(' '))
        {
            throw new InvalidClassException("The class name must not contain spaces.");
        }
    }
}
