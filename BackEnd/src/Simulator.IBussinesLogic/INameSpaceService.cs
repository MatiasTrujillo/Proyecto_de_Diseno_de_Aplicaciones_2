using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface INameSpaceService
{
    Guid CreateNameSpace(NameSpaceDTO nameSpaceDto);
    void AssociateNameSpaceToClass(Guid nameSpaceId, Guid classId);
    void AssociateNameSpaceToInterface(Guid nameSpaceId, Guid interfaceId);
    void AddFatherToNameSpace(Guid id, Guid idFather);
    void AddChildToNameSpace(Guid parentId, Guid childId);
    void RemoveChildFromNameSpace(Guid parentId, Guid childId);
    List<NameSpaceDTO> GetAllNameSpace();
    List<NamespaceComponentDTO> GetAllNamespaceComponents();
}
