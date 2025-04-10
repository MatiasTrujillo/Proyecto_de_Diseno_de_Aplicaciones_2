using Attribute = Simulator.Domain.Attribute;

namespace Simulator.DataAcces;

public class AttributeRepository(SqlContext context)
{
    public void Add(Attribute entity)
    {
        context.Attributes.Add(entity);
        context.SaveChanges();
    }

    public Attribute Find(Guid id)
    {
        return context.Attributes.Find(id);
    }

    public List<Attribute> FindAll()
    {
        return context.Attributes.ToList();
    }

    public bool Exist(Guid key)
    {
        return context.Attributes.Any(a => a.Id == key);
    }

    public void Update(Attribute entity)
    {
        Attribute existingAttribute = Find(entity.Id);
        if (existingAttribute == null)
        {
            return;
        }

        context.Entry(existingAttribute).CurrentValues.SetValues(entity);
        context.SaveChanges();
    }

    public void Delete(Guid id)
    {
    }
}
