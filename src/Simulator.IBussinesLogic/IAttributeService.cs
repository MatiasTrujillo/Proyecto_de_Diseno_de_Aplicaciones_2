using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface IAttributeService
{
    Guid CreateAttribute(AttributeDto attribute);
    void AsociateAttribute(Guid id, Guid idClass);
    void UpdateAttributeName(Guid id, string newName);
    void UpdateAttributeAccessibility(Guid id, string newAccessibility);
    void DeleteAttribute(Guid id);
}
