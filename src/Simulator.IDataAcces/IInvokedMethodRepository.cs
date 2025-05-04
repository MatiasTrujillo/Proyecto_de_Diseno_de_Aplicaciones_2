using Simulator.Domain;

namespace Simulator.IDataAcces;

public interface IInvokedMethodRepository
{
    void Add(InvokedMethod localVariable);
    InvokedMethod Find(Guid id);
}
