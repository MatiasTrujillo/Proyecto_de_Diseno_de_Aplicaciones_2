using DTO;
using Simulator.Domain;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class MethodService(IMethodRepository methodRepository) : IMethodService
{
    private readonly IMethodRepository _methodRepository = methodRepository;

    public void CreateMethod(MethodDTO method)
    {
        var methodT = method.ToEntity();
        ValidateIsNotNull(methodT);
        ValidateUniqueMethod(methodT);
        _methodRepository.Add(methodT);
    }

    private void ValidateIsNotNull(Method methodT)
    {
        if (methodT == null)
        {
            throw new ArgumentNullException(nameof(methodT), "Method cannot be null.");
        }
    }

    private void ValidateUniqueMethod(Method methodT)
    {
        List<Method> methods = _methodRepository.FindAll();
        if (methods.Any(method => method.Name.Equals(methodT.Name)))
        {
            throw new Exception("Method with the same name  already exists.");
        }
    }

    public void DeleteMethod(Guid id)
    {
        _methodRepository.Delete(id);
    }
}
