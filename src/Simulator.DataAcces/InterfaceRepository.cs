using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.DataAcces;

public class InterfaceRepository(SqlContext context) : IInterfaceRepository
{
    public void Add(Interface anInterface)
    {
        context.Interfaces.Add(anInterface);
        context.SaveChanges();
    }

    public void Update(Interface anInterface)
    {
        throw new NotImplementedException();
    }

    public Interface Find(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Interface> FindAll()
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
