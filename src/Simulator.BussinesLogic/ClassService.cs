using System;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class ClassService(
    IClassRepository classRepository,
    IAttributeRepository attributeRepository,
    IMethodRepository methodRepository) : IClassService
{
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IMethodRepository _methodRepository = methodRepository;

    public Guid CreateClass(ClassDTO classDto)
    {
        var aClass = classDto.ToEntity();
        var classes = _classRepository.FindAll();
        var objectClass = classes.FirstOrDefault(c => c.Name == "Object");
        ValidateAccesibility(aClass.Stereotype);
        ValidateClassNameIsUnique(aClass.Name);
        aClass.HierarchyClass = objectClass;
        _classRepository.Add(aClass);
        return aClass.Id;
    }

    public void DeleteClass(Guid id)
    {
        ValidateClassIdExists(id);
        var aClass = _classRepository.Find(id);
        ValidateClassIsNotObject(aClass);
        ValidateClassIsNotInUse(id);
        _classRepository.Delete(id);
    }

    private static void ValidateClassIsNotObject(Class aClass)
    {
        if(aClass is { Name: "Object" })
        {
            throw new InvalidClassException("The 'Object' class cannot be deleted.");
        }
    }

    public void UpdateClass(Guid id, UpdateClassDTO uClass)
    {
        ValidateClassIdExists(id);
        var classT = _classRepository.Find(id);
        ValidateDontUpdateObjectClass(classT);
        ManageName(uClass, classT);
        ManageStereotype(uClass, classT);
        ManageHierarchyClass(id, uClass, classT);
        _classRepository.Update(classT);
    }

    private void ManageStereotype(UpdateClassDTO uClass, Class classT)
    {
        if(!string.IsNullOrWhiteSpace(uClass.Stereotype))
        {
            ValidateStereotype(uClass.Stereotype, classT);
        }
    }

    private void ManageHierarchyClass(Guid id, UpdateClassDTO uClass, Class classT)
    {
        if(uClass.IdClass != null)
        {
            ValidateClassIdExists(uClass.IdClass.Value);
            ValidateClassIsNotInUse(id);
            if(classT.Stereotype == KeyWord.Sealed)
            {
                throw new InvalidClassException("A sealed class cannot inherit.");
            }

            if(uClass.Stereotype == "abstract")
            {
                throw new InvalidClassException("An abstract class cannot inherit from a concrete class.");
            }

            var heritageClass = _classRepository.Find(uClass.IdClass.Value);
            classT.HierarchyClass = heritageClass;
        }
    }

    private void ManageName(UpdateClassDTO uClass, Class classT)
    {
        if(uClass.Name != null)
        {
            ValidateName(uClass.Name);
            if(uClass.Name != classT.Name)
            {
                ValidateClassNameIsUnique(uClass.Name);
                classT.Name = uClass.Name;
            }
        }
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

    private void ValidateAccesibility(KeyWord? stereotype)
    {
        if (stereotype != null && !Enum.IsDefined(typeof(KeyWord), stereotype))
        {
            throw new InvalidClassException("Invalid stereotype value.");
        }
    }

    private static void ValidateDontUpdateObjectClass(Class classT)
    {
        if(classT.Name == "Object")
        {
            throw new InvalidClassException("The 'Object' class cannot be updated.");
        }
    }
}
