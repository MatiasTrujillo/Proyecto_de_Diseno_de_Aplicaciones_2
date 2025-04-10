using Simulator.BussinesLogic.DTO;
using Simulator.DataAcces;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.BussinesLogic;

public class AttributeService
{
    private readonly IAttributeRepository _attributeRepository;

    public AttributeService(IAttributeRepository attributeRepository)
    {
        _attributeRepository = attributeRepository;
    }

    public void CreateAttribute(AttributeDTO aDTO)
    {
    }
}
