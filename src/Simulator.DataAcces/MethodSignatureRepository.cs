using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.DataAcces;

public class MethodSignatureRepository() : IMethodSignatureRepository
{
    public void Add(MethodSignature signature)
    {
        throw new NotImplementedException();
    }

    public MethodSignature Find(Guid id)
    {
        throw new NotImplementedException();
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
