using System.Reflection;
using Moq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class ParameterService_Test
{
    private Mock<IMethodRepository> _mockMethodRepository = null!;
    private Mock<IClassRepository> _mockClassRepository = null!;
    private Mock<IParameterRepository> _mockParameterRepository = null!;
    private Mock<ILocalVariableRepository> _mockLocalVariableRepository = null!;
    private Mock<IInvokedMethodRepository> _mockInvokedMethodRepository = null!;
    private ParameterService _parameterService = null!;
    private MethodDTO _methodDto = new MethodDTO();
    private MethodDTO _otherMethodDto = new MethodDTO();
    private ParameterDTO _parameterDto = new ParameterDTO();
    private Mock<IMethodSignatureRepository> _mockMethodSignatureRepository = null!;

    [TestInitialize]
    public void Initialize()
    {
        _methodDto = new MethodDTO()
        {
            Name = "FirstMethodDto",
            Accessibility = Accessibility.Private,
            Stereotype = null,
            ReturnType = new TypeDTO { Name = "TestClass", Kind = "class" }
        };
        _otherMethodDto = new MethodDTO()
        {
            Name = "SecondMethodDto",
            Accessibility = Accessibility.Public,
            Stereotype = KeyWord.Abstract
        };

        _parameterDto = new ParameterDTO
        {
            Name = "TestParameter",
            ReferenceType = new TypeDTO
            {
                Name = "TestClass",
                Kind = "class"
            }
        };
        _mockMethodRepository = new Mock<IMethodRepository>(MockBehavior.Strict);
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);
        _mockParameterRepository = new Mock<IParameterRepository>(MockBehavior.Strict);
        _mockLocalVariableRepository = new Mock<ILocalVariableRepository>(MockBehavior.Strict);
        _mockInvokedMethodRepository = new Mock<IInvokedMethodRepository>(MockBehavior.Strict);
        _mockMethodRepository
            .Setup(repo => repo.Add(It.IsAny<Method>()));
        _mockMethodRepository
            .Setup(repo => repo.FindAll())
            .Returns([]);
        _mockMethodSignatureRepository = new Mock<IMethodSignatureRepository>();
        _parameterService = new ParameterService(
            _mockParameterRepository.Object,
            _mockClassRepository.Object,
            _mockInvokedMethodRepository.Object);
    }

    [TestMethod]
    public void CreateParameter_ShouldAddParameter_WhenParameterIsValid()
    {
        var classT = new Class
        {
            Name = "TestClass",
        };

        var parameterDto = new Parameter
        {
            Name = "TestParameter",
            ReferenceType = classT,
            InstanceType = classT
        };

        _mockParameterRepository
            .Setup(r => r.Add(It.Is<Parameter>(p =>
                p.Name == parameterDto.Name && p.ReferenceType.Name == classT.Name && p.InstanceType.Name == classT.Name)));

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = "TestClass" }]);

        var result = _parameterService.CreateParameter(parameterDto);

        Assert.AreNotEqual(Guid.Empty, result);
        _mockParameterRepository.Verify(r => r.Add(It.IsAny<Parameter>()), Times.Once);
    }

    [TestMethod]
    public void CreateParameter_ShouldThrowException_WhenParameterIsNotValid()
    {
        var exception = Assert.ThrowsException<ParameterNotFoundException>(() => _parameterService.CreateParameter(null!));

        Assert.AreEqual("Parameter cannot be null.", exception.Message);
    }

    [TestMethod]
    public void SetInvokeMethodToParameter_ShouldAssociateInvokedMethodToParameter()
    {
        var parameterId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var parameter = new Parameter { Id = parameterId, Name = "param" };

        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            MethodId = Guid.NewGuid(),
            ReferenceName = "ref"
        };

        _mockParameterRepository.Setup(repo => repo.Find(parameterId)).Returns(parameter);
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(invokedMethod);
        _mockParameterRepository.Setup(repo => repo.Update(parameter)).Verifiable();

        _parameterService.SetInvokeMethodToParameter(parameterId, invokedMethodId);

        Assert.IsTrue(parameter.InvokedMethods.Contains(invokedMethod));
        _mockParameterRepository.Verify(repo => repo.Update(parameter), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(InvokedMethodNotFoundException))]
    public void SetInvokeMethodToParameter_ShouldThrow_WhenInvokedMethodNotFound()
    {
        var parameterId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var parameter = new Parameter { Id = parameterId, Name = "param" };

        _mockParameterRepository.Setup(repo => repo.Find(parameterId)).Returns(parameter);
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns((InvokedMethod?)null);

        _parameterService.SetInvokeMethodToParameter(parameterId, invokedMethodId);
    }

    [TestMethod]
    public void SetInvokeMethodToParameter_ShouldAssociateInvokedMethod_WhenBothExist()
    {
        var parameterId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var parameter = new Parameter { Id = parameterId };
        var invokedMethod = new InvokedMethod { Id = invokedMethodId };

        _mockParameterRepository
            .Setup(repo => repo.Find(parameterId))
            .Returns(parameter);

        _mockInvokedMethodRepository
            .Setup(repo => repo.Find(invokedMethodId))
            .Returns(invokedMethod);

        _mockParameterRepository
            .Setup(repo => repo.Update(It.Is<Parameter>(p => p == parameter && p.InvokedMethods.Contains(invokedMethod)))).Verifiable();

        _parameterService.SetInvokeMethodToParameter(parameterId, invokedMethodId);

        Assert.IsTrue(parameter.InvokedMethods.Contains(invokedMethod));
        _mockParameterRepository.Verify(repo => repo.Update(parameter), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ParameterNotFoundException))]
    public void SetInvokeMethodToParameter_ShouldThrow_WhenParameterDoesNotExist()
    {
        var parameterId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        _mockParameterRepository
            .Setup(repo => repo.Find(parameterId))
            .Returns((Parameter?)null);

        _mockInvokedMethodRepository
            .Setup(repo => repo.Find(It.IsAny<Guid>()))
            .Returns(new InvokedMethod());

        _parameterService.SetInvokeMethodToParameter(parameterId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(ParameterNotFoundException))]
    public void CreateParameter_ShouldThrowException_WhenParameterIsNull()
    {
        _parameterService.CreateParameter(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(InvokedMethodNotFoundException))]
    public void SetInvokeMethodToParameter_ShouldThrowException_WhenInvokedMethodNotFound()
    {
        var dummyParameter = new Parameter { InvokedMethods = [] };
        _mockParameterRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns(dummyParameter);
        _mockInvokedMethodRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns((InvokedMethod)null!);

        _parameterService.SetInvokeMethodToParameter(Guid.NewGuid(), Guid.NewGuid());
    }

    [TestMethod]
    public void GetAllParameters_ReturnsFilteredAndMappedParameters()
    {
        var allowedParameter = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "ValidParam",
            ReferenceType = new Class { Name = "ClassA" },
            InstanceType = new Class { Name = "ClassB" }
        };

        var excludedParameter = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "Obj",
            ReferenceType = new Primitive { Name = "int", PrimitiveType = PrimitiveType.Int },
            InstanceType = new Primitive { Name = "int", PrimitiveType = PrimitiveType.Int }
        };

        _mockParameterRepository.Setup(repo => repo.FindAll())
            .Returns([allowedParameter, excludedParameter]);

        var result = _parameterService.GetAllParameters();

        Assert.AreEqual(1, result.Count);
        var param = result.First();
        Assert.AreEqual(allowedParameter.Id, param.Id);
        Assert.AreEqual("ValidParam", param.Name);
        Assert.AreEqual("ClassA", param.ReferenceType.Name);
        Assert.AreEqual("ClassB", param.InstanceType.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidAttributeException))]
    public void CreateParameter_ShouldThrowException_WhenClassIsNotSubclassOfReference()
    {
        var parent = new Class { Name = "Parent" };
        var child = new Class { Name = "Child", Parents = [] };

        var parameter = new Parameter
        {
            Name = "Param",
            ReferenceType = new Class { Name = "Parent" },
            InstanceType = new Class { Name = "Child" }
        };

        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([parent, child]);

        _parameterService.CreateParameter(parameter);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidAttributeException))]
    public void CreateParameter_ShouldThrowException_WhenInterfaceReferenceIsNotImplementedByInstance()
    {
        var iface = new Interface { Name = "IMyInterface" };
        var cls = new Class { Name = "MyClass", Parents = [] };

        var parameter = new Parameter
        {
            Name = "Param",
            ReferenceType = new Interface { Name = "IMyInterface" },
            InstanceType = new Class { Name = "MyClass" }
        };

        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([iface, cls]);

        _parameterService.CreateParameter(parameter);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidAttributeException))]
    public void CreateParameter_ShouldThrowException_WhenInstanceIsInterfaceButReferenceIsClass()
    {
        var referenceClass = new Class { Name = "MyClass" };
        var instanceInterface = new Interface { Name = "IMyInterface" };

        var parameter = new Parameter
        {
            Name = "Param",
            ReferenceType = new Class { Name = "MyClass" },
            InstanceType = new Class { Name = "IMyInterface" }
        };

        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([referenceClass, instanceInterface]);

        _parameterService.CreateParameter(parameter);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidAttributeException))]
    public void CreateParameter_ShouldThrowException_WhenBothAreDifferentInterfaces()
    {
        var i1 = new Interface { Name = "IA" };
        var i2 = new Interface { Name = "IB" };

        var parameter = new Parameter
        {
            Name = "Param",
            ReferenceType = new Interface { Name = "IA" },
            InstanceType = new Interface { Name = "IB" }
        };

        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([i1, i2]);

        _parameterService.CreateParameter(parameter);
    }

    [TestMethod]
    public void CaseReferenceInterfaceRecursive_ReturnsFalse_WhenParentsIsNull()
    {
        var service = new ParameterService(null!, null!, null!);
        var reference = new Interface { Name = "IRef" };
        var instance = new Class { Name = "C1", Parents = null };

        var result = (bool)typeof(ParameterService)
            .GetMethod("CaseReferenceInterfaceRecursive", BindingFlags.NonPublic | BindingFlags.Instance)!
            .Invoke(service, [reference, instance])!;

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CaseReferenceInterfaceRecursive_ReturnsTrue_WhenParentIsMatchingInterface()
    {
        var service = new ParameterService(null!, null!, null!);
        var reference = new Interface { Name = "IRef" };
        var instance = new Class
        {
            Name = "C1",
            Parents = [reference]
        };

        var result = (bool)typeof(ParameterService)
            .GetMethod("CaseReferenceInterfaceRecursive", BindingFlags.NonPublic | BindingFlags.Instance)!
            .Invoke(service, [reference, instance])!;

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CaseReferenceInterfaceRecursive_ReturnsTrue_WhenParentIsClassWithMatchingInterfaceInHierarchy()
    {
        var service = new ParameterService(null!, null!, null!);
        var reference = new Interface { Name = "IRef" };
        var parentClass = new Class
        {
            Name = "Parent",
            Parents = [reference]
        };
        var instance = new Class
        {
            Name = "C1",
            Parents = [parentClass]
        };

        var result = (bool)typeof(ParameterService)
            .GetMethod("CaseReferenceInterfaceRecursive", BindingFlags.NonPublic | BindingFlags.Instance)!
            .Invoke(service, [reference, instance])!;

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CaseReferenceInterfaceRecursive_ReturnsFalse_WhenNoParentMatches()
    {
        var service = new ParameterService(null!, null!, null!);
        var reference = new Interface { Name = "IRef" };
        var otherInterface = new Interface { Name = "Other" };
        var parentClass = new Class
        {
            Name = "Parent",
            Parents = [otherInterface]
        };
        var instance = new Class
        {
            Name = "C1",
            Parents = [parentClass]
        };

        var result = (bool)typeof(ParameterService)
            .GetMethod("CaseReferenceInterfaceRecursive", BindingFlags.NonPublic | BindingFlags.Instance)!
            .Invoke(service, [reference, instance])!;

        Assert.IsFalse(result);
    }

    private static bool InvokeCaseReferenceClassRecursive(Class reference, Class instance)
    {
        var service = new ParameterService(null!, null!, null!);
        return (bool)typeof(ParameterService)
            .GetMethod("CaseReferenceClassRecursive", BindingFlags.NonPublic | BindingFlags.Instance)!
            .Invoke(service, new object[] { reference, instance })!;
    }

    [TestMethod]
    public void ReturnsTrue_WhenReferenceEqualsInstance()
    {
        var c = new Class { Name = "A" };
        Assert.IsTrue(InvokeCaseReferenceClassRecursive(c, c));
    }

    [TestMethod]
    public void ReturnsFalse_WhenInstanceParentsIsNull()
    {
        var reference = new Class { Name = "A" };
        var instance = new Class { Name = "B", Parents = null };
        Assert.IsFalse(InvokeCaseReferenceClassRecursive(reference, instance));
    }

    [TestMethod]
    public void ReturnsFalse_WhenNoParentMatchesReference()
    {
        var reference = new Class { Name = "A" };
        var parent = new Class { Name = "C" };
        var instance = new Class { Name = "B", Parents = [parent] };
        Assert.IsFalse(InvokeCaseReferenceClassRecursive(reference, instance));
    }

    [TestMethod]
    public void ReturnsTrue_WhenParentMatchesReference()
    {
        var reference = new Class { Name = "A" };
        var instance = new Class { Name = "B", Parents = [reference] };
        Assert.IsTrue(InvokeCaseReferenceClassRecursive(reference, instance));
    }

    [TestMethod]
    public void ReturnsTrue_WhenGrandparentMatchesReference()
    {
        var reference = new Class { Name = "A" };
        var parent = new Class { Name = "C", Parents = [reference] };
        var instance = new Class { Name = "B", Parents = [parent] };
        Assert.IsTrue(InvokeCaseReferenceClassRecursive(reference, instance));
    }
}
