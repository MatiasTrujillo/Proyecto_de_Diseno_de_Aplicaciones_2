using System;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
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

    public void UpdateNameClass(Guid id, string name)
    {
        ValidateClassNameIsUnique(name);
        ValidateClassIdExists(id);
        var classT = _classRepository.Find(id);
        classT.Name = name;
        _classRepository.Update(classT);
    }

    public void UpdateClassStereotype(Guid id, string stereotype)
    {
        ValidateClassIdExists(id);
        var classT = _classRepository.Find(id);
        ValidateStereotype(stereotype, classT);
        _classRepository.Update(classT);
    }

    public void UpdateClassInherits(Guid id, Guid idClass)
    {
        ValidateClassIdExists(id);
        ValidateClassIdExists(idClass);
        ValidateClassIsNotInUse(id);
        var classT = _classRepository.Find(id);
        var heritageClass = _classRepository.Find(idClass);
        classT.HierarchyClass = heritageClass;
        _classRepository.Update(classT);
    }

    public void DeleteClass(Guid id)
    {
        ValidateClassIdExists(id);
        ValidateClassIsNotInUse(id);
        _classRepository.Delete(id);
    }

    private static void ValidateStereotype(string stereotype, Class classT)
    {
        if(stereotype == "Abstract")
        {
            classT.Stereotype = KeyWord.Abstract;
        }
        else if(stereotype == "Sealed")
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
}
