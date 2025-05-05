using Microsoft.EntityFrameworkCore;
using Simulator.Domain;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.DataAcces;

public class LocalVariableRepository(SqlContext context) : ILocalVariableRepository
{
    public void Add(LocalVariable localVariable)
    {
        if(localVariable.LocalVariableType != null)
        {
            var type = context.Set<Type>().Find(localVariable.LocalVariableType.Id);
            if(type != null)
            {
                localVariable.LocalVariableType = type;
            }
        }

        context.LocalVariables.Add(localVariable);
        context.SaveChanges();
    }

    public LocalVariable Find(Guid id)
    {
        var localVariable = context.LocalVariables
            .Include(p => p.LocalVariableType)
            .Include(p => p.InvokedMethod)
            .FirstOrDefault(p => p.Id == id);

        return localVariable;
    }

    public List<LocalVariable> FindAll()
    {
        return context.LocalVariables
            .Include(p => p.LocalVariableType)
            .Include(p => p.InvokedMethod)
            .ToList();
    }
}
