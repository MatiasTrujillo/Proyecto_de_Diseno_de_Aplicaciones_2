using System;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic;

public class LocalVariableService(ILocalVariableRepository localVariableRepository, IClassRepository classRepository, IInvokedMethodRepository invokedMethodRepository) : ILocalVariableService
{
    private readonly ILocalVariableRepository _localVariableRepository = localVariableRepository;
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IInvokedMethodRepository _invokedMethodRepository = invokedMethodRepository;

    public Guid CreateLocalVariable(LocalVariableDTO localVariable)
    {
        ValidateLocalVariableIsNotNull(localVariable);
        if(localVariable.Name == string.Empty)
        {
            throw new LocalVariableNotFoundException("Local Variable Name cannot be empty.");
        }

        var localVariableT = localVariable.ToEntity();
        var referenceTypeName = localVariableT.ReferenceType.Name;
        var referenceType = _classRepository.FindAllTypes().FirstOrDefault(c => c.Name.ToLower() == referenceTypeName.ToLower());
        var instanceTypeName = localVariableT.InstanceType.Name;
        var instanceType = _classRepository.FindAllTypes().FirstOrDefault(c => c.Name.ToLower() == instanceTypeName.ToLower());
        ValidateTypesExists(referenceType, instanceType);
        ValidateInstanceClass(referenceType, instanceType);
        AssignTypes(localVariableT, referenceType, instanceType);

        _localVariableRepository.Add(localVariableT);
        return localVariableT.Id;
    }

    public void SetInvokeMethodToLocalVariable(Guid id, Guid idInvokeGuid)
    {
        var localVariable = _localVariableRepository.Find(id);
        var invokedMethod = _invokedMethodRepository.Find(idInvokeGuid);
        ValidateLocalVariableExist(localVariable);
        ValidateInvokedMethodExist(invokedMethod);
        ValidateTypeIsPrimitive(localVariable.ReferenceType);
        localVariable.InvokedMethods.Add(invokedMethod);
        _localVariableRepository.Update(localVariable);
    }

    private void ValidateLocalVariableIsNotNull(LocalVariableDTO parameter)
    {
        if(parameter == null)
        {
            throw new LocalVariableNotFoundException("Local Variable cannot be null.");
        }
    }

    private void ValidateTypesExists(Type? referenceType, Type? instanceType)
    {
        if(referenceType is null || instanceType is null)
        {
            throw new TypeNotFoundException("Instance Type and Reference Type cannot be null.");
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

    private bool BothPrimitiveButDifferent(Type? referenceType, Type? instanceType)
    {
        return instanceType is Primitive a && referenceType is Primitive b && a.PrimitiveType != b.PrimitiveType;
    }

    private bool ReferenceNotPrimitive(Type? referenceType, Type? instanceType)
    {
        return instanceType is Primitive && referenceType is not Primitive;
    }

    private bool InstanceNotPrimitive(Type? referenceType, Type? instanceType)
    {
        return instanceType is not Primitive && referenceType is Primitive;
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

    private void AssignTypes(LocalVariable localVariable, Type? typeT, Type? instanceTypeT)
    {
        if(typeT is Primitive p1)
        {
            localVariable.ReferenceType = p1;
        }
        else if(typeT is Class c1)
        {
            localVariable.ReferenceType = c1;
        }

        if(instanceTypeT is Primitive p2)
        {
            localVariable.InstanceType = p2;
        }
        else if(instanceTypeT is Class c2)
        {
            localVariable.InstanceType = c2;
        }
    }

    private void ValidateLocalVariableExist(LocalVariable localVariableT)
    {
        if(localVariableT == null)
        {
            throw new LocalVariableNotFoundException("Local Variable not found.");
        }
    }

    private void ValidateInvokedMethodExist(InvokedMethod invokedMethod)
    {
        if(invokedMethod == null)
        {
            throw new InvokedMethodNotFoundException("Invoked Method not found.");
        }
    }

    private void ValidateTypeIsPrimitive(Type? attributeTType)
    {
        if(attributeTType?.GetType() != typeof(Class) && attributeTType?.GetType() != typeof(Primitive))
        {
            throw new InvalidAttributeException("In v2 of this program only Class and Primitive types are accepted.");
        }
    }
}
