using Method = Simulator.Domain.Method;
namespace Simulator.IDataAcces;

public interface IMethodRepository
{
    void Add(Method method);
    Method Find(Guid id);
    void Update(Method method);
    List<Method> FindAll();
    bool Exist(Guid id);
    void Delete(Guid id);
}
