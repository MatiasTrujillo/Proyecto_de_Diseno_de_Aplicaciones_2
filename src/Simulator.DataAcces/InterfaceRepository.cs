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
        var existingInterface = Find(anInterface.Id);

        existingInterface.Name = anInterface.Name;
        existingInterface.MethodSignatures.Clear();
        foreach (var signature in anInterface.MethodSignatures)
        {
            existingInterface.MethodSignatures.Add(signature);
        }

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
