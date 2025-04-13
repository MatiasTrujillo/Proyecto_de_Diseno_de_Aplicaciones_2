using Simulator.BussinesLogic.DTO;
using Simulator.Domain.Exception;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.BussinesLogic;

public class AttributeService(IAttributeRepository attributeRepository)
{
    private readonly IAttributeRepository _attributeRepository = attributeRepository;

    public void CreateAttribute(AttributeDTO aDTO)
    {
        var attribute = aDTO.ToEntity();
        ValidUniqueName(attribute);

        _attributeRepository.Add(attribute);
    }

    private void ValidUniqueName(Attribute attribute)
    {
        if(_attributeRepository.Find(attribute.Id) != null)
        {
            throw new AttributeException("Attribute already exists");
        }
    }
}
