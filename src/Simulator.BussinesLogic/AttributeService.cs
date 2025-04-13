using Simulator.BussinesLogic.DTO;
using Simulator.Domain.Exception;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class AttributeService(IAttributeRepository attributeRepository)
{
    private readonly IAttributeRepository _attributeRepository = attributeRepository;

    public void CreateAttribute(AttributeDTO aDTO)
    {
        var attribute = aDTO.ToEntity();

        _attributeRepository.Add(attribute);
    }
}
