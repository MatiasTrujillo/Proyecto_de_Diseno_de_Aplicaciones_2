using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface INameSpaceService
{
    Guid CreateNameSpace(NameSpaceDTO nameSpaceDto);
    void AsociateToClass(Guid id, Guid idClass);
    void AsociateToInterface(Guid id, Guid idInterface);
}
