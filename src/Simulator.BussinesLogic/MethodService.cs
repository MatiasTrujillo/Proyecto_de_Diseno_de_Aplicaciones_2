using DTO;
using Simulator.Domain;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class MethodService(IMethodRepository methodRepository, IClassRepository classRepository) : IMethodService
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

    public void DeleteMethod(Guid id)
    {
        var method = _methodRepository.Find(id);
        ValidateMethodExist(method);
        ValidateMehodIsNotInClass(id);
        ValidateMethodIsNotInUse(id);
        _methodRepository.Delete(id);
    }

    private void ValidateMethodIsNotInUse(Guid id)
    {
        var method = _methodRepository.Find(id);
        if(method.Methods.Any())
        {
            throw new Exception("Method is associated with another method.");
        }
    }

    public void AddAMethod(Guid id, Guid idToAdd)
    {
        var method = _methodRepository.Find(id);
        var methodToAdd = _methodRepository.Find(idToAdd);
        ValidateMethodIsNotInRepeted(id, idToAdd);
        method.Methods.Add(methodToAdd);
        _methodRepository.Update(method);
    }

    public void AddLocalVariableToMethod(Guid methodId, LocalVariableDTO localVariable)
    {
        LocalVariable localVariableT = localVariable.ToEntity();
        var method = _methodRepository.Find(methodId);
        ValidateMethodExist(method);

        if (method.LocalVariables.Any(v => v.Name.Equals(localVariable.Name, StringComparison.OrdinalIgnoreCase)) ||
            method.Parameters.Any(p => p.Name.Equals(localVariable.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new Exception("A variable or parameter with the same name already exists.");
        }

        method.LocalVariables.Add(localVariableT);
        _methodRepository.Update(method);
    }

    private void ValidateMethodIsNotInRepeted(Guid idMethod, Guid idMethodToAdd)
    {
        var method = _methodRepository.Find(idMethod);
        var methodToAdd = _methodRepository.Find(idMethodToAdd);
        if(method.Methods.Any(m => m.Id == idMethodToAdd))
        {
            throw new Exception("Method is already associated with this method.");
        }
    }

    private void ValidateMehodIsNotInClass(Guid id)
    {
        var classes = _classRepository.FindAll();
        if(classes.Any(classT => classT.Methods.Any(method => method.Id == id)))
        {
            throw new Exception("Method is associated with a class.");
        }
    }

    private void ValidateClassUniqueNameMethod(Method method, Class classT)
    {
        var methods = classT.Methods;

        if(methods.Any(existingMethod => existingMethod.Name.Equals(method.Name)))
        {
            throw new Exception("A method with the same name already exists in the class.");
        }
    }

    private static void ValidateClassExist(Class classT)
    {
        if(classT == null)
        {
            throw new Exception("Class not found.");
        }
    }

    private void ValidateMethodExist(Method method)
    {
        if(method == null)
        {
            throw new Exception("Method null.");
        }
    }

    private static void ValidateIsNotNull(MethodDTO method)
    {
        if(method == null)
        {
            throw new Exception("Method cannot be null.");
        }
    }

    private void ValidateUniqueMethod(Method methodT)
    {
        List<Method> methods = _methodRepository.FindAll();
        if(methods.Any(method => method.Name.Equals(methodT.Name)))
        {
            throw new Exception("Method with the same name  already exists.");
        }
    }
}
