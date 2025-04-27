using Microsoft.EntityFrameworkCore; // Necesario para Include y ThenInclude
using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.DataAcces;

public class MethodRepository(SqlContext context) : IMethodRepository
{
    public void Add(Method method)
    {
        if(method.ReturnType != null)
        {
            var returnType = context.Classes.Find(method.ReturnType.Id);
            if(returnType != null)
            {
                method.ReturnType = returnType;
            }
        }

        var resolvedParameters = new List<Parameter>();
        foreach(var param in method.Parameters)
        {
            var existingParam = context.Parameters.Find(param.Id);
            resolvedParameters.Add(existingParam ?? param);
        }

        method.Parameters = resolvedParameters;

        var resolvedLocalVars = new List<LocalVariable>();
        foreach(var localVar in method.LocalVariables)
        {
            var existingVar = context.LocalVariables.Find(localVar.Id);
            resolvedLocalVars.Add(existingVar ?? localVar);
        }

        method.LocalVariables = resolvedLocalVars;

        var resolvedMethods = new List<Method>();
        foreach(var innerMethod in method.Methods)
        {
            var existingMethod = context.Methods.Find(innerMethod.Id);
            resolvedMethods.Add(existingMethod ?? innerMethod);
        }

        method.Methods = resolvedMethods;

        context.Methods.Add(method);
        context.SaveChanges();
    }

    public Method Find(Guid id)
    {
        var method = context.Methods
            .Include(m => m.Methods)
            .Include(m => m.Parameters)
            .Include(m => m.LocalVariables)
            .Include(m => m.ReturnType)
            .FirstOrDefault(m => m.Id == id);

        return method;
    }

    public void Update(Method entity)
    {
        var existingMethod = context.Methods
            .Include(m => m.Parameters)
            .FirstOrDefault(m => m.Id == entity.Id);

        context.Entry(existingMethod).CurrentValues.SetValues(entity);
        context.SaveChanges();
    }

    public List<Method> FindAll()
    {
        return context.Methods
            .Include(m => m.Parameters)
            .Include(m => m.LocalVariables)
            .Include(m => m.ReturnType)
            .ToList();
    }

    public bool Exist(Guid id)
    {
        return context.Methods.Any(a => a.Id == id);
    }

    public void Delete(Guid id)
    {
        var existingMethod = context.Methods.Find(id);
        if(existingMethod is not null)
        {
            context.Methods.Remove(existingMethod);
            context.SaveChanges();
        }
    }
}
