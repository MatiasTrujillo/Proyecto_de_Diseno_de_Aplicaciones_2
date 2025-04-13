using Simulator.BussinesLogic.DTO;

namespace Simulator.IBussinesLogic;

public interface IAttributeService
{
    void CreateAttributeForClass(Guid id,AttributeDTO attribute);
    void UpdateAttribute(Guid id, AttributeDTO attribute);
    void DeleteAttribute(Guid id);
}
