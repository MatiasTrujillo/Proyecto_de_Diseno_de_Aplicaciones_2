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

    public void UpdateAttribute(Guid id, UpdateAttributeDTO uAttribute)
    {
        var attribute = _attributeRepository.Find(id);
        ValidateAttributeExist(attribute);
        ValidateAttributeIsNotInClass(attribute);
        ValidateName(uAttribute.Name, attribute);
        ValidateAccessibility(uAttribute.Accessibility, attribute);
        ValidateAttributeTypeUp(uAttribute.IdType);
        _attributeRepository.Update(attribute);
    }

    private static void ValidateAttributeExist(Attribute attributeT) // ASA
    {
        if(attributeT == null)
        {
            throw new InvalidAttributeException("Attribute not found");
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

    private void ValidateName(string? name, Attribute attribute)
    {
    }

    private void ValidateAccessibility(string? newAccessibility, Attribute attribute)
    {
    }

    private void ValidateAttributeTypeUp(Guid? methodTReturnType)
    {
    }

    public void DeleteAttribute(Guid id)
    {
        var attributeT = _attributeRepository.Find(id);
        ValidateAttributeExist(attributeT);
        ValidateAttributeIsNotInClass(attributeT);
        _attributeRepository.Delete(id);
    }

    private void ValidateAttributeType(Type? methodTReturnType) // ASASA
    {
        var classes = _classRepository.FindAll();
        if(methodTReturnType != null && classes.All(c => c.Name != methodTReturnType.Name))
        {
            throw new MethodException("Return type not found.");
        }
    }

    private void ValidateClassExists(Guid idClass) // AS
    {
        var classT = _classRepository.Find(idClass);
        if(classT == null)
        {
            throw new ClassNotFoundException("Class not found");
        }
    }

    private void ValidUniqueNameInClass(Guid idClass, Attribute attribute) // ASDA
    {
        var classT = _classRepository.Find(idClass);

        if(classT.Attributes != null && classT.Attributes.Any(a => a.Name == attribute.Name))
        {
            throw new InvalidAttributeException($"An attribute with the name '{attribute.Name}' already exists in the class");
        }
    }
}
