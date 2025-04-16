using DTO;
using Simulator.Domain;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class MethodService(IMethodRepository methodRepository,IClassRepository classRepository) : IMethodService
{
    private readonly IMethodRepository _methodRepository = methodRepository;
    private readonly IClassRepository _classRepository = classRepository;

    public void CreateMethod(MethodDTO method)
    {
        ValidateIsNotNull(method);
        var methodT = method.ToEntity();
        ValidateUniqueMethod(methodT);
        _methodRepository.Add(methodT);
    }

    public void AssociateMethodWithClass(Guid methodId, Guid classId)
    {
        var method = _methodRepository.Find(methodId);
        ValidateMethodExist(method);
        var classT = _classRepository.Find(classId);
        ValidateClassExist(classT);
        ValidateClassUniqueNameMethod(method, classT);
        classT.Methods.Add(method);
        _classRepository.Update(classT);
    }

    private void ValidateClassUniqueNameMethod(Method method, Class classT)
    {
        var methods = classT.Methods;

        if (methods.Any(existingMethod => existingMethod.Name.Equals(method.Name)))
        {
            throw new Exception("A method with the same name already exists in the class.");
        }
    }

    private static void ValidateClassExist(Class classT)
    {
        if (classT == null)
        {
            throw new Exception("Class not found.");
        }
    }

    private static void ValidateMethodExist(Method method)
    {
        if (method == null)
        {
            throw new Exception("Method not found.");
        }
    }

    public void DeleteMethod(Guid id)
    {
        _methodRepository.Delete(id);
    }

    private static void ValidateIsNotNull(MethodDTO method)
    {
        if (method == null)
        {
            throw new Exception("Method cannot be null.");
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
}
