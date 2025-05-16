using Simulator.Domain;

namespace Simulator.IDataAcces;

public interface IInterfaceRepository
{
    void Add(Interface anInterface);
    void Update(Interface anInterface);
    Interface Find(Guid id);
    List<Interface> FindAll();
    bool Exist(Guid id);
    void Delete(Guid id);
}
