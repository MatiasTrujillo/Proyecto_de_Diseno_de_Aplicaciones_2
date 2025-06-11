using System;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic;

public class ParameterService(IParameterRepository parameterRepository, IClassRepository classRepository, IInvokedMethodRepository invokedMethodRepository) : IParameterService
{
    private readonly IParameterRepository _parameterRepository = parameterRepository;
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IInvokedMethodRepository _invokedMethodRepository = invokedMethodRepository;

    public Guid CreateParameter(ParameterDTO parameter)
    {
        ValidateParameterIsNotNull(parameter);
        var parameterT = parameter.ToEntity();
        var type = parameterT.ReferenceType.Name;
        var insstance = parameterT.InstanceType.Name;
        var returnType = _classRepository.FindAllTypes().FirstOrDefault(c => c.Name == type);
        var instanceType = _classRepository.FindAllTypes().FirstOrDefault(c => c.Name == insstance);
        ValidateInstanceClass(returnType, instanceType);
        parameterT.ReferenceType = returnType;
        parameterT.InstanceType = instanceType;
        _parameterRepository.Add(parameterT);
        return parameterT.Id;
    }

    public void SetInvokeMethodToParameter(Guid id, Guid idInvokeGuid)
    {
        var parameter = _parameterRepository.Find(id);
        var invokedMethod = _invokedMethodRepository.Find(idInvokeGuid);
        ValidateParameterExist(parameter);
        ValidateInvokedMethodExist(invokedMethod);
        ValidateTypeIsPrimitive(parameter.ReferenceType);
        parameter.InvokedMethods.Add(invokedMethod);
        _parameterRepository.Update(parameter);
    }

    private void ValidateTypeIsPrimitive(Type? attributeTType)
    {
        if(attributeTType?.GetType() != typeof(Class) && attributeTType?.GetType() != typeof(Primitive))
        {
            throw new InvalidAttributeException("In v2 of this program only Class and Primitive types are accepted.");
        }
    }

    private void ValidateInvokedMethodExist(InvokedMethod invokedMethod)
    {
        if(invokedMethod == null)
        {
            throw new InvokedMethodNotFoundException("Invoked Method not found.");
        }
    }

    private void ValidateParameterExist(Parameter parameter)
    {
        if(parameter == null)
        {
            throw new ParameterNotFoundException("Parameter not found.");
        }
    }

    private void ValidateParameterIsNotNull(ParameterDTO parameter)
    {
        if(parameter == null)
        {
            throw new ParameterNotFoundException("Parameter cannot be null.");
        }
    }

    private void ValidateInstanceClass(Type? referenceType, Type? instanceType)
    {
        if(BothPrimitiveButDifferent(referenceType, instanceType) || ReferenceNotPrimitive(referenceType, instanceType) || InstanceNotPrimitive(referenceType, instanceType))
        {
            throw new InvalidAttributeException("Invalid reference type or instance type. If one is primitive, both should be primitive. If both are, then they must be the same.");
        }

        if(referenceType is Class referenceClass && instanceType is Class instanceClass)
        {
            var res = CaseReferenceClassRecursive(referenceClass, instanceClass);
            if(!res)
            {
                throw new InvalidAttributeException("Instance Type is not a subclass of Reference Type");
            }
        }
        else if(referenceType is Interface referenceInterface2 && instanceType is Class instanceClass2)
        {
            var res = CaseReferenceInterfaceRecursive(referenceInterface2, instanceClass2);
            if(!res)
            {
                throw new InvalidAttributeException("Instance Type is not a subclass of Reference Type");
            }
        }
        else if(referenceType is Class && instanceType is Interface)
        {
            throw new InvalidAttributeException("Instance Type is not a subclass of Reference Type");
        }
        else if(referenceType is Interface referenceInterface && instanceType is Interface instanceInterface && referenceInterface != instanceInterface)
        {
            throw new InvalidAttributeException("Instance Type is not a subclass of Reference Type");
        }
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
}
