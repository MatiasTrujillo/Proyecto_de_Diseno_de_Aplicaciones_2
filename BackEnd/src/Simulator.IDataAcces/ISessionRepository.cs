using Simulator.Domain;

namespace Simulator.IDataAcces;

public interface ISessionRepository
{
    void Add(Session token);
    void Delete(Guid token);
    void Update(Session token);
    Session Find(Guid token);
    List<Session> FindAll();
}
