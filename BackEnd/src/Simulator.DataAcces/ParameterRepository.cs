using Microsoft.EntityFrameworkCore;
using Simulator.Domain;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.DataAcces;

public class ParameterRepository(SqlContext context) : IParameterRepository
{
    public void Add(Parameter parameter)
    {
        if(parameter.ParameterType != null)
        {
            var type = context.Set<Type>().Find(parameter.ParameterType.Id);
            if(type != null)
            {
                parameter.ParameterType = type;
            }
        }

        context.Parameters.Add(parameter);
        context.SaveChanges();
    }

    public Parameter Find(Guid id)
    {
        var parameter = context.Parameters
            .Include(p => p.ParameterType)
            .Include(iv => iv.InvokedMethods)
            .FirstOrDefault(p => p.Id == id);

        return parameter;
    }

    public List<Parameter> FindAll()
    {
        return context.Parameters
            .Include(p => p.ParameterType)
            .Include(iv => iv.InvokedMethods)
            .ToList();
    }

    public void Update(Parameter parameter)
    {
        var existingParameter = Find(parameter.Id);
        context.Entry(existingParameter).CurrentValues.SetValues(parameter);
        context.SaveChanges();
    }
}
