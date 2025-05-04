using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.DataAcces;

public class InvokedMethodRepository(SqlContext context) : IInvokedMethodRepository
{
    public void Add(InvokedMethod localVariable)
    {
        context.InvokedMethods.Add(localVariable);
        context.SaveChanges();
    }

    public InvokedMethod Find(Guid id)
    {
        return context.Set<InvokedMethod>().Find(id);
    }
}
