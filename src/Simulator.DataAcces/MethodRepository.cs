using Microsoft.EntityFrameworkCore;
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

    public Method Find(Guid id)
    {
        return context.Methods.Find(id);
    }

    public void Update(Method entity)
    {
        Method existingMethod = Find(entity.Id);

        context.Entry(existingMethod).CurrentValues.SetValues(entity);
        context.SaveChanges();
    }

    public List<Method> FindAll()
    {
        return context.Methods.ToList();
    }

    public bool Exist(Guid id)
    {
        return context.Methods.Any(a => a.Id == id);
    }

    public void Delete(Guid id)
    {
        Method existingMethod = Find(id);

        context.Methods.Remove(existingMethod);
        context.SaveChanges();
    }
}
