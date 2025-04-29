using System;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic;

public class AttributeService(IAttributeRepository attributeRepository, IClassRepository classRepository) : IAttributeService
{
    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IClassRepository _classRepository = classRepository;

    public Guid CreateAttribute(AttributeDto aDTO)
    {
        var attribute = aDTO.ToEntity();
        var type = aDTO.Type.Name;
        var typeT = _classRepository.FindAll().FirstOrDefault(c => c.Name == type);
        ValidateAttributeType(attribute.Type);
        attribute.Type = typeT;
        _attributeRepository.Add(attribute);
        return attribute.Id;
    }

    public void AsociateAttribute(Guid id, Guid idClass)
    {
        var attributeT = _attributeRepository.Find(id);
        var classT = _classRepository.Find(idClass);
        ValidateClassExists(idClass);
        ValidUniqueNameInClass(idClass, attributeT);
        classT.Attributes.Add(attributeT);
        _classRepository.Update(classT);
    }

    public void UpdateAttributeName(Guid id, string newName)
    {
        var attribute = _attributeRepository.Find(id);
        ValidateAttributeExist(attribute);
        ValidateAttributeIsNotInClass(attribute);
        attribute.Name = newName;
        _attributeRepository.Update(attribute);
    }

    public void UpdateAttributeAccessibility(Guid id, string newAccessibility)
    {
        var attribute = _attributeRepository.Find(id);
        ValidateAttributeExist(attribute);
        ValidateAttributeIsNotInClass(attribute);
        ValidateAccessibility(newAccessibility, attribute);
        _attributeRepository.Update(attribute);
    }

    /*public void UpdateAttributeType(Guid id, string type)
    {
        var attribute = _attributeRepository.Find(id);
        ValidateAttributeExist(attribute);
        ValidateAttributeIsNotInClass(attribute);
        attribute.Type = type.ToEntity();
        _attributeRepository.Update(attribute);
    }*/

    public void DeleteAttribute(Guid id)
    {
        var attributeT = _attributeRepository.Find(id);
        ValidateAttributeExist(attributeT);
        ValidateAttributeIsNotInClass(attributeT);
        _attributeRepository.Delete(id);
    }

    private void ValidateAttributeType(Type? methodTReturnType)
    {
        var classes = _classRepository.FindAll();
        if(methodTReturnType != null && classes.All(c => c.Name != methodTReturnType.Name))
        {
            throw new MethodException("Return type not found.");
        }
    }

    private void ValidateAccessibility(string newAccessibility, Attribute attribute)
    {
        if(newAccessibility == "Public")
        {
            attribute.Accessibility = Accessibility.Public;
        }
        else if(newAccessibility == "Private")
        {
            attribute.Accessibility = Accessibility.Private;
        }
        else if(newAccessibility == "Protected")
        {
            attribute.Accessibility = Accessibility.Protected;
        }
        else
        {
            throw new InvalidAttributeException("Invalid accessibility type");
        }
    }

    private void ValidateAttributeIsNotInClass(Attribute attributeT)
    {
        var classes = _classRepository.FindAll();

        if(classes.Any(classT => classT.Attributes.Contains(attributeT)))
        {
            throw new InvalidAttributeException("Attribute is associated with a class");
        }
    }

    private static void ValidateAttributeExist(Attribute attributeT)
    {
        if(attributeT == null)
        {
            throw new InvalidAttributeException("Attribute not found");
        }
    }

    private void ValidateClassExists(Guid idClass)
    {
        var classT = _classRepository.Find(idClass);
        if(classT == null)
        {
            throw new ClassNotFoundException("Class not found");
        }
    }

    private void ValidUniqueNameInClass(Guid idClass, Attribute attribute)
    {
        var classT = _classRepository.Find(idClass);

        if(classT.Attributes != null && classT.Attributes.Any(a => a.Name == attribute.Name))
        {
            throw new InvalidAttributeException($"An attribute with the name '{attribute.Name}' already exists in the class");
        }
    }
}
