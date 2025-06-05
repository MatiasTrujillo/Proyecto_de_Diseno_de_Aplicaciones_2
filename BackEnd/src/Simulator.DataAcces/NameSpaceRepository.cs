using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.DataAcces;

public class NameSpaceRepository(SqlContext context) : INameSpaceRepository
{
    public void Add(NameSpace nameSpace)
    {
        context.NameSpaces.Add(nameSpace);
        context.SaveChanges();
    }

    public NameSpace Find(Guid id)
    {
        var nameSpace = context.NameSpaces
            .FirstOrDefault(ns => ns.Id == id);

        return nameSpace;
    }

    public void Update(NameSpace nameSpace)
    {
        var existingNameSpace = Find(nameSpace.Id);
        context.Entry(existingNameSpace).CurrentValues.SetValues(nameSpace);
        context.SaveChanges();
    }

    public void Delete(NameSpace nameSpace)
    {
        var existingNameSpace = Find(nameSpace.Id);
        if(existingNameSpace != null)
        {
            context.NameSpaces.Remove(existingNameSpace);
            context.SaveChanges();
        }
    }

    public List<NameSpace> FindAll()
    {
        return context.NameSpaces
            .ToList();
    }
}
