using Moq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class LocalVariable_Test
{
    private Mock<IMethodRepository> _mockMethodRepository = null!;
    private Mock<IClassRepository> _mockClassRepository = null!;
    private Mock<IParameterRepository> _mockParameterRepository = null!;
    private Mock<ILocalVariableRepository> _mockLocalVariableRepository = null!;
    private Mock<IInvokedMethodRepository> _mockInvokedMethodRepository = null!;
    private LocalVariableService _localVariableService = null!;
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
        _localVariableService = new LocalVariableService(
            _mockLocalVariableRepository.Object,
            _mockClassRepository.Object,
            _mockInvokedMethodRepository.Object);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldThrowException_WhenLocalVariableIsNotValid()
    {
        var exception =
            Assert.ThrowsException<LocalVariableNotFoundException>(() => _localVariableService.CreateLocalVariable(null));

        Assert.AreEqual("Local Variable cannot be null.", exception.Message);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldAddLocalVariable_WhenLocalVariableIsValid()
    {
        var classT = new Class { Name = "TestClass" };
        var classChild = new Class { Name = "Child", Parents = [classT] };

        var localVariableDto = new LocalVariableDTO { Name = "TestParameter", ReferenceType = new TypeDTO { Name = classT.Name, Kind = "class" }, InstanceType = new TypeDTO { Name = classChild.Name, Kind = "class" } };

        _mockLocalVariableRepository
            .Setup(r => r.Add(It.Is<LocalVariable>(p =>
                p.Name == localVariableDto.Name && p.ReferenceType.Name == classT.Name)));

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([classT, classChild]);

        var result = _localVariableService.CreateLocalVariable(localVariableDto);

        Assert.AreNotEqual(Guid.Empty, result);
        _mockLocalVariableRepository.Verify(r => r.Add(It.IsAny<LocalVariable>()), Times.Once);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldThrowException_WhenInstanceTypeNotSubclassOfReferenceType()
    {
        var classT = new Class { Name = "TestClass" };
        var classChild = new Class { Name = "Child" };

        var localVariableDto = new LocalVariableDTO { Name = "TestParameter", ReferenceType = new TypeDTO { Name = classT.Name, Kind = "class" }, InstanceType = new TypeDTO { Name = classChild.Name, Kind = "class" } };

        _mockLocalVariableRepository
            .Setup(r => r.Add(It.Is<LocalVariable>(p =>
                p.Name == localVariableDto.Name && p.ReferenceType.Name == classT.Name && p.InstanceType.Name == classChild.Name)));

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([classT, classChild]);

        var ex = Assert.ThrowsException<LocalVariableException>(() =>
        {
            _localVariableService.CreateLocalVariable(localVariableDto);
        });

        Assert.AreEqual("Instance Type is not a subclass of Reference Type", ex.Message);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldThrowException_WhenInstanceClassIsNotSubclassOfReferenceClass()
    {
        var classT = new Class { Name = "BaseClass" };
        var unrelatedClass = new Class { Name = "UnrelatedClass" };

        var dto = new LocalVariableDTO
        {
            Name = "var1",
            ReferenceType = new TypeDTO { Name = classT.Name, Kind = "class" },
            InstanceType = new TypeDTO { Name = unrelatedClass.Name, Kind = "class" }
        };

        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([classT, unrelatedClass]);

        var ex = Assert.ThrowsException<LocalVariableException>(() =>
            _localVariableService.CreateLocalVariable(dto));

        Assert.AreEqual("Instance Type is not a subclass of Reference Type", ex.Message);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldThrowException_WhenInstanceTypeIsInterfaceAndReferenceIsClass()
    {
        var classT = new Class { Name = "BaseClass" };
        var interfaceT = new Interface { Name = "MyInterface" };

        var dto = new LocalVariableDTO
        {
            Name = "var2",
            ReferenceType = new TypeDTO { Name = classT.Name, Kind = "class" },
            InstanceType = new TypeDTO { Name = interfaceT.Name, Kind = "interface" }
        };

        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([classT, interfaceT]);

        var ex = Assert.ThrowsException<LocalVariableException>(() =>
            _localVariableService.CreateLocalVariable(dto));

        Assert.AreEqual("Instance Type is not a subclass of Reference Type", ex.Message);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldThrowException_WhenBothAreInterfacesAndDifferent()
    {
        var interface1 = new Interface { Name = "Interface1" };
        var interface2 = new Interface { Name = "Interface2" };

        var dto = new LocalVariableDTO
        {
            Name = "var3",
            ReferenceType = new TypeDTO { Name = interface1.Name, Kind = "interface" },
            InstanceType = new TypeDTO { Name = interface2.Name, Kind = "interface" }
        };

        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([interface1, interface2]);

        var ex = Assert.ThrowsException<LocalVariableException>(() =>
            _localVariableService.CreateLocalVariable(dto));

        Assert.AreEqual("Instance Type is not a subclass of Reference Type", ex.Message);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldThrowException_WhenClassDoesNotImplementInterface()
    {
        var interface1 = new Interface { Name = "IMyInterface" };
        var class1 = new Class { Name = "MyClass", Parents = [] }; // No implementa la interfaz

        var dto = new LocalVariableDTO
        {
            Name = "var1",
            ReferenceType = new TypeDTO { Name = interface1.Name, Kind = "interface" },
            InstanceType = new TypeDTO { Name = class1.Name, Kind = "class" }
        };

        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([interface1, class1]);

        var ex = Assert.ThrowsException<LocalVariableException>(() =>
            _localVariableService.CreateLocalVariable(dto));

        Assert.AreEqual("Instance Type is not a subclass of Reference Type", ex.Message);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldThrowException_WhenInstanceTypeNotSubclassOfReferenceType2()
    {
        var class3 = new Class { Name = "Class3" };
        var classT = new Class { Name = "TestClass" };
        var classChild = new Class { Name = "Child", Parents = [class3] };

        var localVariableDto = new LocalVariableDTO { Name = "TestParameter", ReferenceType = new TypeDTO { Name = classT.Name, Kind = "class" }, InstanceType = new TypeDTO { Name = classChild.Name, Kind = "class" } };

        _mockLocalVariableRepository
            .Setup(r => r.Add(It.Is<LocalVariable>(p =>
                p.Name == localVariableDto.Name && p.ReferenceType.Name == classT.Name && p.InstanceType.Name == classChild.Name)));

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([classT, classChild]);

        var ex = Assert.ThrowsException<LocalVariableException>(() =>
        {
            _localVariableService.CreateLocalVariable(localVariableDto);
        });

        Assert.AreEqual("Instance Type is not a subclass of Reference Type", ex.Message);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldThrowException_WhenInstanceTypeIsPrimitive()
    {
        var classT = new Class { Name = "TestClass" };
        var primitive1 = new Primitive(PrimitiveType.Bool) { Name = "Prim" };

        var localVariableDto = new LocalVariableDTO { Name = "TestParameter", ReferenceType = new TypeDTO { Name = classT.Name, Kind = "class" }, InstanceType = new TypeDTO { Name = primitive1.Name, Kind = "class" } };

        _mockLocalVariableRepository
            .Setup(r => r.Add(It.Is<LocalVariable>(p =>
                p.Name == localVariableDto.Name && p.ReferenceType.Name == classT.Name)));

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([classT, primitive1]);

        var ex = Assert.ThrowsException<LocalVariableException>(() =>
        {
            _localVariableService.CreateLocalVariable(localVariableDto);
        });

        Assert.AreEqual("Invalid reference type or instance type. If one is primitive, both should be primitive. If both are, then they must be the same.", ex.Message);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldCreate_WhenReferenceIsInterfaceAndInstanceIsSubclass()
    {
        var interface1 = new Interface { Name = "Inter1" };
        var class1 = new Class { Name = "Class1", Parents = [interface1] };

        var localVariableDto = new LocalVariableDTO { Name = "TestParameter", ReferenceType = new TypeDTO { Name = interface1.Name, Kind = "interface" }, InstanceType = new TypeDTO { Name = class1.Name, Kind = "class" } };

        _mockLocalVariableRepository
            .Setup(r => r.Add(It.Is<LocalVariable>(p =>
                p.Name == localVariableDto.Name && p.ReferenceType.Name == interface1.Name && p.InstanceType.Name == class1.Name)));

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([interface1, class1]);

        var result = _localVariableService.CreateLocalVariable(localVariableDto);

        Assert.AreNotEqual(Guid.Empty, result);
        _mockLocalVariableRepository.Verify(r => r.Add(It.IsAny<LocalVariable>()), Times.Once);
    }

    [DataTestMethod]
    [DataRow("base")]
    [DataRow("this")]
    [DataRow("Object")]
    public void CreateLocalVariable_ShouldThrowException_WhenNameIsReservedKeyword(string reservedName)
    {
        var classT = new Class { Name = "TestClass" };
        var classChild = new Class { Name = "Child", Parents = [classT] };

        var dto = new LocalVariableDTO { Name = reservedName, ReferenceType = new TypeDTO { Name = classT.Name, Kind = "class" }, InstanceType = new TypeDTO { Name = classChild.Name, Kind = "class" } };

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([classChild, classT]);

        var ex = Assert.ThrowsException<LocalVariableException>(() =>
        {
            _localVariableService.CreateLocalVariable(dto);
        });

        Assert.AreEqual("The name is a reserved keyword and is not allowed.", ex.Message);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldThrowException_WhenNameIsEmpty()
    {
        var classT = new TypeDTO { Name = "TestClass", Kind = "class" };

        var localVariableDto = new LocalVariableDTO { Name = string.Empty, ReferenceType = classT };

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = "TestClass" }]);

        var exception = Assert.ThrowsException<LocalVariableNotFoundException>(() =>
            _localVariableService.CreateLocalVariable(localVariableDto));

        Assert.AreEqual("Local Variable Name cannot be empty.", exception.Message);
    }

    [TestMethod]
    public void SetInvokeMethodToALocalVariable_ShouldAssociateInvokedMethodToLocalVariable()
    {
        var localVariableId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var localVariable = new LocalVariable { Id = localVariableId, Name = "myVar" };

        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            MethodId = Guid.NewGuid(),
            ReferenceName = "ref"
        };

        _mockLocalVariableRepository.Setup(repo => repo.Find(localVariableId)).Returns(localVariable);
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(invokedMethod);
        _mockLocalVariableRepository.Setup(repo => repo.Update(localVariable)).Verifiable();

        _localVariableService.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId);

        Assert.IsTrue(localVariable.InvokedMethods.Contains(invokedMethod));
        _mockLocalVariableRepository.Verify(repo => repo.Update(localVariable), Times.Once);
    }

    [TestMethod]
    public void SetInvokeMethodToALocalVariable_ShouldThrowException_WhenTypeIsNotPrimitiveorClass()
    {
        var localVariableId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var localVariable = new LocalVariable
        {
            Id = localVariableId,
            Name = "myVar",
            ReferenceType = new Interface()
        };

        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            MethodId = Guid.NewGuid(),
            ReferenceName = "ref"
        };

        _mockLocalVariableRepository.Setup(repo => repo.Find(localVariableId)).Returns(localVariable);
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(invokedMethod);
        _mockLocalVariableRepository.Setup(repo => repo.Update(localVariable)).Verifiable();

        var exception = Assert.ThrowsException<InvalidAttributeException>(() => _localVariableService.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId));

        Assert.AreEqual("In v2 of this program only Class and Primitive types are accepted.", exception.Message);
    }

    [TestMethod]
    [ExpectedException(typeof(InvokedMethodNotFoundException))]
    public void SetInvokeMethodToLocalVariable_ShouldThrowException_WhenInvokedMethodNotFound()
    {
        var localVariableId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var localVariable = new LocalVariable { Id = localVariableId, Name = "myVar" };

        _mockLocalVariableRepository.Setup(repo => repo.Find(localVariableId)).Returns(localVariable);
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns((InvokedMethod?)null);

        _localVariableService.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId);
    }

    [TestMethod]
    public void SetInvokeMethodToLocalVariable_ShouldThrowException_WhenLocalVariableNotFound()
    {
        var localVariableId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        _mockLocalVariableRepository.Setup(repo => repo.Find(localVariableId)).Returns((LocalVariable?)null);
        _mockInvokedMethodRepository.Setup(repo => repo.Find(It.IsAny<Guid>())).Returns((InvokedMethod?)null);
        var ex = Assert.ThrowsException<LocalVariableNotFoundException>(() =>
            _localVariableService.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId));

        Assert.AreEqual("Local Variable not found.", ex.Message);
    }
}
