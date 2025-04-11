using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.DataAcces;

public class ClassRepository(SqlContext context) : IAttributeRepository
{
    public void Add(Attribute attribute)
    {
        throw new NotImplementedException();
    }

    public void Update(Attribute attribute)
    {
        throw new NotImplementedException();
    }

    public Attribute Find(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Attribute> FindAll()
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
