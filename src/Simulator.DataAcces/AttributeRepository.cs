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
}
