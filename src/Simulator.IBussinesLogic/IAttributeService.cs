using DTO;

namespace Simulator.IBussinesLogic;

public interface IAttributeService
{
    void CreateAttribute(AttributeDto attribute);
    void AsociateAttribute(Guid idClass, AttributeDto attribute);
    void UpdateAttribute(Guid id, AttributeDto attribute);
    void DeleteAttribute(Guid id);
}
