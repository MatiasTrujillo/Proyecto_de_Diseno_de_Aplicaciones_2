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

public class MethodService(IMethodRepository methodRepository, IClassRepository classRepository, IParameterRepository parameterRepository, ILocalVariableRepository localVariableRepository) : IMethodService
{
    private readonly IMethodRepository _methodRepository = methodRepository;
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IParameterRepository _parameterRepository = parameterRepository;
    private readonly ILocalVariableRepository _localVariableRepository = localVariableRepository;
    private static int _indentacion = 0;

    public Guid CreateMethod(MethodDTO method)
    {
        ValidateMethodDTOExist(method);
        var methodT = method.ToEntity();
        var type = methodT.ReturnType.Name;
        var returnType = _classRepository.FindAll().FirstOrDefault(c => c.Name == type);
        ValidateUniqueMethod(methodT);
        methodT.ReturnType = returnType;
        ValidateMethodReturnType(methodT.ReturnType);
        _methodRepository.Add(methodT);
        return methodT.Id;
    }

    public void AddAMethod(Guid id, Guid idToAdd)
    {
        var method = _methodRepository.Find(id);
        var methodToAdd = _methodRepository.Find(idToAdd);
        ValidateMethodExist(method);
        ValidateMethodExist(methodToAdd);
        ValidateMethodIsNotInRepeted(id, idToAdd);
        method.Methods.Add(methodToAdd);
        _methodRepository.Update(method);
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

    public string ExecuteMethod(Type reference, Class objectToCreate, Method methodReference)
    {
        _indentacion = 0;
        var result = new List<string>();
        var flag = false;
        if(objectToCreate.HierarchyClass != null)
        {
            flag = reference.Id != objectToCreate.HierarchyClass.Id;
        }

        FirstLine(reference, objectToCreate, methodReference, flag, result);
        ParametersMethod(methodReference, result);
        Attribute(reference, methodReference, result);
        LocalVariable(methodReference, result);
        return string.Join("\n", result);
    }

    private void LocalVariable(Method methodReference, List<string> result)
    {
        foreach(var calledMethod in methodReference.Methods)
        {
            var slead = true;
            foreach(var localVar in methodReference.LocalVariables)
            {
                var classType = localVar.LocalVariableType as Class;
                var c = classType.Methods != null && classType.Methods.Any(m => m.Name == calledMethod.Name);
                var aux = classType;
                var i = 0;
                if(classType != null && c)
                {
                    var classInherited = classType.HierarchyClass as Class;
                    var cant = classInherited.Methods.Count;
                    while(aux != null && i < cant)
                    {
                        if(classInherited != null && classInherited.Methods[i].Stereotype != KeyWord.Sealed)
                        {
                            var indentation = new string('\t', _indentacion);
                            result.Add(
                                $"{indentation}{localVar.Name}.{classInherited.Methods[i].Name}() -> {aux.Name}.{classInherited.Methods[i].Name}()");
                        }

                        aux = (Class)aux.HierarchyClass;
                        i++;
                    }

                    while(classInherited != null)
                    {
                        Sobreescrito(classType, result, classInherited);
                        classType = classInherited;
                        classInherited = (Class)classInherited.HierarchyClass;
                    }

                    MethodTThis(classType, result);
                }
            }
        }
    }

    private static void Attribute(Type reference, Method methodReference, List<string> result)
    {
        foreach(var calledMethod in methodReference.Methods)
        {
            if(reference is Class referenceClass)
            {
                var indentation = new string('\t', _indentacion);
                foreach(var attr in referenceClass.Attributes)
                {
                    if(attr.Type is Class attrClass)
                    {
                        if(attrClass.Methods != null && attrClass.Methods.Any(m => m.Name == calledMethod.Name))
                        {
                            result.Add(
                                $"{indentation}{attr.Name}.{calledMethod.Name}() -> {attrClass.Name}.{calledMethod.Name}()");
                            break;
                        }

                        var parent = attrClass.HierarchyClass as Class;
                        while(parent != null)
                        {
                            if(parent.Methods != null)
                            {
                                var methodInParent = parent.Methods.FirstOrDefault(m => m.Name == calledMethod.Name);
                                if(methodInParent != null)
                                {
                                    result.Add(
                                        $"{indentation}{attr.Name}.{calledMethod.Name}() -> {parent.Name}.{methodInParent.Name}()");
                                    break;
                                }
                            }

                            parent = (Class)parent.HierarchyClass;
                        }
                    }
                }

                if(referenceClass.Methods != null && referenceClass.Methods.Any(m => m.Name == calledMethod.Name))
                {
                    result.Add(
                        $"{indentation}this.{calledMethod.Name}() -> {referenceClass.Name}.{calledMethod.Name}()");
                }
            }
        }
    }

    private static void ParametersMethod(Method methodReference, List<string> result)
    {
        foreach(var calledMethod in methodReference.Methods)
        {
            foreach(var param in methodReference.Parameters)
            {
                if(param.ParameterType is Class paramClass)
                {
                    if(paramClass.Methods != null && paramClass.Methods.Any(m => m.Name == calledMethod.Name))
                    {
                        var indentation = new string('\t', _indentacion);
                        result.Add(
                            $"{indentation}{param.Name}.{calledMethod.Name}() -> {paramClass.Name}.{calledMethod.Name}()");
                    }
                }
            }
        }
    }

    private static void MethodTThis(Class classType, List<string> result)
    {
        var indentation = new string('\t', _indentacion + 1);
        if(classType.Stereotype == null)
        {
            foreach(var m in classType.Methods)
            {
                if(!(m.Methods.Count > 0))
                {
                    result.Add($"{indentation}this.{m.Name}() -> {classType.Name}.{m.Name}()");
                    _indentacion++;
                }
            }
        }
    }

    private static void FirstLine(Type reference, Class objectToCreate, Method methodReference, bool flag,
        List<string> result)
    {
        var indentation = new string('\t', _indentacion);
        if(!flag)
        {
            result.Add(
                $"{indentation}{reference.Name}.{methodReference.Name}() -> {objectToCreate.Name}.{methodReference.Name}()");
        }
        else
        {
            result.Add(
                $"{indentation}{reference.Name}.{methodReference.Name}() -> {reference.Name}.{methodReference.Name}()");
        }

        _indentacion++;
    }

    private void Sobreescrito(Class classType, List<string> result, Class parent)
    {
        foreach(var methodInParent in classType.Methods)
        {
            var cant = ContarOcurrenciasMetodoEnJerarquia(methodInParent);
            var indentation = new string('\t', _indentacion + 1);
            if(cant > 1)
            {
                result.Add($"{indentation}base.{methodInParent.Name}() -> {parent.Name}.{methodInParent.Name}()");
                _indentacion++;
            }
        }
    }

    public int ContarOcurrenciasMetodoEnJerarquia(Method metodoNombre)
    {
        var count = 0;
        var met = metodoNombre;

        foreach(var m in met.Methods)
        {
            if(m.Name == metodoNombre.Name)
            {
                count++;
            }

            if(m.Methods.Count > 0)
            {
                count += ContarOcurrenciasMetodoEnJerarquia(m);
            }
        }

        return count;
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

    private void ValidateMethodIsNotInRepeted(Guid idMethod, Guid idMethodToAdd)
    {
        var method = _methodRepository.Find(idMethod);
        var methodToAdd = _methodRepository.Find(idMethodToAdd);
        if(method.Methods != null && method.Methods.Any(m => m.Id == idMethodToAdd))
        {
            throw new MethodInUseException("Method is already associated with this method.");
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
}
