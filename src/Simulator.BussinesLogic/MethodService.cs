using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic;

public class MethodService(IMethodRepository methodRepository, IClassRepository classRepository, IParameterRepository parameterRepository, ILocalVariableRepository localVariableRepository, IInvokedMethodRepository invokedMethodRepository) : IMethodService
{
    private readonly IMethodRepository _methodRepository = methodRepository;
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IParameterRepository _parameterRepository = parameterRepository;
    private readonly ILocalVariableRepository _localVariableRepository = localVariableRepository;
    private readonly IInvokedMethodRepository _invokedMethodRepository = invokedMethodRepository;

    public Guid CreateMethod(MethodDTO method)
    {
        ValidateMethodDTOExist(method);
        var methodT = method.ToEntity();
        Type? returnType = null;
        if(methodT.ReturnType != null)
        {
            ValidateMethodReturnTypeString(methodT.ReturnType.Name);
            var type = methodT.ReturnType.Name;
            returnType = _classRepository.FindAll().FirstOrDefault(c => c.Name == type);
        }

        ValidateUniqueMethod(methodT);
        methodT.ReturnType = returnType;
        _methodRepository.Add(methodT);
        return methodT.Id;
    }

    public Guid AssociateInvokeMethod(InvokedMethodDTO invokedMethod)
    {
        ValidateInvokedMethodDTOExist(invokedMethod);
        var method = _methodRepository.Find(invokedMethod.MethodId);
        ValidateMethodExist(method);
        ValidateInvokedMethodReferenceNameIsNotNull(invokedMethod.ReferenceName);
        var invokedMethodT = invokedMethod.ToEntity();
        _invokedMethodRepository.Add(invokedMethodT);
        return invokedMethodT.Id;
    }

    public void AddAMethod(Guid id, Guid invokedMethodId)
    {
        var method = _methodRepository.Find(id);
        var invokedMethod = _invokedMethodRepository.Find(invokedMethodId);
        ValidateMethodExist(method);
        if(invokedMethod == null)
        {
            throw new InvokedMethodNotFoundException("Invoked Method not found.");
        }
    }

    private void ValidateInvokedMethodExist(InvokedMethod invokedMethod)
    {
        if(invokedMethod == null)
        {
            throw new InvokedMethodNotFoundException("Invoked Method not found.");
        }
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

    public Guid CreateLocalVariable(LocalVariableDTO localVariable)
    {
        ValidateLocalVariableIsNotNull(localVariable);
        var localVariableT = localVariable.ToEntity();
        var type = localVariableT.LocalVariableType.Name;
        var returnType = _classRepository.FindAll().FirstOrDefault(c => c.Name == type);
        localVariableT.LocalVariableType = returnType;
        ValidateMethodReturnType(localVariableT.LocalVariableType);
        _localVariableRepository.Add(localVariableT);
        return localVariableT.Id;
    }

    public void AddLocalVariableToMethod(Guid methodId, Guid localVariable)
    {
        var method = _methodRepository.Find(methodId);
        var localVariableT = _localVariableRepository.Find(localVariable);
        ValidateMethodExist(method);
        ValidateLocalVariableExist(localVariableT);
        ValidateNameLocalVariable(localVariableT, method);
        method.LocalVariables.Add(localVariableT);
        _methodRepository.Update(method);
    }

    public Guid CreateParameter(ParameterDTO parameter)
    {
        ValidateParameterIsNotNull(parameter);
        var parameterT = parameter.ToEntity();
        var type = parameterT.ParameterType.Name;
        var returnType = _classRepository.FindAll().FirstOrDefault(c => c.Name == type);
        parameterT.ParameterType = returnType;
        ValidateMethodReturnType(parameterT.ParameterType);
        _parameterRepository.Add(parameterT);
        return parameterT.Id;
    }

    public void AddParameterToMethod(Guid methodId, Guid parameterId)
    {
        var param = _parameterRepository.Find(parameterId);
        var method = _methodRepository.Find(methodId);
        ValidateMethodExist(method);
        ValidateParameterExist(param);
        ValidateNameParameter(param, method);
        method.Parameters.Add(param);
        _methodRepository.Update(method);
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
        if(method.Methods.Count != 0)
        {
            throw new InvalidMethodException("Method is associated with another method.");
        }
    }

    private void ValidateNameParameter(Parameter parameter, Method method)
    {
        if(method.LocalVariables != null && method.Parameters != null && (method.LocalVariables.Any(v => v.Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase)) ||
                                                                          method.Parameters.Any(p => p.Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase))))
        {
            throw new InvalidMethodException("A variable or parameter with the same name already exists.");
        }
    }

    private static void ValidateNameLocalVariable(LocalVariable localVariable, Method method)
    {
        if(method.LocalVariables != null && method.Parameters != null && (method.LocalVariables.Any(v => v.Name.Equals(localVariable.Name, StringComparison.OrdinalIgnoreCase)) ||
                                                                          method.Parameters.Any(p => p.Name.Equals(localVariable.Name, StringComparison.OrdinalIgnoreCase))))
        {
            throw new InvalidMethodException("A variable or parameter with the same name already exists.");
        }
    }

    private void ValidateMehodIsNotInClass(Guid id)
    {
        var classes = _classRepository.FindAll();
        if(classes.Any(classT => classT.Methods != null && classT.Methods.Any(method => method.Id == id)))
        {
            throw new MethodInUseException("Method is associated with a class.");
        }
    }

    private void ValidateClassUniqueNameMethod(Method method, Class classT)
    {
        var methods = classT.Methods;

        if(methods != null && methods.Any(existingMethod => existingMethod.Name.Equals(method.Name)))
        {
            throw new InvalidMethodException("A method with the same name already exists in the class.");
        }
    }

    private static void ValidateClassExist(Class classT)
    {
        if(classT == null)
        {
            throw new ClassNotFoundException("Class not found.");
        }
    }

    private void ValidateMethodExist(Method method)
    {
        if(method == null)
        {
            throw new MethodNotFoundException("Method null.");
        }
    }

    private void ValidateUniqueMethod(Method methodT)
    {
        List<Method> methods = _methodRepository.FindAll();
        if(methods.Any(method => method.Name.Equals(methodT.Name)))
        {
            throw new InvalidMethodException("Method with the same name  already exists.");
        }
    }

    private void ValidateMethodReturnTypeString(string? methodTReturnType)
    {
        var classes = _classRepository.FindAll();
        if(classes.All(c => c.Name != methodTReturnType))
        {
            throw new MethodException("Method return type not found.");
        }
    }

    private void ValidateMethodReturnType(Type? methodTReturnType)
    {
        var classes = _classRepository.FindAll();
        if(classes.All(c => c.Name != methodTReturnType.Name))
        {
            throw new MethodException("Method return type not found.");
        }
    }

    private void ValidateMethodDTOExist(MethodDTO method)
    {
        if(method == null)
        {
            throw new MethodNotFoundException("Method cannot be null.");
        }
    }

    private void ValidateParameterIsNotNull(ParameterDTO parameter)
    {
        if(parameter == null)
        {
            throw new ParameterNotFoundException("Parameter cannot be null.");
        }
    }

    private void ValidateLocalVariableIsNotNull(LocalVariableDTO parameter)
    {
        if(parameter == null)
        {
            throw new LocalVariableNotFoundException("Local Variable cannot be null.");
        }
    }

    private void ValidateLocalVariableExist(LocalVariable localVariableT)
    {
        if(localVariableT == null)
        {
            throw new LocalVariableNotFoundException("Local Variable not found.");
        }
    }

    private void ValidateParameterExist(Parameter parameter)
    {
        if(parameter == null)
        {
            throw new ParameterNotFoundException("Parameter not found.");
        }
    }

    private void ValidateInvokedMethodReferenceNameIsNotNull(string? invokedMethodReferenceName)
    {
        if(string.IsNullOrEmpty(invokedMethodReferenceName))
        {
            throw new InvokedMethodNotFoundException("Invoked Method Reference Name cannot be null or empty.");
        }
    }

    private void ValidateInvokedMethodDTOExist(InvokedMethodDTO invokedMethod)
    {
        if(invokedMethod == null)
        {
            throw new InvokedMethodNotFoundException("Invoked Method cannot be null.");
        }
    }
}
