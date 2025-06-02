using Simulator.Domain;

namespace Simulator.IDataAcces;

public interface IMethodSignatureRepository
{
    void Add(MethodSignature signature);
    MethodSignature Find(Guid id);
    void Update(MethodSignature signature);
    List<MethodSignature> FindAll();
}
