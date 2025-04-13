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

    public void CreateAttributeForClass(Guid idClass, AttributeDTO aDTO)
    {
        var attribute = aDTO.ToEntity();
        ValidUniqueName(idClass, attribute);
        var classT = _classRepository.Find(idClass);
        classT.Attributes.Add(attribute);
        _attributeRepository.Add(attribute);
    }

    private void ValidUniqueName(Guid idClass, Attribute attribute)
    {
    }

    public void UpdateAttribute(Guid id, AttributeDTO attribute)
    {
        throw new NotImplementedException();
    }

    public void DeleteAttribute(Guid id)
    {
        throw new NotImplementedException();
    }
}
