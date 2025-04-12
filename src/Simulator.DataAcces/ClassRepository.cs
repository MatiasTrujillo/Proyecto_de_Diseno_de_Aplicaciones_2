using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.DataAcces;

public class ClassRepository(SqlContext context) : IClassRepository
{
    public void Add(Class aClass)
    {
        context.Classes.Add(aClass);
        context.SaveChanges();
    }

    public void Update(Class aClass)
    {
        throw new NotImplementedException();
    }

    public Class Find(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Class> FindAll()
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
