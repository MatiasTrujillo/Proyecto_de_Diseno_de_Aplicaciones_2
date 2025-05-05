using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface IAttributeService
{
    Guid CreateAttribute(AttributeDto attribute);
    void AsociateAttribute(Guid id, Guid idClass);
    void UpdateAttribute(Guid id, UpdateAttributeDTO uAttribute);
    void DeleteAttribute(Guid id);
    void SetInvokeMethodToAttribute(Guid id, Guid idMethodInvoke);
}
