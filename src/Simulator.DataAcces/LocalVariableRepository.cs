using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.DataAcces;

public class LocalVariableRepository(SqlContext context) : ILocalVariableRepository
{
    public void Add(LocalVariable localVariable)
    {
        context.LocalVariables.Add(localVariable);
        context.SaveChanges();
    }

    public LocalVariable Find(Guid id)
    {
        throw new NotImplementedException();
    }
}
