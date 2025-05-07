using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic;

public class MethodService(
    IMethodRepository methodRepository,
    IClassRepository classRepository,
    IParameterRepository parameterRepository,
    ILocalVariableRepository localVariableRepository,
    IInvokedMethodRepository invokedMethodRepository) : IMethodService
{
    private readonly IMethodRepository _methodRepository = methodRepository;
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IParameterRepository _parameterRepository = parameterRepository;
    private readonly ILocalVariableRepository _localVariableRepository = localVariableRepository;
    private readonly IInvokedMethodRepository _invokedMethodRepository = invokedMethodRepository;
    private int _identation = 0;

    public Guid CreateMethod(MethodDTO method)
    {
        ValidateMethodDTOExist(method);
        var methodT = method.ToEntity();
        Type? returnType = null;
        ValidateAccesibility(methodT.Accessibility);
        ValidateKeyWord(methodT.Stereotype);
        if(methodT.ReturnType != null)
        {
            ValidateMethodReturnTypeString(methodT.ReturnType.Name);
            var type = methodT.ReturnType.Name;
            returnType = _classRepository.FindAllTypes().FirstOrDefault(c => c.Name == type);
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
        if(invokedMethod.ReferenceName == string.Empty)
        {
            throw new InvokedMethodNotFoundException("Invoked Method Reference Name cannot be empty.");
        }

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
        ValidateInvokedMethodExist(invokedMethod);
        ValidateInvokedMethodReferenceName(invokedMethod.ReferenceName, method);
        method.Methods.Add(invokedMethod);
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
        if(localVariable.Name == string.Empty)
        {
            throw new LocalVariableNotFoundException("Local Variable Name cannot be empty.");
        }

        var localVariableT = localVariable.ToEntity();
        var type = localVariableT.LocalVariableType.Name;
        var returnType = _classRepository.FindAllTypes().FirstOrDefault(c => c.Name == type);
        localVariableT.LocalVariableType = returnType;
        ValidateMethodReturnType(localVariableT.LocalVariableType);
        _localVariableRepository.Add(localVariableT);
        return localVariableT.Id;
    }

    public void SetInvokeMethodToLocalVariable(Guid id, Guid idInvokeGuid)
    {
        var localVariable = _localVariableRepository.Find(id);
        var invokedMethod = _invokedMethodRepository.Find(idInvokeGuid);
        ValidateLocalVariableExist(localVariable);
        ValidateInvokedMethodExist(invokedMethod);
        ValidateTypeIsPrimitive(localVariable.LocalVariableType);
        localVariable.InvokedMethod = invokedMethod;
        _localVariableRepository.Update(localVariable);
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
        if(parameter.Name == string.Empty)
        {
            throw new LocalVariableNotFoundException("Parameter Name cannot be empty.");
        }

        var type = parameterT.ParameterType.Name;
        var returnType = _classRepository.FindAllTypes().FirstOrDefault(c => c.Name == type);
        parameterT.ParameterType = returnType;
        ValidateMethodReturnType(parameterT.ParameterType);
        _parameterRepository.Add(parameterT);
        return parameterT.Id;
    }

    public void SetInvokeMethodToParameter(Guid id, Guid idInvokeGuid)
    {
        var parameter = _parameterRepository.Find(id);
        var invokedMethod = _invokedMethodRepository.Find(idInvokeGuid);
        ValidateParameterExist(parameter);
        ValidateInvokedMethodExist(invokedMethod);
        ValidateTypeIsPrimitive(parameter.ParameterType);
        parameter.InvokedMethod = invokedMethod;
        _parameterRepository.Update(parameter);
    }

    private void ValidateTypeIsPrimitive(Type? attributeTType)
    {
        if(attributeTType == null || attributeTType is not Primitive)
        {
            throw new InvalidAttributeException("In v1 of this program only primitive types are accepted.");
        }
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
        ValidateMethodIsNotInInvokedMethod(id);
        _methodRepository.Delete(id);
    }

    private void ValidateMethodIsNotInInvokedMethod(Guid id)
    {
        var iVAll = _invokedMethodRepository.FindAll();
        if(iVAll.Any(iV => iV.MethodId == id))
        {
            throw new InvalidMethodException("Method is associated with invokedMethod.");
        }
    }

    public string ExecuteMethod(ClassDTO referenceDTO, ClassDTO objectToCreateDTO, MethodDTO methodDTO)
    {
        var reference = _classRepository.FindAll().FirstOrDefault(x => x.Name == referenceDTO.Name);
        var objectToCreate = _classRepository.FindAll().FirstOrDefault(x => x.Name == objectToCreateDTO.Name);
        Method method = null;
        var result = new List<string>();
        _identation = 0;

        method = FindMethod(reference.Methods!, methodDTO.Name);

        result = result.Concat(FirstLine(reference, objectToCreate!, method)).ToList();
        result = result.Concat(WriteOtherLines(reference, objectToCreate!, method, method)).ToList();

        return string.Join("\n", result);
    }

    private List<string> WriteOtherLines(Class refe, Class oTC, Method method, Method originalMethod)
    {
        var result = new List<string>();
        for(var i = 0; i < method.Methods.Count; i++)
        {
            var invokedMethod = method.Methods[i];
            var success = false;
            var lines = result.Count;

            if(invokedMethod.ReferenceName == "this")
            {
                result = result.Concat(CaseThis(invokedMethod, originalMethod, oTC, refe)).ToList();
                if(result.Count > lines)
                {
                    success = true;
                    lines = result.Count;
                }
            }

            if(!success && invokedMethod.ReferenceName == "base")
            {
                result = result.Concat(CaseBase(invokedMethod, refe, originalMethod)).ToList();
                if(result.Count > lines)
                {
                    success = true;
                    lines = result.Count;
                }
            }

            if(!success && refe.Attributes != null)
            {
                result = result.Concat(CaseAttribute(refe, invokedMethod, originalMethod)).ToList();
                if(result.Count > lines)
                {
                    success = true;
                    lines = result.Count;
                }
            }

            if(!success && method.Parameters != null)
            {
                result = result.Concat(CaseParameter(invokedMethod, method, originalMethod)).ToList();
                if(result.Count > lines)
                {
                    success = true;
                }
            }

            if(!success && method.LocalVariables != null)
            {
                result = result.Concat(CaseLocal(invokedMethod, method, originalMethod)).ToList();
            }
        }

        return result;
    }

    private List<string> CaseThis(InvokedMethod invokedMethod, Method originalMethod, Class oTC, Class refe)
    {
        var result = new List<string>();
        var invokedMethodMethod = _methodRepository.Find(invokedMethod.MethodId);

        var aClass = oTC;
        aClass = FindClassWithMethod(aClass, invokedMethodMethod);
        if(CheckClassHasMethod(aClass, invokedMethodMethod))
        {
            result.Add(AddToResult(invokedMethod.ReferenceName, aClass.Name, invokedMethodMethod));

            if(CheckOriginal(invokedMethodMethod, originalMethod))
            {
                return result;
            }

            invokedMethodMethod = aClass.Methods.Find(x => x.Name == invokedMethodMethod.Name);
            result = result.Concat(ConcatNext(oTC, oTC, invokedMethodMethod!, originalMethod)).ToList();
        }
        else
        {
            if(oTC.Attributes != null)
            {
                var anAttribute = oTC.Attributes.FirstOrDefault(x =>
                    x.InvokedMethod != null && x.InvokedMethod.MethodId == invokedMethod.MethodId);
                var allPrimitives = _classRepository.FindAllTypes().OfType<Primitive>().ToList();
                var aPrimitive = allPrimitives.Find(x => anAttribute.Type is Primitive p && x.PrimitiveType.ToString().ToLower() == p.PrimitiveType.ToString().ToLower());

                if(anAttribute != null && aPrimitive != null)
                {
                    result.Add(AddToResult($"this.{anAttribute.Name}", aPrimitive.PrimitiveType.ToString(), invokedMethodMethod));

                    if(CheckOriginal(invokedMethodMethod, originalMethod))
                    {
                        return result;
                    }

                    result = result.Concat(ConcatNext(oTC, oTC, invokedMethodMethod, originalMethod)).ToList();
                }
            }
        }

        return result;
    }

    private List<string> CaseBase(InvokedMethod invokedMethod, Class refe, Method originalMethod)
    {
        var result = new List<string>();
        var invokedMethodMethod = _methodRepository.Find(invokedMethod.MethodId);

        result.Add(AddToResult(invokedMethod.ReferenceName, refe.HierarchyClass.Name,  invokedMethodMethod));
        if(CheckOriginal(invokedMethodMethod, originalMethod))
        {
            return result;
        }

        result = result.Concat(ConcatNext(refe.HierarchyClass, refe.HierarchyClass, invokedMethodMethod, originalMethod)).ToList();

        return result;
    }

    private List<string> CaseAttribute(Class refe, InvokedMethod invokedMethod, Method originalMethod)
    {
        var result = new List<string>();
        var invokedMethodMethod = _methodRepository.Find(invokedMethod.MethodId);

        var anAttribute = refe.Attributes!.FirstOrDefault(x => x.Name == invokedMethod.ReferenceName);
        if(anAttribute != null)
        {
            var aClass = _classRepository.FindAll().FirstOrDefault(x => x.Name == anAttribute.Type.Name);
            if(aClass != null)
            {
                aClass = FindClassWithMethod(aClass, invokedMethodMethod);
                if(CheckClassHasMethod(aClass, invokedMethodMethod))
                {
                    result.Add(AddToResult(anAttribute.Name, aClass.Name, invokedMethodMethod));

                    if(CheckOriginal(invokedMethodMethod, originalMethod))
                    {
                        return result;
                    }

                    result = result.Concat(ConcatNext(aClass, aClass, invokedMethodMethod, originalMethod))
                        .ToList();
                }
            }
        }

        return result;
    }

    private List<string> CaseParameter(InvokedMethod invokedMethod, Method method, Method originalMethod)
    {
        var result = new List<string>();
        var invokedMethodMethod = _methodRepository.Find(invokedMethod.MethodId);

        var aParameter = method.Parameters!.FirstOrDefault(x => x.Name == invokedMethod.ReferenceName);
        if(aParameter != null)
        {
            var aClass = _classRepository.FindAll().FirstOrDefault(x => x.Name == aParameter.ParameterType.Name);
            if(aClass != null)
            {
                aClass = FindClassWithMethod(aClass, invokedMethodMethod);
                if(CheckClassHasMethod(aClass, invokedMethodMethod))
                {
                    result.Add(AddToResult(aParameter.Name, aClass.Name, invokedMethodMethod));

                    if(CheckOriginal(invokedMethodMethod, originalMethod))
                    {
                        return result;
                    }

                    result = result.Concat(ConcatNext(aClass, aClass, invokedMethodMethod, originalMethod))
                        .ToList();
                }
            }
        }

        return result;
    }

    private List<string> CaseLocal(InvokedMethod invokedMethod, Method method, Method originalMethod)
    {
        var result = new List<string>();
        var invokedMethodMethod = _methodRepository.Find(invokedMethod.MethodId);

        var aLocal = method.LocalVariables!.FirstOrDefault(x => x.Name == invokedMethod.ReferenceName);
        if(aLocal != null)
        {
            var aClass = _classRepository.FindAll().FirstOrDefault(x => x.Name == aLocal.LocalVariableType.Name);
            if(aClass != null)
            {
                aClass = FindClassWithMethod(aClass, invokedMethodMethod);
                if(CheckClassHasMethod(aClass, invokedMethodMethod))
                {
                    result.Add(AddToResult(aLocal.Name, aClass.Name, invokedMethodMethod));
                    if(CheckOriginal(invokedMethodMethod, originalMethod))
                    {
                        return result;
                    }

                    result = result.Concat(ConcatNext(aClass, aClass, invokedMethodMethod, originalMethod))
                        .ToList();
                }
            }
        }

        return result;
    }

    private Method FindMethod(List<Method> referenceMethods, string methodName)
    {
        var success = false;
        Method method = null;

        for(var i = 0; i < referenceMethods.Count && !success; i++)
        {
            method = referenceMethods.FirstOrDefault(x => x.Name == methodName);
            if(method != null)
            {
                success = true;
            }
        }

        return method;
    }

    private List<string> FirstLine(Class refe, Class oTC, Method method)
    {
        var result = new List<string>();

        var parenthesis = ParametersInParenthesis(method);
        if(CheckClassHasMethod(oTC, method))
        {
            result.Add($"{refe.Name}.{method.Name}({parenthesis}) -> {oTC.Name}.{method.Name}({parenthesis})");
        }
        else
        {
            result.Add($"{refe.Name}.{method.Name}({parenthesis}) -> {refe.Name}.{method.Name}({parenthesis})");
        }

        _identation++;

        return result;
    }

    private string AddToResult(string objName, string className, Method method)
    {
        var indentation = new string('\t', _identation);
        var parenthesis = ParametersInParenthesis(method);

        return $"{indentation}{objName}.{method.Name}({parenthesis}) -> {className}.{method.Name}({parenthesis})";
    }

    private string ParametersInParenthesis(Method method)
    {
        var r = string.Empty;
        var allTypes = _classRepository.FindAllTypes();
        var allClasses = allTypes.OfType<Class>().ToList();
        var allPrimitives = allTypes.OfType<Primitive>().ToList();
        var success = false;

        for(var i = 0; i < method.Parameters.Count; i++)
        {
            Class aClass = allClasses.FirstOrDefault(x => x.Name == method.Parameters[i].ParameterType.Name);
            if(aClass != null && aClass.Stereotype != KeyWord.Abstract)
            {
                if(i == method.Parameters.Count - 1)
                {
                    r += $"{method.Parameters[i].ParameterType.Name}";
                }
                else
                {
                    r += $"{method.Parameters[i].ParameterType.Name}, ";
                }

                success = true;
            }

            if(!success && method.Parameters[i].ParameterType is Primitive p)
            {
                if(i == method.Parameters.Count - 1)
                {
                    r += $"{p.PrimitiveType.ToString().ToLower()}";
                }
                else
                {
                    r += $"{p.PrimitiveType.ToString().ToLower()}, ";
                }
            }
        }

        return r;
    }

    private bool CheckOriginal(Method actualMethod, Method originalMethod)
    {
        return actualMethod.Equals(originalMethod);
    }

    private List<string> ConcatNext(Class refe, Class oTC, Method method, Method originalMethod)
    {
        List<string> result = new List<string>();

        _identation++;
        result = WriteOtherLines(refe, oTC, method, originalMethod);
        _identation--;

        return result;
    }

    private Class FindClassWithMethod(Class aClass, Method method)
    {
        if(aClass.HierarchyClass != null && !CheckClassHasMethod(aClass, method))
        {
            return FindClassWithMethod(aClass.HierarchyClass, method);
        }

        return aClass;
    }

    private bool CheckClassHasMethod(Class aClass, Method method)
    {
        return aClass.Methods != null && aClass.Methods.Any(x => x.Name == method.Name);
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
        if(method.LocalVariables != null && method.Parameters != null &&
           (method.LocalVariables.Any(v => v.Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase)) ||
            method.Parameters.Any(p => p.Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase))))
        {
            throw new InvalidMethodException("A variable or parameter with the same name already exists.");
        }
    }

    private static void ValidateNameLocalVariable(LocalVariable localVariable, Method method)
    {
        if(method.LocalVariables != null && method.Parameters != null &&
           (method.LocalVariables.Any(v => v.Name.Equals(localVariable.Name, StringComparison.OrdinalIgnoreCase)) ||
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
        var classes = _classRepository.FindAllTypes();
        if(classes.All(c => c.Name != methodTReturnType))
        {
            throw new MethodException("Method return type not found.");
        }
    }

    private void ValidateMethodReturnType(Type? methodTReturnType)
    {
        var classes = _classRepository.FindAllTypes();
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
        if(invokedMethod == null || invokedMethod.MethodId == Guid.Empty)
        {
            throw new InvokedMethodNotFoundException("Invoked Method cannot be null.");
        }
    }

    private void ValidateInvokedMethodReferenceName(string invokedMethodReferenceName, Method method)
    {
        var classT = _classRepository.FindAll()
            .FirstOrDefault(c => c.Methods != null && c.Methods.Any(m => m.Id == method.Id));

        var existsInParameters = method.Parameters?.Any(p => p.Name == invokedMethodReferenceName) ?? false;
        var existsInLocalVariables = method.LocalVariables?.Any(lv => lv.Name == invokedMethodReferenceName) ?? false;

        var existsInAttributes = false;
        var existsInMethods = false;
        var existingInHeredity = false;

        if(classT != null)
        {
            var attribute = classT.Attributes?.FirstOrDefault(a => a.Name == invokedMethodReferenceName);
            existsInAttributes = attribute != null;
            ManageAttribute(method, attribute);
            existsInMethods = InvokeMethodReferenceThis(invokedMethodReferenceName, method, existsInMethods, classT);
            existingInHeredity = InvokeMethodReferenceBase(invokedMethodReferenceName, method, classT, existingInHeredity);
        }

        ManageParams(invokedMethodReferenceName, method);
        var localVar = method.LocalVariables?.FirstOrDefault(v => v.Name == invokedMethodReferenceName);
        ManageLocalVariable(method, localVar);

        if(!existsInParameters && !existsInLocalVariables && !existsInAttributes && !existsInMethods &&
           !existingInHeredity)
        {
            throw new MethodException(
                $"Reference name '{invokedMethodReferenceName}' does not match any parameter, local variable, or attribute in method '{method.Name}'.");
        }
    }

    private void ManageAttribute(Method method, Attribute? attribute)
    {
        if(attribute?.Type is Class classAttrType)
        {
            var attrClass = _classRepository.Find(classAttrType.Id);
            if(attrClass == null || !HasMethodInHierarchy(attrClass, method.Id))
            {
                throw new MethodException(
                    $"The method with ID '{method.Id}' does not exist in the class of attribute '{attribute.Name}' or its ancestors.");
            }
        }
    }

    private void ManageLocalVariable(Method method, LocalVariable? localVar)
    {
        if(localVar?.LocalVariableType is Class classLocalVarType)
        {
            var localVarClass = _classRepository.Find(classLocalVarType.Id);
            if(localVarClass == null || !HasMethodInHierarchy(localVarClass, method.Id))
            {
                throw new MethodException(
                    $"The method with ID '{method.Id}' does not exist in the class of local variable '{localVar.Name}' or its ancestors.");
            }
        }
    }

    private void ManageParams(string invokedMethodReferenceName, Method method)
    {
        var param = method.Parameters?.FirstOrDefault(p => p.Name == invokedMethodReferenceName);
        if(param?.ParameterType is Class classParamType)
        {
            var paramClass = _classRepository.Find(classParamType.Id);
            if(paramClass == null || !HasMethodInHierarchy(paramClass, method.Id))
            {
                throw new MethodException(
                    $"The method with ID '{method.Id}' does not exist in the class of parameter '{param.Name}' or its ancestors.");
            }
        }
    }

    private bool HasMethodInHierarchy(Class targetClass, Guid methodId)
    {
        while(targetClass != null)
        {
            if(targetClass.Methods?.Any(m => m.Id == methodId) == true)
            {
                return true;
            }

            targetClass = targetClass.HierarchyClass;
        }

        return false;
    }

    private static bool InvokeMethodReferenceBase(string invokedMethodReferenceName, Method method, Class classT,
        bool existingInHeredity)
    {
        if(invokedMethodReferenceName == "base" && classT.HierarchyClass != null)
        {
            existingInHeredity = classT.HierarchyClass.Methods?.Any(m => m.Name == method.Name) ?? false;
        }

        return existingInHeredity;
    }

    private static bool InvokeMethodReferenceThis(string invokedMethodReferenceName, Method method, bool existsInMethods, Class classT)
    {
        if(invokedMethodReferenceName == "this")
        {
            existsInMethods = classT.Methods?.Any(m => m.Name == method.Name) ?? false;
        }

        return existsInMethods;
    }

    private void ValidateInvokedMethodExist(InvokedMethod invokedMethod)
    {
        if(invokedMethod == null)
        {
            throw new InvokedMethodNotFoundException("Invoked Method not found.");
        }
    }

    private void ValidateKeyWord(KeyWord? stereotype)
    {
        if(stereotype != null && !Enum.IsDefined(typeof(KeyWord), stereotype))
        {
            throw new InvalidMethodException("Invalid stereotype value.");
        }
    }

    private void ValidateAccesibility(Accessibility methodTAccessibility)
    {
        if(!Enum.IsDefined(typeof(Accessibility), methodTAccessibility))
        {
            throw new InvalidMethodException("Invalid accessibility value.");
        }
    }
}
