using DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class MethodService(IMethodRepository methodRepository) : IMethodService
{
    private readonly IMethodRepository _methodRepository = methodRepository;

    public void CreateMethod(MethodDTO method)
    {
        _methodRepository.Add(method.ToEntity());
    }

    public void AsociateMethod(Guid idClass, MethodDTO method)
    {
        throw new NotImplementedException();
    }

    public void UpdateMethod(Guid id, MethodDTO method)
    {
        throw new NotImplementedException();
    }

    public void DeleteMethod(Guid id)
    {
        throw new NotImplementedException();
    }
}
