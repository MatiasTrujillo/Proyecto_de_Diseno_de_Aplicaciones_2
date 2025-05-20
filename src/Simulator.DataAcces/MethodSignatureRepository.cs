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

    public void Update(MethodSignature signature)
    {
        throw new NotImplementedException();
    }

    public List<MethodSignature> FindAll()
    {
        throw new NotImplementedException();
    }

    public bool Exist(Guid id)
    {
        throw new NotImplementedException();
    }
}
