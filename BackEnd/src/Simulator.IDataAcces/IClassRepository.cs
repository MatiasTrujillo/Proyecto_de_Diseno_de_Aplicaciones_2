using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.IDataAcces;

public interface IClassRepository
{
    void Add(Class aClass);
    void Update(Class aClass);
    Class Find(Guid id);
    List<Class> FindAll();
    List<Type> FindAllTypes();
    bool Exist(Guid id);
    void Delete(Guid id);
}
