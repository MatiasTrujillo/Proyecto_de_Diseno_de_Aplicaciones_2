using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.IDataAcces;

public interface IClassRepository
{
    void Add(Class aClass);
    void Update(Class aClass);
    Class Find(Guid id);
    List<Class> FindAll();
    bool Exist(Guid id);
    void Delete(Guid id);
}
