using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
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
    IInvokedMethodRepository invokedMethodRepository, IMethodSignatureRepository methodSignatureRepository) : IMethodService
{
    private readonly IMethodRepository _methodRepository = methodRepository;
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IParameterRepository _parameterRepository = parameterRepository;
    private readonly ILocalVariableRepository _localVariableRepository = localVariableRepository;
    private readonly IInvokedMethodRepository _invokedMethodRepository = invokedMethodRepository;
    private readonly IMethodSignatureRepository _methodSignatureRepository = methodSignatureRepository;
    private int _identation = 0;

    public Guid CreateMethod(Method methodT)
    {
        ValidateMethodDTOExist(methodT);
        Type? returnType = null;
        ValidateAccesibility(methodT.Accessibility);
        ValidateKeyWord(methodT.Stereotype);
        string type;
        if(methodT.ReturnType != null)
        {
            if(methodT.ReturnType is Primitive primitive)
            {
                type = primitive.PrimitiveType.ToString().ToLower();
            }
            else
            {
                ValidateMethodReturnTypeString(methodT.ReturnType.Name);
                type = methodT.ReturnType.Name;
            }

            returnType = _classRepository.FindAllTypes().FirstOrDefault(c => c.Name == type);
        }

        methodT.ReturnType = returnType;
        _methodRepository.Add(methodT);
        return methodT.Id;
    }

    public Guid AssociateInvokeMethod(InvokedMethod invokedMethodT)
    {
        ValidateInvokedMethodDTOExist(invokedMethodT);
        var method = _methodRepository.Find(invokedMethodT.MethodId);
        ValidateMethodExist(method);
        if(invokedMethodT.ReferenceName == string.Empty)
        {
            throw new InvokedMethodNotFoundException("Invoked Method Reference Name cannot be empty.");
        }

        ValidateInvokedMethodReferenceNameIsNotNull(invokedMethodT.ReferenceName);
        _invokedMethodRepository.Add(invokedMethodT);
        return invokedMethodT.Id;
    }

    public void AddAMethod(Guid id, Guid invokedMethodId)
    {
        var method = _methodRepository.Find(id);
        var invokedMethod = _invokedMethodRepository.Find(invokedMethodId);
        ValidateInvokedMethodExist(invokedMethod);
        var inv = _methodRepository.Find(invokedMethod.MethodId);
        ValidateMethodExist(method);
        ValidateInvokedMethodReferenceName(invokedMethod.ReferenceName, inv, method);
        method.Methods?.Add(invokedMethod);

        _methodRepository.Update(method);
    }

    public void AssociateMethodWithClass(Guid methodId, Guid classId)
    {
        var method = _methodRepository.Find(methodId);
        ValidateMethodExist(method);
        var classT = _classRepository.Find(classId);
        ValidateClassExist(classT);
        ValidateClassUniqueNameMethod(method, classT);
        ValidateVirtualAndOverride(method, classT);
        classT.Methods.Add(method);
        _classRepository.Update(classT);
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

    public List<Method> GetMethods()
    {
        return _methodRepository.FindAll()
            .OrderBy(a => a.Name)
            .ToList();
    }

    public List<InvokedMethod> GetInvokedMethodById()
    {
        return _invokedMethodRepository.FindAll()
            .OrderBy(a => a.ReferenceName)
            .ToList();
    }

    private void ValidateMethodIsNotInInvokedMethod(Guid id)
    {
        var iVAll = _invokedMethodRepository.FindAll();
        if(iVAll.Any(iV => iV.MethodId == id))
        {
            throw new InvalidMethodException("Method is associated with invokedMethod.");
        }
    }

    public string ExecuteMethod(Guid referenceId, Guid objectToCreateId, Guid methodId, List<TypeDTO> parameterInstanceTypes)
    {
        var reference = _classRepository.FindAllTypes().FirstOrDefault(x => x.Id == referenceId);
        var objectToCreate = _classRepository.FindAll().FirstOrDefault(x => x.Id == objectToCreateId);
        var method = _methodRepository.FindAll().FirstOrDefault(x => x.Id == methodId);
        var result = new List<string>();
        _identation = 0;

        ValidateTypeExist(reference!);
        ValidateClassExist(objectToCreate!);
        ValidateMethodExist(method!);

        ValidateParameterInstanceType(method.Parameters, parameterInstanceTypes);

        result = result.Concat(FirstLine(reference!, objectToCreate!, method)).ToList();
        result = result.Concat(WriteOtherLines(objectToCreate!, method, method)).ToList();

        return string.Join("\n", result);
    }

    private List<string> WriteOtherLines(Class? oTC, Method method, Method originalMethod)
    {
        var result = new List<string>();
        for(var i = 0; i < method.Methods.Count; i++)
        {
            var invokedMethod = method.Methods[i];
            var success = false;
            var lines = result.Count;

            if(invokedMethod.ReferenceName == "this")
            {
                result = result.Concat(CaseThis(invokedMethod, originalMethod, oTC)).ToList();
                if(result.Count > lines)
                {
                    success = true;
                    lines = result.Count;
                }
            }

            if(!success && invokedMethod.ReferenceName == "base")
            {
                result = result.Concat(CaseBase(invokedMethod, oTC, originalMethod)).ToList();
                if(result.Count > lines)
                {
                    success = true;
                    lines = result.Count;
                }
            }

            if(!success && oTC.Attributes != null && oTC.Attributes.Count > 0)
            {
                result = result.Concat(CaseAttribute(oTC, invokedMethod, originalMethod)).ToList();
                if(result.Count > lines)
                {
                    success = true;
                    lines = result.Count;
                }
            }

            if(!success && method.Parameters != null && method.Parameters.Count > 0)
            {
                result = result.Concat(CaseParameter(invokedMethod, method, originalMethod)).ToList();
                if(result.Count > lines)
                {
                    success = true;
                }
            }

            if(!success && method.LocalVariables != null && method.LocalVariables.Count > 0)
            {
                result = result.Concat(CaseLocal(invokedMethod, method, originalMethod)).ToList();
            }
        }

        return result;
    }

    private List<string> CaseThis(InvokedMethod invokedMethod, Method originalMethod, Type? oTC)
    {
        var oTc = oTC as Class;
        var result = new List<string>();
        var invokedMethodMethod = _methodRepository.Find(invokedMethod.MethodId);

        oTc = FindClassWithMethod(oTc, invokedMethodMethod);
        if(CheckClassHasMethod(oTc, invokedMethodMethod))
        {
            result.Add(AddToResult(invokedMethod.ReferenceName, oTc.Name, invokedMethodMethod));

            if(CheckOriginal(invokedMethodMethod, originalMethod))
            {
                return result;
            }

            invokedMethodMethod = oTc.Methods.Find(x => x.Name == invokedMethodMethod.Name);
            result = result.Concat(ConcatNext(oTc, invokedMethodMethod!, originalMethod)).ToList();
        }
        else
        {
            if(oTc.Attributes != null)
            {
                result = result.Concat(CaseThisAttribute(oTc, invokedMethod, originalMethod)).ToList();
            }
        }

        return result;
    }

    private List<string> CaseThisAttribute(Class oTC, InvokedMethod invokedMethod, Method originalMethod)
    {
        var result = new List<string>();
        var invokedMethodMethod = _methodRepository.Find(invokedMethod.MethodId);
        var anAttribute = oTC.Attributes!.FirstOrDefault(x =>
            x.InvokedMethods != null && x.InvokedMethods!.Exists(i => i.MethodId == invokedMethod.MethodId));

        if(anAttribute == null)
        {
            return result;
        }

        var allPrimitives = _classRepository.FindAllTypes().OfType<Primitive>().ToList();
        if(anAttribute.ReferenceType is Primitive attributePrimitive2)
        {
            var aPrimitive = allPrimitives.Find(x => x.PrimitiveType == attributePrimitive2.PrimitiveType);
            if(aPrimitive != null)
            {
                result.Add(AddToResult($"this.{anAttribute.Name}", aPrimitive.PrimitiveType.ToString(), invokedMethodMethod));

                if(CheckOriginal(invokedMethodMethod, originalMethod))
                {
                    return result;
                }

                result = result.Concat(ConcatNext(oTC, invokedMethodMethod, originalMethod)).ToList();
            }
        }

        return result;
    }

    private List<string> CaseBase(InvokedMethod invokedMethod, Type? refe, Method originalMethod)
    {
        var aRefe = refe as Class;
        var result = new List<string>();
        var invokedMethodMethod = _methodRepository.Find(invokedMethod.MethodId);

        result.Add(AddToResult(invokedMethod.ReferenceName, aRefe.Parents[0].Name, invokedMethodMethod));
        if(CheckOriginal(invokedMethodMethod, originalMethod))
        {
            return result;
        }

        result = result.Concat(ConcatNext(aRefe.Parents[0] as Class, invokedMethodMethod, originalMethod)).ToList();

        return result;
    }

    private List<string> CaseAttribute(Class refe, InvokedMethod invokedMethod, Method originalMethod)
    {
        var result = new List<string>();
        var invokedMethodMethod = _methodRepository.Find(invokedMethod.MethodId);

        var anAttribute = refe.Attributes!.FirstOrDefault(x => x.Name == invokedMethod.ReferenceName);
        if(anAttribute == null)
        {
            anAttribute = FindClassWithAttribute(refe, invokedMethod.ReferenceName);
        }

        if(anAttribute != null)
        {
            var aClass = _classRepository.FindAll().FirstOrDefault(x => x.Name == anAttribute.InstanceType.Name);
            if(aClass == null)
            {
                aClass = _classRepository.FindAll().FirstOrDefault(x => x.Name == anAttribute.ReferenceType.Name);
            }

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

                    result = result.Concat(ConcatNext(aClass, invokedMethodMethod, originalMethod))
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
            Class aClass = null;
            if(aParameter.InstanceType != null)
            {
                aClass = _classRepository.FindAll().FirstOrDefault(x => x.Name == aParameter.InstanceType.Name);
            }

            if(aClass == null)
            {
                aClass = _classRepository.FindAll().FirstOrDefault(x => x.Name == aParameter.ReferenceType.Name);
            }

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

                    result = result.Concat(ConcatNext(aClass, invokedMethodMethod, originalMethod))
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
            var aClass = _classRepository.FindAll().FirstOrDefault(x => x.Name == aLocal.ReferenceType.Name);
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

                    result = result.Concat(ConcatNext(aClass, invokedMethodMethod, originalMethod))
                        .ToList();
                }
            }
        }

        return result;
    }

    private List<string> FirstLine(Type refe, Class oTC, Method method)
    {
        var result = new List<string>();

        oTC = FindClassWithMethod(oTC, method);
        var firstParenthesis = FirstLineParametersReference(method);
        var secondParenthesis = FirstLineParametersInstance(method);
        if(CheckClassHasMethod(oTC, method))
        {
            result.Add($"{refe.Name}.{method.Name}({firstParenthesis}) -> {oTC.Name}.{method.Name}({secondParenthesis})");
        }
        else
        {
            result.Add($"{refe.Name}.{method.Name}({firstParenthesis}) -> {refe.Name}.{method.Name}({secondParenthesis})");
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
        var success = false;

        for(var i = 0; i < method.Parameters.Count; i++)
        {
            Class aClass = allClasses.FirstOrDefault(x => x.Name == method.Parameters[i].ReferenceType.Name);
            if(aClass != null && aClass.Stereotype != KeyWord.Abstract)
            {
                if(i == method.Parameters.Count - 1)
                {
                    r += $"{method.Parameters[i].ReferenceType.Name}";
                }
                else
                {
                    r += $"{method.Parameters[i].ReferenceType.Name}, ";
                }

                success = true;
            }

            if(!success && method.Parameters[i].ReferenceType is Primitive p)
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

    private string FirstLineParametersReference(Method method)
    {
        var r = string.Empty;
        var allTypes = _classRepository.FindAllTypes();
        var allClasses = allTypes.OfType<Class>().ToList();
        var success = false;

        for(var i = 0; i < method.Parameters.Count; i++)
        {
            Class aClass = allClasses.FirstOrDefault(x => x.Name == method.Parameters[i].ReferenceType.Name);
            if(aClass != null && aClass.Stereotype != KeyWord.Abstract)
            {
                if(i == method.Parameters.Count - 1)
                {
                    r += $"{method.Parameters[i].ReferenceType.Name}";
                }
                else
                {
                    r += $"{method.Parameters[i].ReferenceType.Name}, ";
                }

                success = true;
            }

            if(!success && method.Parameters[i].ReferenceType is Primitive p)
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

            Type anInterface = allTypes.FirstOrDefault(x => x.Name == method.Parameters[i].ReferenceType.Name);
            if(!success && anInterface != null && anInterface is Interface)
            {
                if(i == method.Parameters.Count - 1)
                {
                    r += $"{method.Parameters[i].ReferenceType.Name}";
                }
                else
                {
                    r += $"{method.Parameters[i].ReferenceType.Name}, ";
                }
            }
        }

        return r;
    }

    private string FirstLineParametersInstance(Method method)
    {
        var r = string.Empty;
        var allTypes = _classRepository.FindAllTypes();
        var allClasses = allTypes.OfType<Class>().ToList();
        var success = false;

        for(var i = 0; i < method.Parameters.Count; i++)
        {
            Class aClass = allClasses.FirstOrDefault(x => x.Name == method.Parameters[i].InstanceType.Name);
            if(aClass != null && aClass.Stereotype != KeyWord.Abstract)
            {
                if(i == method.Parameters.Count - 1)
                {
                    r += $"{method.Parameters[i].InstanceType.Name}";
                }
                else
                {
                    r += $"{method.Parameters[i].InstanceType.Name}, ";
                }

                success = true;
            }

            if(!success && method.Parameters[i].InstanceType is Primitive p)
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

    private List<string> ConcatNext(Class? oTC, Method method, Method originalMethod)
    {
        List<string> result;

        _identation++;
        result = WriteOtherLines(oTC, method, originalMethod);
        _identation--;

        return result;
    }

    private Class FindClassWithMethod(Type? aClass, Method method)
    {
        var currentClass = aClass as Class;
        if(currentClass == null)
        {
            return null!;
        }

        Class originalClass = currentClass;

        while(currentClass != null)
        {
            if(CheckClassHasMethod(currentClass, method))
            {
                return currentClass;
            }

            if(currentClass.Parents == null || currentClass.Parents.Count == 0)
            {
                break;
            }

            currentClass = currentClass.Parents[0] as Class;
        }

        return originalClass;
    }

    private Attribute FindClassWithAttribute(Type aClass, string attributeName)
    {
        var oneClass = aClass as Class;
        if(oneClass == null)
        {
            return null!;
        }

        var anAttribute = oneClass.Attributes?.FirstOrDefault(x => x.Name == attributeName);
        if(anAttribute != null)
        {
            return anAttribute;
        }

        if(oneClass.Parents != null && oneClass.Parents.Count > 0)
        {
            return FindClassWithAttribute(oneClass.Parents[0], attributeName);
        }

        return null!;
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

    private static void ValidateTypeExist(Type type)
    {
        if(type == null)
        {
            throw new TypeNotFoundException("Type not found.");
        }
    }

    private void ValidateMethodExist(Method method)
    {
        if(method == null)
        {
            throw new MethodNotFoundException("Method null.");
        }
    }

    private void ValidateMethodReturnTypeString(string? methodTReturnType)
    {
        var classes = _classRepository.FindAllTypes();
        if(classes.All(c => c.Name != methodTReturnType))
        {
            throw new TypeNotFoundException("Method return type not found.");
        }
    }

    private void ValidateMethodDTOExist(Method method)
    {
        if(method == null)
        {
            throw new MethodNotFoundException("Method cannot be null.");
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

    private void ValidateInvokedMethodDTOExist(InvokedMethod invokedMethod)
    {
        if(invokedMethod == null || invokedMethod.MethodId == Guid.Empty)
        {
            throw new InvokedMethodNotFoundException("Invoked Method cannot be null.");
        }
    }

    private void ValidateInvokedMethodReferenceName(string invokedMethodReferenceName, Method method, Method methodR)
    {
        var classT = _classRepository.FindAll()
            .FirstOrDefault(c => c.Methods != null && c.Methods.Any(m => m.Id == methodR.Id));

        var existsInParameters = method.Parameters?.Any(p => p.Name == invokedMethodReferenceName) ?? false;
        var existsInLocalVariables = method.LocalVariables?.Any(lv => lv.Name == invokedMethodReferenceName) ?? false;

        var existsInAttributes = false;
        var existsInMethods = false;
        var existingInHeredity = false;

        if(classT != null)
        {
            var attribute = classT.Attributes?.FirstOrDefault(a => a.Name == invokedMethodReferenceName);
            existsInAttributes = attribute != null;
            if(!existsInAttributes)
            {
                ManageAttribute(methodR, attribute);
            }

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
        if(attribute?.ReferenceType is Class classAttrType)
        {
            var attrClass = _classRepository.Find(classAttrType.Id);
            if(!HasMethodInHierarchy(attrClass, method.Id))
            {
                throw new MethodException(
                    $"The method with ID '{method.Id}' does not exist in the class of attribute '{attribute.Name}' or its ancestors.");
            }
        }
    }

    private void ManageLocalVariable(Method method, LocalVariable? localVar)
    {
        if(localVar?.ReferenceType is Class classLocalVarType)
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
        if(param?.ReferenceType is Class classParamType)
        {
            var paramClass = _classRepository.Find(classParamType.Id);
            if(paramClass == null || !HasMethodInHierarchy(paramClass, method.Id))
            {
                throw new MethodException(
                    $"The method with ID '{method.Id}' does not exist in the class of parameter '{param.Name}' or its ancestors.");
            }
        }
    }

    private bool HasMethodInHierarchy(Type targetClass, Guid methodId)
    {
        while(targetClass is Class currentClass)
        {
            if(currentClass.Methods?.Any(m => m.Id == methodId) == true)
            {
                return true;
            }

            targetClass = currentClass.Parents?.FirstOrDefault();
        }

        return false;
    }

    private static bool InvokeMethodReferenceBase(string invokedMethodReferenceName, Method method, Class classT,
        bool existingInHeredity)
    {
        if(invokedMethodReferenceName == "base" && classT.Parents != null)
        {
            existingInHeredity = classT.Parents[0].Methods?.Any(m => m.Name == method.Name) ?? false;
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

    private void ValidateVirtualAndOverride(Method methodChild, Class classChild)
    {
        if(methodChild.IsOverride)
        {
            if(classChild.Parents == null || classChild.Parents.Count == 0)
            {
                throw new MethodException("There is no parent class with this method.");
            }

            var classParent = classChild.Parents[0] as Class;
            if(classParent == null)
            {
                throw new MethodException("There is no parent class with this method.");
            }

            var methodParent = classParent.Methods?.FirstOrDefault(x => x.Name == methodChild.Name);
            if(methodParent == null)
            {
                try
                {
                    ValidateVirtualAndOverride(methodChild, classParent);
                }
                catch(MethodException)
                {
                    throw new MethodException("There is no parent class with this method.");
                }
            }
            else if(methodParent.Stereotype != KeyWord.Virtual)
            {
                throw new MethodException("Parent class must have this method as virtual.");
            }
        }
    }

    public Guid CreateMethodSignature(MethodSignature methodSignatureT)
    {
        ValidateMethodDtoIsNotNull(methodSignatureT);
        ValidateMethodSignatureNameIsNotNull(methodSignatureT);
        var type = methodSignatureT!.ReturnType?.Name;
        var returnType = _classRepository.FindAllTypes().FirstOrDefault(t => t.Name == type);
        methodSignatureT.ReturnType = returnType;
        _methodSignatureRepository.Add(methodSignatureT);
        return methodSignatureT.Id;
    }

    public List<MethodSignature> GetMethodSignatures()
    {
        return _methodSignatureRepository.FindAll()
            .OrderBy(a => a.Name)
            .ToList();
    }

    private static void ValidateMethodSignatureNameIsNotNull(MethodSignature methodSignatureDto)
    {
        if(string.IsNullOrEmpty(methodSignatureDto.Name))
        {
            throw new InvalidMethodSignatureException("MethodSignature name cannot be null.");
        }
    }

    private static void ValidateMethodDtoIsNotNull(MethodSignature methodSignatureDto)
    {
        if(methodSignatureDto == null)
        {
            throw new MethodSignatureNotFoundException("MethodSignature cannot be null.");
        }
    }

    private void ValidateInstanceClass(Type? referenceType, Type? instanceType)
    {
        if(BothPrimitiveButDifferent(referenceType, instanceType) || ReferenceNotPrimitive(referenceType, instanceType) || InstanceNotPrimitive(referenceType, instanceType))
        {
            throw new LocalVariableException("Invalid reference type or instance type. If one is primitive, both should be primitive. If both are, then they must be the same.");
        }

        if(referenceType is Class referenceClass && instanceType is Class instanceClass)
        {
            var res = CaseReferenceClassRecursive(referenceClass, instanceClass);
            if(!res)
            {
                throw new LocalVariableException("Instance Type is not a subclass of Reference Type");
            }
        }
        else if(referenceType is Interface referenceInterface2 && instanceType is Class instanceClass2)
        {
            var res = CaseReferenceInterfaceRecursive(referenceInterface2, instanceClass2);
            if(!res)
            {
                throw new LocalVariableException("Instance Type is not a subclass of Reference Type");
            }
        }
        else if(referenceType is Class && instanceType is Interface)
        {
            throw new LocalVariableException("Instance Type is not a subclass of Reference Type");
        }
        else if(referenceType is Interface referenceInterface && instanceType is Interface instanceInterface && referenceInterface != instanceInterface)
        {
            throw new LocalVariableException("Instance Type is not a subclass of Reference Type");
        }
    }

    private bool CaseReferenceInterfaceRecursive(Interface reference, Class instance)
    {
        if(instance.Parents is null)
        {
            return false;
        }

        var aux = new bool[instance.Parents.Count];

        for(var i = 0; i < instance.Parents.Count; i++)
        {
            if(instance.Parents[i] is Class c)
            {
                aux[i] = CaseReferenceInterfaceRecursive(reference, c);
            }
            else if(instance.Parents[i] is Interface inter && reference == inter)
            {
                aux[i] = true;
            }
        }

        return aux.Any(x => x);
    }

    private bool CaseReferenceClassRecursive(Class reference, Class instance)
    {
        if(reference == instance)
        {
            return true;
        }

        if(instance.Parents is null)
        {
            return false;
        }

        var aux = new bool[instance.Parents.Count];
        for(var i = 0; i < instance.Parents.Count; i++)
        {
            if(instance.Parents[i] is Class c)
            {
                aux[i] = CaseReferenceClassRecursive(reference, c);
            }
        }

        return aux.Any(x => x);
    }

    private bool InstanceNotPrimitive(Type? referenceType, Type? instanceType)
    {
        return instanceType is not Primitive && referenceType is Primitive;
    }

    private bool ReferenceNotPrimitive(Type? referenceType, Type? instanceType)
    {
        return instanceType is Primitive && referenceType is not Primitive;
    }

    private bool BothPrimitiveButDifferent(Type? referenceType, Type? instanceType)
    {
        return instanceType is Primitive a && referenceType is Primitive b && a.PrimitiveType != b.PrimitiveType;
    }

    private void ValidateParameterInstanceType(List<Parameter>? parameters, List<TypeDTO> types)
    {
        if(parameters.Count != types.Count)
        {
            throw new ParameterException("The number of types is not the same as the number of parameters.");
        }

        var allTypes = _classRepository.FindAllTypes();
        for(var i = 0; i < parameters.Count; i++)
        {
            var type = allTypes.FirstOrDefault(x => x.Name == types[i].Name);
            ValidateInstanceClass(parameters[i].ReferenceType, type);
            parameters[i].InstanceType = type;
        }
    }
}
