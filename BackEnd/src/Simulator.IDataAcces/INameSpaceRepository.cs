using Simulator.Domain;

namespace Simulator.IDataAcces;

public interface INameSpaceRepository
{
    void Add(NameSpace nameSpace);
    NameSpace Find(Guid id);
    void Update(NameSpace nameSpace);
    List<NameSpace> FindAll();
}
