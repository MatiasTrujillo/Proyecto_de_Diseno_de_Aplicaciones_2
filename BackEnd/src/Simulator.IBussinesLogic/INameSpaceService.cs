using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface INameSpaceService
{
    Guid CreateNameSpace(NameSpaceDTO nameSpaceDto);
}
