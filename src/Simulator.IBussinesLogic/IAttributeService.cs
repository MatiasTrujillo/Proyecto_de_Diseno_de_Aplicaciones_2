using Simulator.BussinesLogic.DTO;

namespace Simulator.IBussinesLogic;

public interface IAttributeService
{
    void CreateAttribute(AttributeDTO attribute);
    void UpdateAttribute(Guid id, AttributeDTO attribute);
    void DeleteAttribute(Guid id);
}
