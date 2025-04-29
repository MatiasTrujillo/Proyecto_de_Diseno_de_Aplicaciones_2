using Simulator.Domain;

namespace Simulator.IDataAcces;

public interface ILocalVariableRepository
{
    void Add(LocalVariable localVariable);
    LocalVariable Find(Guid id);
}
