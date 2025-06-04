using Microsoft.EntityFrameworkCore;
using Simulator.Domain;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.DataAcces;

public class LocalVariableRepository(SqlContext context) : ILocalVariableRepository
{
    public void Add(LocalVariable localVariable)
    {
        if(localVariable.ReferenceType != null)
        {
            var type = context.Set<Type>().Find(localVariable.ReferenceType.Id);
            if(type != null)
            {
                localVariable.ReferenceType = type;
            }
        }

        context.LocalVariables.Add(localVariable);
        context.SaveChanges();
    }

    public LocalVariable Find(Guid id)
    {
        var localVariable = context.LocalVariables
            .Include(p => p.ReferenceType)
            .Include(p => p.InvokedMethod)
            .FirstOrDefault(p => p.Id == id);

        return localVariable;
    }

    public List<LocalVariable> FindAll()
    {
        return context.LocalVariables
            .Include(p => p.ReferenceType)
            .Include(p => p.InvokedMethod)
            .ToList();
    }

    public void Update(LocalVariable localVariable)
    {
        var existingLocalVariable = Find(localVariable.Id);
        context.Entry(existingLocalVariable).CurrentValues.SetValues(localVariable);
        context.SaveChanges();
    }
}
