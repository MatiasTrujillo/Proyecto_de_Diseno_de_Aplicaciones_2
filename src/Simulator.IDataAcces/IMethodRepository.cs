using Method = Simulator.Domain.Method;
namespace Simulator.IDataAcces;

public interface IMethodRepository
{
    void Add(Method attribute);
    void Update(Method attribute);
    Method Find(Guid id);
    List<Method> FindAll();
    bool Exist(Guid id);
    void Delete(Guid id);
}
