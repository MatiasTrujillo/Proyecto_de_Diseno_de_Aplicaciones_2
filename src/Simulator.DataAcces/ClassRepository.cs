using Simulator.Domain;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

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
        Class existingClass = Find(aClass.Id);

        context.Entry(existingClass).CurrentValues.SetValues(aClass);
        context.SaveChanges();
    }

    public Class Find(Guid id)
    {
        return context.Set<Class>().Find(id);
    }

    public List<Class> FindAll()
    {
        return context.Set<Class>().ToList();
    }

    public List<Type> FindAllTypes()
    {
        return context.Set<Type>().ToList();
    }

    public bool Exist(Guid id)
    {
        return context.Classes.Any(a => a.Id == id);
    }

    public void Delete(Guid id)
    {
        Class existingClass = Find(id);

        context.Classes.Remove(existingClass);
        context.SaveChanges();
    }
}
