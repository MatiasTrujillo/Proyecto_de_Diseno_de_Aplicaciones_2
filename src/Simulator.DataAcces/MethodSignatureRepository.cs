using Microsoft.EntityFrameworkCore;
using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.DataAcces;

public class MethodSignatureRepository(SqlContext context) : IMethodSignatureRepository
{
    public void Add(MethodSignature signature)
    {
        context.MethodSignatures.Add(signature);
        context.SaveChanges();
    }

    public MethodSignature Find(Guid id)
    {
        var signature = context.MethodSignatures.FirstOrDefault(p => p.Id == id);
        return signature;
    }

    public void Update(MethodSignature updated)
    {
        var existing = context.MethodSignatures
            .Include(ms => ms.Parameters)
            .FirstOrDefault(ms => ms.Id == updated.Id);
        if (existing == null)
        {
            return;
        }

        foreach (var param in updated.Parameters)
        {
            existing.Parameters.Add(param);
        }

        context.SaveChanges();
    }

    public List<MethodSignature> FindAll()
    {
        return context.MethodSignatures.ToList();
    }
}
