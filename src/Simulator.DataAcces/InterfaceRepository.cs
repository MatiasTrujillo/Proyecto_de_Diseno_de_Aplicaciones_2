using Microsoft.EntityFrameworkCore;
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
        var existing = context.Interfaces
            .Include(i => i.MethodSignatures)
            .FirstOrDefault(i => i.Id == anInterface.Id);
        if(existing == null)
        {
            return;
        }

        context.Entry(existing).CurrentValues.SetValues(anInterface);
        existing.MethodSignatures = anInterface.MethodSignatures?.ToList()
                                    ?? [];
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
        Interface existingInterface = null!;
        if(Exist(id))
        {
            existingInterface = Find(id);
        }

        context.Interfaces.Remove(existingInterface);
        context.SaveChanges();
    }
}
