using DTO;
using Simulator.Domain.Exception;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.BussinesLogic;

public class AttributeService(IAttributeRepository attributeRepository, IClassRepository classRepository) : IAttributeService
{
    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IClassRepository _classRepository = classRepository;

    public void CreateAttribute(AttributeDto aDTO)
    {
        var attribute = aDTO.ToEntity();
        _attributeRepository.Add(attribute);
    }

    public void AsociateAttribute(Guid idClass, AttributeDto attribute)
    {
        var attributeT = attribute.ToEntity();
        var classT = _classRepository.Find(idClass);
        ValidateClassExists(idClass);
        ValidUniqueNameInClass(idClass, attributeT);
        classT.Attributes.Add(attributeT);
        _classRepository.Update(classT);
    }

    public void UpdateAttribute(Guid id, AttributeDto attribute)
    {
        var attributeT = _attributeRepository.Find(id);
        ValidateAttributeExist(attributeT);
        ValidateAttributeIsNotInClass(attributeT);
        attributeT.Name = attribute.Name;
        attributeT.Accessibility = attribute.Accessibility;
        _attributeRepository.Update(attributeT);
    }

    public void DeleteAttribute(Guid id)
    {
        var attributeT = _attributeRepository.Find(id);
        ValidateAttributeExist(attributeT);
        ValidateAttributeIsNotInClass(attributeT);
        _attributeRepository.Delete(id);
    }

    private void ValidateAttributeIsNotInClass(Attribute attributeT)
    {
        var classes = _classRepository.FindAll();

        if(classes.Any(classT => classT.Attributes.Contains(attributeT)))
        {
            throw new AttributeException("Attribute is associated with a class");
        }
    }

    private static void ValidateAttributeExist(Attribute attributeT)
    {
        if(attributeT == null)
        {
            throw new AttributeException("Attribute not found");
        }
    }

    private void ValidateClassExists(Guid idClass)
    {
        var classT = _classRepository.Find(idClass);
        if(classT == null)
        {
            throw new AttributeException("Class not found");
        }
    }

    private void ValidUniqueNameInClass(Guid idClass, Attribute attribute)
    {
        var classT = _classRepository.Find(idClass);

        if(classT.Attributes.Any(a => a.Name == attribute.Name))
        {
            throw new AttributeException($"An attribute with the name '{attribute.Name}' already exists in the class");
        }
    }
}
