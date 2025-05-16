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
        Interface existingInterface = Find(anInterface.Id);

        context.Entry(existingInterface).CurrentValues.SetValues(anInterface);
        context.SaveChanges();
    }

    public Interface Find(Guid id)
    {
        return context.Set<Interface>().Find(id);
    }

    public List<Interface> FindAll()
    {
        return context.Set<Interface>().ToList();
    }

    public bool Exist(Guid id)
    {
        return context.Interfaces.Any(a => a.Id == id);
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
