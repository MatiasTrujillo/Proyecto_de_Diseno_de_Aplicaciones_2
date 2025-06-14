using Simulator.UpdateDTO;
using DomainAttribute = Simulator.Domain.Attribute;

namespace Simulator.IBussinesLogic;

public interface IAttributeService
{
    Guid CreateAttribute(DomainAttribute attribute);
    void AsociateAttribute(Guid id, Guid idClass);
    void UpdateAttribute(Guid id, UpdateAttributeDTO uAttribute);
    void DeleteAttribute(Guid id);
    void SetInvokeMethodToAttribute(Guid id, Guid idMethodInvoke);
    List<DomainAttribute> GetAttributes();
}
