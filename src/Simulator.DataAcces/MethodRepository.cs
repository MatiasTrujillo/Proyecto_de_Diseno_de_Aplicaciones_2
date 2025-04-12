using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.DataAcces;

public class MethodRepository(SqlContext context) : IMethodRepository
{
    public void Add(Method attribute)
    {
        context.Methods.Add(attribute);
        context.SaveChanges();
    }

    public void Update(Method attribute)
    {
        throw new NotImplementedException();
    }

    public Method Find(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Method> FindAll()
    {
        throw new NotImplementedException();
    }

    public bool Exist(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
