using Simulator.Domain;

namespace Simulator.IBussinesLogic;

public interface INameSpaceService
{
    Guid CreateNameSpace(NameSpace nameSpaceDto);
    void AddChildToNameSpace(Guid parentId, Guid childId);
    void RemoveChildFromNameSpace(Guid parentId, Guid childId);
    List<NameSpace> GetAllNameSpace();
    List<NamespaceComponent> GetAllNamespaceComponents();
}
