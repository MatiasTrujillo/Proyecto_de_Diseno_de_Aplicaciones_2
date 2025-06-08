using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic;

public class ClassService(
    IClassRepository classRepository,
    IAttributeRepository attributeRepository,
    IMethodRepository methodRepository, IInterfaceRepository interfaceRepository) : IClassService
{
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IMethodRepository _methodRepository = methodRepository;
    private readonly IInterfaceRepository _interfaceRepository = interfaceRepository;

    public Guid CreateClass(ClassDTO classDto)
    {
        var aClass = classDto.ToEntity();
        var classes = _classRepository.FindAllTypes();
        var objectClass = classes.FirstOrDefault(c => c.Name == "Object") as Class;
        ValidateStereotypeInCreate(aClass.Stereotype);
        ValidateClassNameIsUnique(aClass.Name);
        aClass.Parents = [objectClass];
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

    public List<Type> GetTypes()
    {
        return _classRepository.FindAllTypes();
    }

    public List<Class> GetClasses()
    {
        return _classRepository.FindAll();
    }

    public ClassDTO GetClassId(Guid id)
    {
        ValidateClassIdExists(id);
        var classT = _classRepository.Find(id);
        var clasDto = new ClassDTO
        {
            Name = classT.Name,
            Stereotype = classT.Stereotype,
            IdClass = classT.Parents?.FirstOrDefault()?.Id.ToString()
        };
        return clasDto;
    }

    private void ManageStereotype(UpdateClassDTO uClass, Class classT)
    {
        ValidateStereotypeInUpdate(uClass.Stereotype, classT);
    }

    private void ManageHierarchyClass(Guid id, UpdateClassDTO uClass, Class classT)
    {
        if(uClass.IdClass != null)
        {
            var isInterface = ValidateParentIdExists(Guid.Parse(uClass.IdClass));
            ValidateClassIsNotInUse(id);
            var heritageClassT = new Class();
            var heritageInterfaceT = new Interface();
            if(isInterface)
            {
                heritageInterfaceT = _interfaceRepository.Find(Guid.Parse(uClass.IdClass));
            }
            else
            {
                heritageClassT = _classRepository.Find(Guid.Parse(uClass.IdClass));
            }

            if(classT.Stereotype == KeyWord.Sealed)
            {
                throw new InvalidClassException("A sealed class cannot inherit from any class.");
            }

            if(classT.Stereotype == KeyWord.Abstract)
            {
                throw new InvalidClassException("An abstract class cannot inherit from a concrete class.");
            }

            if(isInterface)
            {
                classT.Parents.Add(heritageInterfaceT);
                foreach(var signature in heritageInterfaceT.MethodSignatures)
                {
                    if(NotHasImplementedThisSignature(classT, signature))
                    {
                        var methodT = new Method(signature.Name, Accessibility.Public, null, [], signature.Parameters, [], signature.ReturnType, false);
                        classT.Methods.Add(methodT);
                    }
                }
            }
            else
            {
                classT.Parents.Add(heritageClassT);
            }
        }
    }

    private static bool NotHasImplementedThisSignature(Class classT, MethodSignature signature)
    {
        return classT.Methods is null || !classT.Methods.Any(m => m.Name == signature.Name && HasSameParemeters(m, signature) && m.ReturnType == signature.ReturnType);
    }

    private static bool HasSameParemeters(Method aMethod, MethodSignature signature)
    {
        var itHas = aMethod.Parameters.Count == signature.Parameters.Count;
        foreach(var parameter in signature.Parameters)
        {
            itHas = itHas && aMethod.Parameters.Any(p => p.Name == parameter.Name && p.ReferenceType == parameter.ReferenceType);
        }

        return itHas;
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

    private static void ValidateStereotypeInUpdate(string? stereotype, Class classT)
    {
        if(stereotype == null)
        {
            classT.Stereotype = null;
            return;
        }

        if(stereotype.ToLower() == "abstract")
        {
            classT.Stereotype = KeyWord.Abstract;
        }
        else if(stereotype.ToLower() == "sealed")
        {
            classT.Stereotype = KeyWord.Sealed;
        }
        else if(stereotype.ToLower() == "virtual")
        {
            classT.Stereotype = KeyWord.Virtual;
        }
        else if(stereotype.ToLower() == "no change")
        {
            return;
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
        if(classes.Any(c => c.Parents != null && c.Parents.Find(c => c.Id == id) is not null) ||
           attributeT.Any(a => a.ReferenceType == classT) ||
           methods.Any(m => m.ReturnType == classT))
        {
            throw new ClassInUseException("Class is associated with another class, attribute or method");
        }
    }

    private void ValidateClassNameIsUnique(string name)
    {
        var classes = _classRepository.FindAllTypes();

        if(classes.Any(classT => classT.Name == name))
        {
            throw new InvalidClassException("There is already a type with this name");
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

    private bool ValidateParentIdExists(Guid id)
    {
        var classes = _classRepository.FindAllTypes();

        if(!classes.Exists(classT => classT.Id == id && classT is not Primitive))
        {
            throw new ClassNotFoundException("There is no class with this id");
        }

        return classes.Any(classT => classT.Id == id && classT is Interface);
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

    private void ValidateStereotypeInCreate(KeyWord? stereotype)
    {
        if(stereotype != null && stereotype != KeyWord.Abstract && stereotype != KeyWord.Sealed)
        {
            throw new InvalidClassException("Invalid stereotype value. Classes stereotypes can only be null, abstract or sealed.");
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
