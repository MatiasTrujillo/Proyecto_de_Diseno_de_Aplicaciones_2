using Simulator.Domain;

namespace Simulator.IDataAcces;

public interface INameSpaceRepository
{
    void Add(NameSpace nameSpace);
    NameSpace Find(Guid id);
    void Update(NameSpace nameSpace);
    void Delete(NameSpace nameSpace);
    List<NameSpace> FindAll();
    List<NamespaceComponent> FindAllNamespaceComponents();
}
