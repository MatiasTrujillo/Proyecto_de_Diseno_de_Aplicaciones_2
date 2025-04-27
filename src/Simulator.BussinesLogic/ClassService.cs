using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.BussinesLogic;

public class ClassService(IClassRepository classRepository) : IClassService
{
    private readonly IClassRepository _classRepository = classRepository;

    // private readonly IAttributeRepository _attributeRepository = attributeRepository;

    // private readonly IMethodRepository _methodRepository = methodRepository;

    public void CreateClass(ClassDTO classDto)
    {
        var aClass = classDto.ToEntity();
        ValidateClassNameIsUnique(aClass.Name);
        _classRepository.Add(aClass);
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
            throw new InvalidClassStereotypeException("The stereotype is not valid");
        }

        _classRepository.Update(classT);
    }

    public void DeleteClass(Guid id)
    {
        ValidateClassIdExists(id);
        _classRepository.Delete(id);
    }

    private void ValidateClassNameIsUnique(string name)
    {
        var classes = _classRepository.FindAll();

        if(classes.Any(classT => classT.Name == name))
        {
            throw new ClassException("There is already a class with this name");
        }
    }

    private void ValidateClassIdExists(Guid id)
    {
        var classes = _classRepository.FindAll();

        if(!classes.Exists(classT => classT.Id == id))
        {
            throw new ClassException("There is no class with this id");
        }
    }

    private List<Attribute> ConvertAttributesToEntity(List<AttributeDto> attributeDtos)
    {
        var attributes = new List<Attribute>();
        foreach(var attDto in attributeDtos)
        {
            attributes.Add(attDto.ToEntity());
        }

        return attributes;
    }
}
