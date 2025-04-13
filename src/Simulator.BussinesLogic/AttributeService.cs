using Simulator.BussinesLogic.DTO;
using Simulator.Domain.Exception;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.BussinesLogic;

public class AttributeService(IAttributeRepository attributeRepository, IClassRepository classRepository) : IAttributeService
{
    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IClassRepository _classRepository = classRepository;

    public void CreateAttribute(AttributeDTO aDTO)
    {
        var attribute = aDTO.ToEntity();
        _attributeRepository.Add(attribute);
    }

    public void AsociateAtributeToClass(Guid idClass, AttributeDTO attribute)
    {
        var attributeT = attribute.ToEntity();
        var classT = _classRepository.Find(idClass);
        ValidateClassExists(idClass);
        ValidUniqueNameInClass(idClass, attributeT);
        classT.Attributes.Add(attributeT);
    }

    public void UpdateAttribute(Guid id, AttributeDTO attribute)
    {
        var attributeT = _attributeRepository.Find(id);
        ValidateAttributeExist(attributeT);
        ValidateAttributeIsNotInClass(attributeT);
        attributeT.Name = attribute.Name;
        attributeT.Accessibility = attribute.Accessibility;
        _attributeRepository.Update(attributeT);
    }

    private void ValidateAttributeIsNotInClass(Attribute attributeT)
    {
        var classes = _classRepository.FindAll();
    }

    private static void ValidateAttributeExist(Attribute attributeT)
    {
        if (attributeT == null)
        {
            throw new AttributeException("Attribute not found");
        }
    }

    private void ValidateClassExists(Guid idClass)
    {
        var classT = _classRepository.Find(idClass);
        if (classT == null)
        {
            throw new AttributeException("Class not found");
        }
    }

    private void ValidUniqueNameInClass(Guid idClass, Attribute attribute)
    {
        var classT = _classRepository.Find(idClass);

        if (classT.Attributes.Any(a => a.Name == attribute.Name))
        {
            throw new AttributeException($"An attribute with the name '{attribute.Name}' already exists in the class");
        }
    }

    public void DeleteAttribute(Guid id)
    {
        throw new NotImplementedException();
    }
}
