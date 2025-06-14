using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Simulator.UpdateDTO;
using Attribute = Simulator.Domain.Attribute;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic;

public class AttributeService(IAttributeRepository attributeRepository, IClassRepository classRepository, IInvokedMethodRepository invokeMethodRepository)
    : IAttributeService
{
    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IInvokedMethodRepository _invokeMethodRepository = invokeMethodRepository;

    public Guid CreateAttribute(Attribute attribute)
    {
        ValidateName(attribute.Name, attribute);
        var type = attribute.ReferenceType.Name;
        var instanceType = attribute.InstanceType.Name;
        var typeT = _classRepository.FindAllTypes().FirstOrDefault(c => c.Name == type);
        var instanceTypeT = _classRepository.FindAllTypes().FirstOrDefault(c => c.Name == instanceType);
        ValidateAccesibility(attribute.Accessibility);
        ValidateStereotype(attribute.Stereotype);
        ValidateAttributeType(attribute.ReferenceType);
        ValidateTypesExists(typeT, instanceTypeT);
        ValidateInstanceClass(typeT, instanceTypeT);
        AssignTypes(attribute, typeT, instanceTypeT);

        _attributeRepository.Add(attribute);
        return attribute.Id;
    }

    public void AsociateAttribute(Guid id, Guid idClass)
    {
        var attributeT = _attributeRepository.Find(id);
        var classT = _classRepository.Find(idClass);
        ValidateClassExists(idClass);
        ValidUniqueNameInClass(idClass, attributeT);
        classT.Attributes.Add(attributeT);
        _classRepository.Update(classT);
    }

    public void UpdateAttribute(Guid id, UpdateAttributeDTO uAttribute)
    {
        var attribute = _attributeRepository.Find(id);
        ValidateAttributeExist(attribute);
        ValidateAttributeIsNotInClass(attribute);
        ValidateName(uAttribute.Name, attribute);
        ValidateAccessibilityUpdate(uAttribute.Accessibility, attribute);
        ManageStereotype(uAttribute.Stereotype, attribute);
        var typeT = _classRepository.FindAllTypes().FirstOrDefault(x => x.Id.ToString() == uAttribute.IdReferenceType);
        var instanceTypeT = _classRepository.FindAllTypes().FirstOrDefault(x => x.Id.ToString() == uAttribute.IdInstanceType);
        ValidateInstanceClassUpdate(typeT, instanceTypeT, attribute);
        AssignTypesUpdate(attribute, typeT, instanceTypeT);

        _attributeRepository.Update(attribute);
    }

    public void DeleteAttribute(Guid id)
    {
        var attributeT = _attributeRepository.Find(id);
        ValidateAttributeExist(attributeT);
        ValidateAttributeIsNotInClass(attributeT);
        _attributeRepository.Delete(id);
    }

    public void SetInvokeMethodToAttribute(Guid id, Guid idInvokeGuid)
    {
        var attributeT = _attributeRepository.Find(id);
        var invokeT = _invokeMethodRepository.Find(idInvokeGuid);
        ValidateAttributeExist(attributeT);
        ValidateInvokedMethodExist(invokeT);
        ValidateTypeIsPrimitive(attributeT.ReferenceType);
        attributeT.InvokedMethods.Add(invokeT);
        _attributeRepository.Update(attributeT);
    }

    public List<Attribute> GetAttributes()
    {
        return _attributeRepository.FindAll();
    }

    private void ValidateTypeIsPrimitive(Type? attributeTType)
    {
        if(attributeTType?.GetType() != typeof(Primitive))
        {
            throw new InvalidAttributeException("In v1 of this program only primitive types are accepted.");
        }
    }

    private void ValidateInvokedMethodExist(InvokedMethod invokedMethod)
    {
        if(invokedMethod == null)
        {
            throw new InvokedMethodNotFoundException("Invoked Method not found.");
        }
    }

    private void ValidateAttributeType(Type? methodTReturnType)
    {
        var classes = _classRepository.FindAllTypes();
        if(methodTReturnType != null && classes.All(c => c.Name.ToLower() != methodTReturnType.Name.ToLower()))
        {
            throw new TypeNotFoundException("Return type not found.");
        }
    }

    private void ValidateClassExists(Guid idClass)
    {
        var classT = _classRepository.Find(idClass);
        if(classT == null)
        {
            throw new ClassNotFoundException("Class not found");
        }
    }

    private void ValidUniqueNameInClass(Guid idClass, Attribute attribute)
    {
        var classT = _classRepository.Find(idClass);

        if(classT.Attributes != null && classT.Attributes.Any(a => a.Name == attribute.Name))
        {
            throw new InvalidAttributeException(
                $"An attribute with the name '{attribute.Name}' already exists in the class");
        }
    }

    private static void ValidateAttributeExist(Attribute attributeT)
    {
        if(attributeT == null)
        {
            throw new AttributeNotFoundException("Attribute not found");
        }
    }

    private void ValidateAttributeIsNotInClass(Attribute attributeT)
    {
        var classes = _classRepository.FindAll();

        if(classes.Any(classT => classT.Attributes.Contains(attributeT)))
        {
            throw new AttributeInUseExpetion("Attribute is associated with a class");
        }
    }

    private void ValidateName(string? name, Attribute attribute)
    {
        ValidateStringIsNotEmptyOrNull(name);
        ValidateStringLength(name);
        if(name == null)
        {
            return;
        }

        attribute.Name = name;
    }

    private static void ValidateStringLength(string? name)
    {
        if(name != null && name.Length > 50)
        {
            throw new AttributeException("Name cannot exceed 50 characters");
        }
    }

    private void ValidateStringIsNotEmptyOrNull(string? name)
    {
        if(name == string.Empty)
        {
            throw new AttributeException("Name cannot be empty");
        }
    }

    private void ManageStereotype(string? newStereotype, Attribute attribute)
    {
        if(newStereotype == null)
        {
            attribute.Stereotype = null;
        }
        else if(newStereotype.ToLower() == "static")
        {
            attribute.Stereotype = KeyWord.Static;
        }
        else
        {
            throw new InvalidAttributeException("Invalid stereotype value. Attribute stereotype can only be null or static.");
        }
    }

    private void ValidateAccesibility(Accessibility aDtoAccessibility)
    {
        if(!Enum.IsDefined(typeof(Accessibility), aDtoAccessibility))
        {
            throw new InvalidAttributeException("Invalid accessibility value.");
        }
    }

    private void ValidateStereotype(KeyWord? stereotype)
    {
        if(stereotype != null && stereotype != KeyWord.Static)
        {
            throw new InvalidAttributeException("Invalid stereotype value. Attribute stereotype can only be null or static.");
        }
    }

    private void ValidateInstanceClassUpdate(Type? referenceType, Type? instanceType, Attribute attribute)
    {
        if(referenceType == null && instanceType == null)
        {
            return;
        }

        if(referenceType == null)
        {
            ValidateTypesExists(attribute.ReferenceType, instanceType);
            ValidateInstanceClass(attribute.ReferenceType, instanceType);
        }
        else if(instanceType == null)
        {
            ValidateTypesExists(referenceType, attribute.InstanceType);
            ValidateInstanceClass(referenceType, attribute.InstanceType);
        }
        else
        {
            ValidateTypesExists(referenceType, instanceType);
            ValidateInstanceClass(referenceType, instanceType);
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

    private void ValidateTypesExists(Type? referenceType, Type? instanceType)
    {
        if(referenceType is null || instanceType is null)
        {
            throw new TypeNotFoundException("Instance Type and Reference Type cannot be null.");
        }
    }

    private void AssignTypes(Attribute attribute, Type? typeT, Type? instanceTypeT)
    {
        if(typeT is Primitive p1)
        {
            attribute.ReferenceType = p1;
        }
        else if(typeT is Class c1)
        {
            attribute.ReferenceType = c1;
        }

        if(instanceTypeT is Primitive p2)
        {
            attribute.InstanceType = p2;
        }
        else if(instanceTypeT is Class c2)
        {
            attribute.InstanceType = c2;
        }
    }

    private void AssignTypesUpdate(Attribute attribute, Type? typeT, Type? instanceTypeT)
    {
        if(typeT != null)
        {
            if(typeT is Primitive p1)
            {
                attribute.ReferenceType = p1;
            }
            else if(typeT is Class c1)
            {
                attribute.ReferenceType = c1;
            }
        }

        if(instanceTypeT != null)
        {
            if(instanceTypeT is Primitive p2)
            {
                attribute.InstanceType = p2;
            }
            else if(instanceTypeT is Class c2)
            {
                attribute.InstanceType = c2;
            }
        }
    }

    private void ValidateAccessibilityUpdate(string? newAccessibility, Attribute attribute)
    {
        if(newAccessibility != null && newAccessibility.ToLower() == "public")
        {
            attribute.Accessibility = Accessibility.Public;
        }
        else if(newAccessibility != null && newAccessibility.ToLower() == "private")
        {
            attribute.Accessibility = Accessibility.Private;
        }
        else if(newAccessibility != null && newAccessibility.ToLower() == "protected")
        {
            attribute.Accessibility = Accessibility.Protected;
        }
        else if(newAccessibility != null)
        {
            return;
        }
    }
}
