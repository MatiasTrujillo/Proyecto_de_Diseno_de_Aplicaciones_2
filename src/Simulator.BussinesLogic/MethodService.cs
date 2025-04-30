using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic;

public class MethodService(IMethodRepository methodRepository, IClassRepository classRepository) : IMethodService
{
    private readonly IMethodRepository _methodRepository = methodRepository;
    private readonly IClassRepository _classRepository = classRepository;
    private static int _indentacion = 0;

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
        ValidateNameLocalVariable(localVariable, method);
        method.LocalVariables.Add(localVariableT);
        _methodRepository.Update(method);
    }

    public void AddParameterToMethod(Guid methodId, ParameterDTO parameter)
    {
        Parameter parameterT = parameter.ToEntity();
        var method = _methodRepository.Find(methodId);
        ValidateMethodExist(method);
        ValidateNameParameter(parameter, method);
        method.Parameters.Add(parameterT);
        _methodRepository.Update(method);
    }

    public string ExecuteMethod(Type reference, Class objectToCreate, Method methodReference)
    {
        _indentacion = 0;
        var result = new List<string>();
        FirstLine(reference, objectToCreate, methodReference, result);
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
                var c = classType.Methods.Any(m => m.Name == calledMethod.Name);
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
                        if(attrClass.Methods.Any(m => m.Name == calledMethod.Name))
                        {
                            result.Add(
                                $"{indentation}{attr.Name}.{calledMethod.Name}() -> {attrClass.Name}.{calledMethod.Name}()");
                            break;
                        }

                        var parent = attrClass.HierarchyClass as Class;
                        while(parent != null)
                        {
                            var methodInParent = parent.Methods.FirstOrDefault(m => m.Name == calledMethod.Name);
                            if(methodInParent != null)
                            {
                                result.Add(
                                    $"{indentation}{attr.Name}.{calledMethod.Name}() -> {parent.Name}.{methodInParent.Name}()");
                                break;
                            }

                            parent = (Class)parent.HierarchyClass;
                        }
                    }
                }

                if(referenceClass.Methods.Any(m => m.Name == calledMethod.Name))
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
                    if(paramClass.Methods.Any(m => m.Name == calledMethod.Name))
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

    private static void FirstLine(Type reference, Class objectToCreate, Method methodReference, List<string> result)
    {
        var flag = false;

        if (objectToCreate.HierarchyClass != null && objectToCreate.Methods != null)
        {
            flag = objectToCreate.Methods.Any(m => m.Name == methodReference.Name);
        }

        var indentation = new string('\t', _indentacion);

        var parameters = methodReference.Parameters != null
            ? string.Join(", ", methodReference.Parameters.Select(p => p.Name))
            : string.Empty;

        if (flag)
        {
            result.Add($"{indentation}{reference.Name}.{methodReference.Name}({parameters}) -> {objectToCreate.Name}.{methodReference.Name}({parameters})");
        }
        else
        {
            result.Add($"{indentation}{reference.Name}.{methodReference.Name}({parameters}) -> {reference.Name}.{methodReference.Name}({parameters})");
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
            throw new MethodException("Method is associated with another method.");
        }
    }

    private void ValidateNameParameter(ParameterDTO parameter, Method method)
    {
        if(method.LocalVariables.Any(v => v.Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase)) ||
            method.Parameters.Any(p => p.Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new MethodException("A variable or parameter with the same name already exists.");
        }
    }

    private static void ValidateNameLocalVariable(LocalVariableDTO localVariable, Method method)
    {
        if(method.LocalVariables.Any(v => v.Name.Equals(localVariable.Name, StringComparison.OrdinalIgnoreCase)) ||
            method.Parameters.Any(p => p.Name.Equals(localVariable.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new MethodException("A variable or parameter with the same name already exists.");
        }
    }

    private void ValidateMethodIsNotInRepeted(Guid idMethod, Guid idMethodToAdd)
    {
        var method = _methodRepository.Find(idMethod);
        var methodToAdd = _methodRepository.Find(idMethodToAdd);
        if(method.Methods.Any(m => m.Id == idMethodToAdd))
        {
            throw new MethodException("Method is already associated with this method.");
        }
    }

    private void ValidateMehodIsNotInClass(Guid id)
    {
        var classes = _classRepository.FindAll();
        if(classes.Any(classT => classT.Methods.Any(method => method.Id == id)))
        {
            throw new MethodException("Method is associated with a class.");
        }
    }

    private void ValidateClassUniqueNameMethod(Method method, Class classT)
    {
        var methods = classT.Methods;

        if(methods.Any(existingMethod => existingMethod.Name.Equals(method.Name)))
        {
            throw new MethodException("A method with the same name already exists in the class.");
        }
    }

    private static void ValidateClassExist(Class classT)
    {
        if(classT == null)
        {
            throw new MethodException("Class not found.");
        }
    }

    private void ValidateMethodExist(Method method)
    {
        if(method == null)
        {
            throw new MethodException("Method null.");
        }
    }

    private static void ValidateIsNotNull(MethodDTO method)
    {
        if(method == null)
        {
            throw new MethodException("Method cannot be null.");
        }
    }

    private void ValidateUniqueMethod(Method methodT)
    {
        List<Method> methods = _methodRepository.FindAll();
        if(methods.Any(method => method.Name.Equals(methodT.Name)))
        {
            throw new MethodException("Method with the same name  already exists.");
        }
    }
}
