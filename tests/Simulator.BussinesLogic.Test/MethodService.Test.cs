using Moq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class MethodServiceTest
{
    private Mock<IMethodRepository> _mockMethodRepository = null!;
    private Mock<IClassRepository> _mockClassRepository = null!;
    private Mock<IParameterRepository> _mockParameterRepository = null!;
    private Mock<ILocalVariableRepository> _mockLocalVariableRepository = null!;
    private Mock<IInvokedMethodRepository> _mockInvokedMethodRepository = null!;
    private MethodService _methodService = null!;
    private MethodDTO _methodDto = new MethodDTO();
    private MethodDTO _otherMethodDto = new MethodDTO();
    private ParameterDTO _parameterDto = new ParameterDTO();

    [TestInitialize]
    public void Initialize()
    {
        _methodDto = new MethodDTO()
        {
            Name = "FirstMethodDto",
            Accessibility = Accessibility.Private,
            Stereotype = null,
            ReturnType = new TypeDTO { Name = "TestClass", }
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
            ParameterType = new TypeDTO
            {
                Name = "TestClass",
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
        _methodService = new MethodService(_mockMethodRepository.Object, _mockClassRepository.Object,
            _mockParameterRepository.Object, _mockLocalVariableRepository.Object, _mockInvokedMethodRepository.Object);
    }

    [TestMethod]
    public void CreateMethod_ShouldCallRepositoryAdd_WithValidMethod()
    {
        var classes = new List<Type>();
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns(classes);

        if(_methodDto.ReturnType != null)
        {
            classes.Add(new Class { Name = _methodDto.ReturnType.Name });
        }

        _methodService.CreateMethod(_methodDto);

        _mockMethodRepository.Verify(repo => repo.Add(
            It.Is<Method>(m =>
                m.Name.Equals(_methodDto.Name) &&
                m.Accessibility == _methodDto.Accessibility &&
                m.Stereotype == null)), Times.Once);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowException_WhenReturnTypeIsNull()
    {
        var classT = new TypeDTO { Name = "TestClass" };
        var method = new MethodDTO
        {
            Name = "TestMethod",
            Accessibility = Accessibility.Public,
            Stereotype = null,
            ReturnType = classT
        };
        var classes = new List<Type>();
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns(classes);

        var exception = Assert.ThrowsException<TypeNotFoundException>(() => _methodService.CreateMethod(method));
        Assert.AreEqual("Method return type not found.", exception.Message);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowException_WhenKeyAccessibilityIsNotValid()
    {
        var invalidAccessibility = (Accessibility)100;
        var classT = new TypeDTO { Name = "TestClass" };
        var method = new MethodDTO
        {
            Name = "TestMethod",
            Accessibility = invalidAccessibility,
            Stereotype = null,
            ReturnType = classT
        };
        var classes = new List<Type>();
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns(classes);

        var exception = Assert.ThrowsException<InvalidMethodException>(() => _methodService.CreateMethod(method));
        Assert.AreEqual("Invalid accessibility value.", exception.Message);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowExcepton_WhenKeyWordIsNotValid()
    {
        var invalidStereotype = (KeyWord)100;
        var classT = new TypeDTO { Name = "TestClass" };
        var method = new MethodDTO
        {
            Name = "TestMethod",
            Accessibility = Accessibility.Public,
            Stereotype = invalidStereotype,
            ReturnType = classT
        };
        var classes = new List<Type>();
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns(classes);

        var exception = Assert.ThrowsException<InvalidMethodException>(() => _methodService.CreateMethod(method));
        Assert.AreEqual("Invalid stereotype value.", exception.Message);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowException_WhenMethodIsNull()
    {
        MethodDTO nullMethodDto = null!;

        var exception =
            Assert.ThrowsException<MethodNotFoundException>(() => _methodService.CreateMethod(nullMethodDto));
        Assert.AreEqual("Method cannot be null.", exception.Message);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowException_WhenMethodNameAlreadyExists()
    {
        var existingMethod = new Method
        {
            Id = Guid.NewGuid(),
            Name = _methodDto.Name,
            Accessibility = Accessibility.Private,
            Stereotype = null,
            ReturnType = new Simulator.Domain.Type { Name = _methodDto.ReturnType.Name }
        };

        _mockMethodRepository
            .Setup(repo => repo.FindAll())
            .Returns([existingMethod]);

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = _methodDto.ReturnType.Name }]);

        var exception = Assert.ThrowsException<InvalidMethodException>(() => _methodService.CreateMethod(_methodDto));
        Assert.AreEqual("Method with the same name  already exists.", exception.Message);
    }

    [TestMethod]
    public void AssociateMethodWithClass_ShouldThrowException_WhenMethodNotFound()
    {
        _mockMethodRepository.Setup(repo => repo.Find(It.IsAny<Guid>())).Returns((Method)null!);

        var exception = Assert.ThrowsException<MethodNotFoundException>(() =>
            _methodService.AssociateMethodWithClass(Guid.NewGuid(), Guid.NewGuid()));
        Assert.AreEqual("Method null.", exception.Message);
    }

    [TestMethod]
    public void AssociateMethodWithClass_ShouldThrowException_WhenClassNotFound()
    {
        _mockMethodRepository.Setup(repo => repo.Find(It.IsAny<Guid>())).Returns(new Method());
        _mockClassRepository.Setup(repo => repo.Find(It.IsAny<Guid>())).Returns((Class)null!);

        var exception = Assert.ThrowsException<ClassNotFoundException>(() =>
            _methodService.AssociateMethodWithClass(Guid.NewGuid(), Guid.NewGuid()));
        Assert.AreEqual("Class not found.", exception.Message);
    }

    [TestMethod]
    public void AssociateMethodWithClass_ShouldAddMethodToClass_WhenValidInputsProvided()
    {
        var method = new Method { Id = Guid.NewGuid(), Name = "TestMethod" };
        var classT = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [] };

        _mockMethodRepository.Setup(repo => repo.Find(method.Id)).Returns(method);
        _mockClassRepository.Setup(repo => repo.Find(classT.Id)).Returns(classT);
        _mockClassRepository.Setup(repo => repo.Update(It.IsAny<Class>()));

        _methodService.AssociateMethodWithClass(method.Id, classT.Id);

        Assert.IsTrue(classT.Methods.Contains(method));
        _mockClassRepository.Verify(repo => repo.Update(classT), Times.Once);
    }

    [TestMethod]
    public void ValidateClassUniqueNameMethod_ShouldThrowException_WhenMethodNameAlreadyExists()
    {
        var method = new Method { Id = Guid.NewGuid(), Name = "TestMethod" };
        var existingMethod = new Method { Id = Guid.NewGuid(), Name = "TestMethod" };
        var classT = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [existingMethod] };

        _mockMethodRepository.Setup(repo => repo.Find(method.Id)).Returns(method);
        _mockClassRepository.Setup(repo => repo.Find(classT.Id)).Returns(classT);

        var exception = Assert.ThrowsException<InvalidMethodException>(() =>
            _methodService.AssociateMethodWithClass(method.Id, classT.Id));
        Assert.AreEqual("A method with the same name already exists in the class.", exception.Message);
    }

    [TestMethod]
    public void DeleteMethod_ShouldDeleteMethod_WhenValid()
    {
        var methodId = Guid.NewGuid();
        var method = new Method { Id = methodId, Name = "TestMethod", Methods = [] };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(repo => repo.Delete(It.IsAny<Guid>()));
        _mockInvokedMethodRepository.Setup(repo => repo.FindAll()).Returns([]);

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([]);

        _methodService.DeleteMethod(methodId);

        _mockMethodRepository.Verify(repo => repo.Delete(methodId), Times.Once);
    }

    [TestMethod]
    public void DeleteMethod_ShouldThrowException_WhenMethodIsAssociatedWithInvokedMethod()
    {
        var methodId = Guid.NewGuid();
        var method = new Method { Id = methodId, Name = "TestMethod" };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([]);

        var invokedMethods = new List<InvokedMethod> { new InvokedMethod { Id = Guid.NewGuid(), MethodId = methodId } };

        _mockInvokedMethodRepository.Setup(repo => repo.FindAll()).Returns(invokedMethods);

        var exception = Assert.ThrowsException<InvalidMethodException>(() => _methodService.DeleteMethod(methodId));

        Assert.AreEqual("Method is associated with invokedMethod.", exception.Message);
        _mockMethodRepository.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Never);
    }

    [TestMethod]
    public void AddLocalVariableToMethod_ShouldAddLocalVariable_WhenValidInputsProvided()
    {
        var methodId = Guid.NewGuid();
        var localVariableId = Guid.NewGuid();
        var localVariable = new LocalVariable { Id = localVariableId, Name = "TestLocalVariable" };
        var method = new Method { Id = methodId, Name = "TestMethod", LocalVariables = [] };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockLocalVariableRepository.Setup(repo => repo.Find(localVariableId)).Returns(localVariable);
        _mockMethodRepository.Setup(repo => repo.Update(It.IsAny<Method>()));

        _methodService.AddLocalVariableToMethod(methodId, localVariableId);

        Assert.IsTrue(method.LocalVariables.Contains(localVariable));
        _mockMethodRepository.Verify(repo => repo.Update(method), Times.Once);
    }

    [TestMethod]
    public void AddLocalVariableToMethod_ShouldThrowException_WhenLocalVariableNameConflictsWithExistingVariable()
    {
        var methodId = Guid.NewGuid();
        var localVariableId = Guid.NewGuid();
        var localVariable = new LocalVariable
        {
            Id = localVariableId,
            Name = "TestLocalVariable"
        };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            LocalVariables = [localVariable],
            Parameters = []
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockLocalVariableRepository.Setup(repo => repo.Find(localVariableId)).Returns(localVariable);
        _mockMethodRepository.Setup(repo => repo.Update(It.IsAny<Method>()));

        var exception = Assert.ThrowsException<InvalidMethodException>(() =>
            _methodService.AddLocalVariableToMethod(methodId, localVariableId));
        Assert.AreEqual("A variable or parameter with the same name already exists.", exception.Message);
    }

    [TestMethod]
    public void AddParameterToMethod_ShouldAddParameterToMethod_WhenValidInputsProvided()
    {
        var methodId = Guid.NewGuid();
        var parameterId = Guid.NewGuid();
        var parameter = new Parameter { Id = parameterId, Name = "TestParameter" };
        var method = new Method { Id = methodId, Name = "TestMethod", Parameters = [] };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockParameterRepository.Setup(repo => repo.Find(parameterId)).Returns(parameter);
        _mockMethodRepository.Setup(repo => repo.Update(It.IsAny<Method>()));

        _methodService.AddParameterToMethod(methodId, parameterId);

        Assert.IsTrue(method.Parameters.Contains(parameter));
        _mockMethodRepository.Verify(repo => repo.Update(method), Times.Once);
    }

    [TestMethod]
    public void AddParameterToMethod_ShouldThrowException_WhenParameterNameConflictsWithExistingLocalVariable()
    {
        var methodId = Guid.NewGuid();
        var parameterId = Guid.NewGuid();
        var parameter = new Parameter { Id = parameterId, Name = "TestParameter" };
        var method = new Method { Id = methodId, Name = "TestMethod", LocalVariables = [], Parameters = [parameter] };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockParameterRepository.Setup(repo => repo.Find(parameterId)).Returns(parameter);
        _mockMethodRepository.Setup(repo => repo.Update(It.IsAny<Method>()));

        var exception = Assert.ThrowsException<InvalidMethodException>(() =>
            _methodService.AddParameterToMethod(methodId, parameterId));
        Assert.AreEqual("A variable or parameter with the same name already exists.", exception.Message);
    }

    [TestMethod]
    public void AddParameterToMethod_ShouldThrowException_WhenParameterNameConflictsWithExistingParameter()
    {
        var methodId = Guid.NewGuid();
        var parameterId = Guid.NewGuid();
        var parameter = new Parameter { Id = parameterId, Name = "TestParameter" };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            LocalVariables = [],
            Parameters =
            [
                new Parameter { Name = "TestParameter" }
            ]
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockParameterRepository.Setup(repo => repo.Find(parameterId)).Returns(parameter);

        var exception = Assert.ThrowsException<InvalidMethodException>(() =>
            _methodService.AddParameterToMethod(methodId, parameterId));
        Assert.AreEqual("A variable or parameter with the same name already exists.", exception.Message);
    }

    [TestMethod]
    public void AddParameterToMethod_ShouldAddParameter_WhenValidInputsProvided()
    {
        var methodId = Guid.NewGuid();
        var method = new Method { Id = methodId, Name = "TestMethod", LocalVariables = [], Parameters = [] };
        var parameterId = Guid.NewGuid();
        var parameter = new Parameter { Id = parameterId, Name = "TestParameter" };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockParameterRepository.Setup(repo => repo.Find(parameterId)).Returns(parameter);
        _mockMethodRepository.Setup(repo => repo.Update(It.IsAny<Method>()));

        _methodService.AddParameterToMethod(methodId, parameterId);

        Assert.IsTrue(method.Parameters.Any(p => p.Name == parameter.Name));

        _mockMethodRepository.Verify(repo => repo.Update(method), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void MethodConstructor_WithEmptyName_ShouldThrowMethodException()
    {
        var name = string.Empty;
        new Method(name, Accessibility.Public, null, [], [], [], new Primitive());
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void MethodConstructor_WithSpacesInName_ShouldThrowMethodException()
    {
        var name = "Invalid Name";
        new Method(name, Accessibility.Public, null, [], [], [], new Primitive());
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void MethodConstructor_WithNameLongerThan50Characters_ShouldThrowMethodException()
    {
        var name = new string('a', 51);
        new Method(name, Accessibility.Public, null, [], [], [], new Primitive());
    }

    [TestMethod]
    public void CreateParameter_ShouldAddParameter_WhenParameterIsValid()
    {
        var classT = new TypeDTO { Name = "TestClass" };

        var parameterDto = new ParameterDTO { Name = "TestParameter", ParameterType = classT };

        _mockParameterRepository
            .Setup(r => r.Add(It.Is<Parameter>(p =>
                p.Name == parameterDto.Name && p.ParameterType.Name == classT.Name)));

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = "TestClass" }]);

        var result = _methodService.CreateParameter(parameterDto);

        Assert.AreNotEqual(Guid.Empty, result);
        _mockParameterRepository.Verify(r => r.Add(It.IsAny<Parameter>()), Times.Once);
    }

    [TestMethod]
    public void CreateParameter_ShouldThrowException_WhenParameterIsNotValid()
    {
        var exception = Assert.ThrowsException<ParameterNotFoundException>(() => _methodService.CreateParameter(null));

        Assert.AreEqual("Parameter cannot be null.", exception.Message);
    }

    [TestMethod]
    public void CreateParameter_ShouldThrowException_WhenParameterNameIsEmpty()
    {
        var classT = new TypeDTO { Name = "TestClass" };

        var parameterDto = new ParameterDTO { Name = string.Empty, ParameterType = classT };

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = "TestClass" }]);

        var exception = Assert.ThrowsException<ParameterException>(() =>
            _methodService.CreateParameter(parameterDto));

        Assert.AreEqual("Name cannot be null or empty", exception.Message);
    }

    [DataTestMethod]
    [DataRow("base")]
    [DataRow("this")]
    [DataRow("Object")]
    public void CreateParameter_ShouldThrowException_WhenNameIsReservedKeyword(string reservedName)
    {
        var dto = new ParameterDTO { Name = reservedName, ParameterType = new TypeDTO { Name = "SomeType" }, };

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = "SomeType", Id = Guid.NewGuid() }]);

        var ex = Assert.ThrowsException<ParameterException>(() =>
        {
            _methodService.CreateParameter(dto);
        });

        Assert.AreEqual("The name is a reserved keyword and is not allowed.", ex.Message);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldThrowException_WhenLocalVaraiableIsNotValid()
    {
        var exception =
            Assert.ThrowsException<LocalVariableNotFoundException>(() => _methodService.CreateLocalVariable(null));

        Assert.AreEqual("Local Variable cannot be null.", exception.Message);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldAddLocalVariable_WhenLocalVariableIsValid()
    {
        var classT = new TypeDTO { Name = "TestClass" };

        var localVariableDto = new LocalVariableDTO { Name = "TestParameter", LocalVariableType = classT };

        _mockLocalVariableRepository
            .Setup(r => r.Add(It.Is<LocalVariable>(p =>
                p.Name == localVariableDto.Name && p.LocalVariableType.Name == classT.Name)));

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = "TestClass" }]);

        var result = _methodService.CreateLocalVariable(localVariableDto);

        Assert.AreNotEqual(Guid.Empty, result);
        _mockLocalVariableRepository.Verify(r => r.Add(It.IsAny<LocalVariable>()), Times.Once);
    }

    [DataTestMethod]
    [DataRow("base")]
    [DataRow("this")]
    [DataRow("Object")]
    public void CreateLocalVariable_ShouldThrowException_WhenNameIsReservedKeyword(string reservedName)
    {
        var dto = new LocalVariableDTO { Name = reservedName, LocalVariableType = new TypeDTO { Name = "SomeType" }, };

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = "SomeType", Id = Guid.NewGuid() }]);

        var ex = Assert.ThrowsException<LocalVariableException>(() =>
        {
            _methodService.CreateLocalVariable(dto);
        });

        Assert.AreEqual("The name is a reserved keyword and is not allowed.", ex.Message);
    }

    [TestMethod]
    public void AssociateInvokeMethod_ShouldReturnId_WhenValidInput()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodDto = new InvokedMethodDTO { MethodId = methodId, ReferenceName = "someVariable" };

        var method = new Method
        {
            Id = methodId,
            Parameters =
            [
                new Parameter { Name = "someVariable" }
            ],
            LocalVariables = [],
        };

        var classT = new Class { Methods = [method], Attributes = [] };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockInvokedMethodRepository.Setup(repo => repo.Add(It.IsAny<InvokedMethod>()));
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([classT]);

        _methodService.AssociateInvokeMethod(invokedMethodDto);

        _mockMethodRepository.Verify(repo => repo.Find(methodId), Times.Once);
        _mockInvokedMethodRepository.Verify(repo => repo.Add(It.IsAny<InvokedMethod>()), Times.Once);
    }

    [TestMethod]
    public void CreateLocalVariable_ShouldThrowException_WhenNameIsEmpty()
    {
        var classT = new TypeDTO { Name = "TestClass" };

        var localVariableDto = new LocalVariableDTO { Name = string.Empty, LocalVariableType = classT };

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = "TestClass" }]);

        var exception = Assert.ThrowsException<LocalVariableNotFoundException>(() =>
            _methodService.CreateLocalVariable(localVariableDto));

        Assert.AreEqual("Local Variable Name cannot be empty.", exception.Message);
    }

    [TestMethod]
    public void AssociateInvokeMethod_ShouldThrow_WhenDTOIsNull()
    {
        InvokedMethodDTO? invokedMethodDto = null;

        var exception = Assert.ThrowsException<InvokedMethodNotFoundException>(() =>
            _methodService.AssociateInvokeMethod(invokedMethodDto!));

        Assert.AreEqual("Invoked Method cannot be null.", exception.Message);
    }

    [TestMethod]
    public void AssociateInvokeMethod_ShouldThrow_WhenDTOReferenceNameISEmpty()
    {
        var invoked = new InvokedMethodDTO { MethodId = Guid.NewGuid(), ReferenceName = string.Empty };

        _mockMethodRepository
            .Setup(r => r.Find(invoked.MethodId))
            .Returns(new Method());

        var exception = Assert.ThrowsException<InvokedMethodNotFoundException>(() =>
            _methodService.AssociateInvokeMethod(invoked!));

        Assert.AreEqual("Invoked Method Reference Name cannot be empty.", exception.Message);
    }

    [TestMethod]
    public void AssociateInvokeMethod_ShouldThrow_WhenReferenceNameIsNullOrEmpty()
    {
        var invokedMethodDto = new InvokedMethodDTO { MethodId = Guid.NewGuid(), ReferenceName = null };

        _mockMethodRepository.Setup(repo => repo.Find(It.IsAny<Guid>()))
            .Returns(new Method());

        var exception = Assert.ThrowsException<InvokedMethodNotFoundException>(() =>
            _methodService.AssociateInvokeMethod(invokedMethodDto));

        Assert.AreEqual("Invoked Method Reference Name cannot be null or empty.", exception.Message);
    }

    [TestMethod]
    public void AssociateInvokeMethod_ShouldThrow_WhenMethodNotFound()
    {
        var invokedMethodDto = new InvokedMethodDTO { MethodId = Guid.NewGuid(), ReferenceName = "validReference" };

        _mockMethodRepository.Setup(repo => repo.Find(invokedMethodDto.MethodId))
            .Returns((Method)null!);

        var exception = Assert.ThrowsException<MethodNotFoundException>(() =>
            _methodService.AssociateInvokeMethod(invokedMethodDto));

        Assert.AreEqual("Method null.", exception.Message);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodNotFoundException))]
    public void AddAMethod_ShouldThrow_WhenMethodIsNull()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns((Method?)null);
        _mockMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(new Method());
        _mockInvokedMethodRepository.Setup(repo => repo.Find(It.IsAny<Guid>())).Returns(new InvokedMethod());

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(InvokedMethodNotFoundException))]
    public void AddAMethod_ShouldThrow_WhenInvokedMethodIsNull()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var method = new Method
        {
            Id = methodId,
            Name = "MyMethod",
            Parameters =
            [
                new Parameter { Name = "SomeName" }
            ],
            Methods = []
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(new Method());
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns((InvokedMethod?)null);

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldThrow_WhenReferenceNameIsInvalid()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var invokedMethod = new InvokedMethod { Id = invokedMethodId, ReferenceName = "InvalidReference" };
        var method = new Method
        {
            Id = methodId,
            Name = "MyMethod",
            Parameters = [],
            LocalVariables = [],
            Methods = []
        };
        var classWithMethod = new Class { Methods = [method] };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(new Method());
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([classWithMethod]);

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldAddInvokedMethod_WhenValid()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var invokedMethod = new InvokedMethod { Id = invokedMethodId, ReferenceName = "TestReference" };
        var method = new Method
        {
            Id = methodId,
            Name = "MyMethod",
            Parameters = [ new Parameter { Name = "TestReference" } ],
            LocalVariables = [],
            Methods = []
        };

        var invokedMethodAsMethod = new Method
        {
            Id = invokedMethodId,
            Name = "InvokedAsMethod",
            Parameters = [],
            LocalVariables = [],
            Methods = []
        };

        var classWithMethod = new Class { Methods = [method] };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(invokedMethod);
        _mockMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(invokedMethodAsMethod);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([classWithMethod]);
        _mockMethodRepository.Setup(repo => repo.Update(It.IsAny<Method>()));

        _methodService.AddAMethod(methodId, invokedMethodId);

        _mockMethodRepository.Verify(repo => repo.Update(It.Is<Method>(m => m.Methods.Contains(invokedMethod))), Times.Once);
    }

    [TestMethod]
    public void AddAMethod_ShouldSucceed_WhenReferenceNameIsThis()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Parameters = [],
            LocalVariables = [],
            Methods = []
        };

        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            ReferenceName = "this"
        };

        var classT = new Class
        {
            Methods = [new Method { Id = methodId, Name = "TestMethod" }],
            Attributes = []
        };

        _mockMethodRepository
            .Setup(r => r.Find(It.IsAny<Guid>()))
            .Returns<Guid>(id =>
            {
                if (id == methodId)
                {
                    return method;
                }

                if (id == invokedMethodId)
                {
                    return method;
                }

                return null!;
            });

        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT]);
        _mockMethodRepository.Setup(r => r.Update(It.IsAny<Method>()));

        _methodService.AddAMethod(methodId, invokedMethodId);

        _mockMethodRepository.Verify(
            r => r.Update(It.Is<Method>(m => m.Methods.Contains(invokedMethod))),
            Times.Once);
    }

    [TestMethod]
    public void AddAMethod_ShouldSucceed_WhenReferenceNameIsBase()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var methodName = "BaseMethod";

        var method = new Method
        {
            Id = methodId,
            Name = methodName,
            Parameters = [],
            LocalVariables = [],
            Methods = []
        };

        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            ReferenceName = "base"
        };

        var hierarchyClass = new Class
        {
            Id = Guid.NewGuid(),
            Methods = [new Method { Id = Guid.NewGuid(), Name = methodName }],
            Attributes = []
        };

        var classWithMethod = new Class
        {
            Id = Guid.NewGuid(),
            Methods = [method],
            HierarchyClass = hierarchyClass,
            Attributes = []
        };

        _mockMethodRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns<Guid>(id =>
        {
            if (id == methodId || id == invokedMethodId)
            {
                return method;
            }

            return null!;
        });

        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classWithMethod]);
        _mockMethodRepository.Setup(r => r.Update(It.IsAny<Method>()));

        _methodService.AddAMethod(methodId, invokedMethodId);

        _mockMethodRepository.Verify(
            r => r.Update(It.Is<Method>(m => m.Methods.Contains(invokedMethod))),
            Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldThrow_WhenReferenceNotFoundAnywhere()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var invokedReferenceName = "invalidRef";

        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Parameters = [],
            LocalVariables = [],
            Methods = []
        };

        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            ReferenceName = invokedReferenceName
        };

        var invokedMethodReal = new Method
        {
            Id = invokedMethodId,
            Name = "AnyMethod",
            Parameters = [],
            LocalVariables = []
        };

        var classContainingMethod = new Class
        {
            Id = Guid.NewGuid(),
            Methods = [method],
            Attributes = []
        };

        _mockMethodRepository.Setup(r => r.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethodReal);
        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classContainingMethod]);

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldThrow_WhenAttributeClassDoesNotContainMethod()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Parameters = [],
            LocalVariables = [],
            Methods = []
        };

        var invokedMethod = new InvokedMethod { Id = invokedMethodId, ReferenceName = "attr1" };

        var attributeClass = new Class { Id = Guid.NewGuid(), Methods = [] };

        var attribute = new Attribute { Name = "attr1", Type = attributeClass };

        var classContainingMethod = new Class
        {
            Id = Guid.NewGuid(),
            Methods = [method],
            Attributes = [attribute]
        };

        _mockMethodRepository.Setup(r => r.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(new Method()); // ← necesario
        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classContainingMethod]);
        _mockClassRepository.Setup(r => r.Find(attributeClass.Id)).Returns(attributeClass);

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldThrow_WhenParameterClassDoesNotContainMethod()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var parameterClassId = Guid.NewGuid();

        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Parameters =
            [
                new Parameter
                {
                    Name = "param1",
                    ParameterType = new Class { Id = parameterClassId }
                }

            ],
            LocalVariables = [],
            Methods = []
        };

        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            ReferenceName = "param1"
        };

        var classContainingMethod = new Class
        {
            Id = Guid.NewGuid(),
            Methods = [method]
        };

        var parameterClass = new Class
        {
            Id = parameterClassId,
            Methods = [] // No contiene métodos → debería lanzar excepción
        };

        _mockMethodRepository.Setup(r => r.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(new Method()); // ← esta línea es clave
        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classContainingMethod]);
        _mockClassRepository.Setup(r => r.Find(parameterClassId)).Returns(parameterClass);

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldThrow_WhenLocalVariableClassDoesNotContainMethod()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var localVariableClassId = Guid.NewGuid();

        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Parameters = [],
            LocalVariables =
            [
                new LocalVariable
                {
                    Name = "localVar1",
                    LocalVariableType = new Class { Id = localVariableClassId }
                }

            ],
            Methods = []
        };

        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            ReferenceName = "localVar1"
        };

        var classContainingMethod = new Class
        {
            Id = Guid.NewGuid(),
            Methods = [method]
        };

        var localVariableClass = new Class
        {
            Id = localVariableClassId,
            Methods = []
        };

        _mockMethodRepository.Setup(r => r.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(new Method());
        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classContainingMethod]);
        _mockClassRepository.Setup(r => r.Find(localVariableClassId)).Returns(localVariableClass);

        _methodService.AddAMethod(methodId, invokedMethodId);
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

        _methodService.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId);

        Assert.AreEqual(invokedMethod, localVariable.InvokedMethod);
        _mockLocalVariableRepository.Verify(repo => repo.Update(localVariable), Times.Once);
    }

    [TestMethod]
    public void SetInvokeMethodToALocalVariable_ShouldThrowException_WhenTypeIsNotPrimitive()
    {
        var localVariableId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var localVariable = new LocalVariable
        {
            Id = localVariableId,
            Name = "myVar",
            LocalVariableType = new Class()
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

        var exception = Assert.ThrowsException<InvalidAttributeException>(() => _methodService.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId));

        Assert.AreEqual("In v1 of this program only primitive types are accepted.", exception.Message);
    }

    [TestMethod]
    public void SetInvokeMethodToAParameter_ShouldThrowException_WhenTypeIsNotPrimitive()
    {
        var id = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var parameter = new Parameter
        {
            Id = id,
            Name = "myVar",
            ParameterType = new Class()
        };

        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            MethodId = Guid.NewGuid(),
            ReferenceName = "ref"
        };

        _mockParameterRepository.Setup(repo => repo.Find(id)).Returns(parameter);
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(invokedMethod);
        _mockParameterRepository.Setup(repo => repo.Update(parameter)).Verifiable();

        var exception = Assert.ThrowsException<InvalidAttributeException>(() => _methodService.SetInvokeMethodToParameter(id, invokedMethodId));

        Assert.AreEqual("In v1 of this program only primitive types are accepted.", exception.Message);
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

        _methodService.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId);
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

        _methodService.SetInvokeMethodToParameter(parameterId, invokedMethodId);

        Assert.AreEqual(invokedMethod, parameter.InvokedMethod);
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

        _methodService.SetInvokeMethodToParameter(parameterId, invokedMethodId);
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
            .Setup(repo => repo.Update(It.Is<Parameter>(p => p == parameter && p.InvokedMethod == invokedMethod)));

        _methodService.SetInvokeMethodToParameter(parameterId, invokedMethodId);

        Assert.AreEqual(invokedMethod, parameter.InvokedMethod);
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

        _methodService.SetInvokeMethodToParameter(parameterId, invokedMethodId);
    }

    [TestMethod]
    public void ToEntity_ShouldThrowArgumentNullException_WhenLocalVariableTypeIsNull()
    {
        var dto = new LocalVariableDTO { Name = "myVar", LocalVariableType = null };

        var exception = Assert.ThrowsException<ArgumentNullException>(dto.ToEntity);

        Assert.AreEqual("The Type property cannot be null. (Parameter 'Type')", exception.Message);
    }

    [TestMethod]
    public void ToEntity_ShouldThrowArgumentNullException_WhenParameterTypeIsNull()
    {
        var dto = new ParameterDTO { Name = "myVar", ParameterType = null };
        var exception = Assert.ThrowsException<ArgumentNullException>(dto.ToEntity);
        Assert.AreEqual("The Type property cannot be null. (Parameter 'Type')", exception.Message);
    }

    [TestMethod]
    public void ToEntity_ShouldThrowArgumentNullException_WhenTypeArgumentIsNull()
    {
        var dto = new AttributeDto { Name = "attr1", Accessibility = Accessibility.Private, Type = null };

        var exception = Assert.ThrowsException<ArgumentNullException>(dto.ToEntity);

        Assert.AreEqual("The Type property cannot be null. (Parameter 'Type')", exception.Message);
    }

    [TestMethod]
    public void ExecuteMethodExample1_ShouldReturnMethodExecuted_WhenValidInputProvided()
    {
        var encender = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Encender",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            Stereotype = null
        };

        var encenderInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = encender.Id,
            ReferenceName = "motorAuto"
        };

        var apagar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Apagar",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            Stereotype = null
        };

        var apagarInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = apagar.Id,
            ReferenceName = "motorAuto"
        };

        var iniciarViaje = new Method
        {
            Id = Guid.NewGuid(),
            Name = "IniciarViaje",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            Stereotype = null
        };

        var iniciarViajeInvoked = new InvokedMethod
        {
            Id = iniciarViaje.Id,
            MethodId = iniciarViaje.Id,
            ReferenceName = "this"
        };

        iniciarViaje.Methods = [encenderInvoked, iniciarViajeInvoked];

        var detenerViaje = new Method
        {
            Id = Guid.NewGuid(),
            Name = "DetenerViaje",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            Stereotype = null
        };
        detenerViaje.Methods = [apagarInvoked];

        var motor = new Class()
        {
            Id = Guid.NewGuid(),
            Name = "Motor",
            Stereotype = null,
            HierarchyClass = null,
            Attributes = [],
            Methods = [encender, apagar]
        };

        var motorAuto = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "motorAuto",
            Accessibility = Accessibility.Private,
            Type = motor
        };

        var auto = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Auto",
            Stereotype = null,
            HierarchyClass = null,
            Attributes = [motorAuto],
            Methods = [iniciarViaje, detenerViaje]
        };

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([motor, auto]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([motor, auto]);
        _mockMethodRepository.Setup(repo => repo.Find(encenderInvoked.MethodId)).Returns(encender);
        _mockMethodRepository.Setup(repo => repo.Find(iniciarViajeInvoked.MethodId)).Returns(iniciarViaje);

        var result = _methodService.ExecuteMethod(auto.Id, auto.Id, iniciarViaje.Id);
        Console.WriteLine(result);
        var result2 =
            "Auto.IniciarViaje() -> Auto.IniciarViaje()\n" +
            "\tmotorAuto.Encender() -> Motor.Encender()\n" +
            "\tthis.IniciarViaje() -> Auto.IniciarViaje()";
        Assert.AreEqual(result, result2);
    }

    [TestMethod]
    public void ExecuteMethodExample2_ShouldReturnMethodExecuted_WhenValidInputProvided()
    {
        var id = Guid.NewGuid();
        var confirmar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Confirmar",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            ReturnType = null,
            Stereotype = null
        };

        var confirmarInvoked1 = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = confirmar.Id,
            ReferenceName = "aviso"
        };

        var confirmarInvoked2 = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = confirmar.Id,
            ReferenceName = "otroAviso"
        };

        var enviar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Enviar",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            ReturnType = null,
            Stereotype = KeyWord.Abstract
        };

        var enviarInvoked1 = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = enviar.Id,
            ReferenceName = "aviso"
        };

        var enviarEmail = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Enviar",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            ReturnType = null,
            Stereotype = null
        };
        var enviarInvoked2 = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = enviarEmail.Id,
            ReferenceName = "otroAviso"
        };

        var enviarSMS = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Enviar",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            ReturnType = null,
            Stereotype = null
        };

        var notificacion = new Class
        {
            Id = id,
            Name = "Notificacion",
            Stereotype = KeyWord.Abstract,
            HierarchyClass = null,
            Attributes = [],
            Methods = [enviar, confirmar]
        };

        var email = new Class
        {
            Id = id,
            Name = "Email",
            Stereotype = null,
            HierarchyClass = notificacion,
            Attributes = [],
            Methods = [enviarEmail]
        };

        var sms = new Class
        {
            Id = id,
            Name = "SMS",
            Stereotype = null,
            HierarchyClass = notificacion,
            Attributes = [],
            Methods = [enviarSMS]
        };

        var otroAviso = new LocalVariable()
        {
            Id = Guid.NewGuid(),
            Name = "otroAviso",
            LocalVariableType = email
        };

        var notificar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Notificar",
            Accessibility = Accessibility.Public,
            Parameters = [
                new Parameter
                {
                    Id = Guid.NewGuid(),
                    Name = "aviso",
                    ParameterType = notificacion
                }

            ],
            Methods = [enviarInvoked1, confirmarInvoked1, enviarInvoked2, confirmarInvoked2],
            LocalVariables = [otroAviso],
            Stereotype = null
        };

        var notificador = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Notificador",
            Stereotype = null,
            HierarchyClass = null,
            Attributes = [],
            Methods = [notificar]
        };

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([notificador, sms, email, notificacion]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([notificador, sms, email, notificacion]);
        _mockMethodRepository.Setup(repo => repo.Find(confirmarInvoked1.MethodId)).Returns(confirmar);
        _mockMethodRepository.Setup(repo => repo.Find(confirmarInvoked2.MethodId)).Returns(confirmar);
        _mockMethodRepository.Setup(repo => repo.Find(enviarInvoked1.MethodId)).Returns(enviar);
        _mockMethodRepository.Setup(repo => repo.Find(enviarInvoked2.MethodId)).Returns(enviarEmail);

        var result = _methodService.ExecuteMethod(notificador.Id, notificador.Id, notificar.Id);
        var result2 = _methodService.ExecuteMethod(notificacion.Id, sms.Id, enviar.Id);
        Console.WriteLine(result2);
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(result);
        Assert.AreEqual("Notificacion.Enviar() -> SMS.Enviar()", result2);
        var res =
            "Notificador.Notificar() -> Notificador.Notificar()\n" +
            "\taviso.Enviar() -> Notificacion.Enviar()\n" +
            "\taviso.Confirmar() -> Notificacion.Confirmar()\n" +
            "\totroAviso.Enviar() -> Email.Enviar()\n" +
            "\totroAviso.Confirmar() -> Notificacion.Confirmar()";

        Assert.AreEqual(res, result);
    }

    [TestMethod]
    public void ExecuteMethodExample3_ShouldReturnMethodExecuted_WhenValidInputProvided()
    {
        var limpiar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Limpiar",
            Accessibility = Accessibility.Protected,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            ReturnType = null,
            Stereotype = null
        };
        var limpiarInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = limpiar.Id,
            ReferenceName = "this"
        };

        var inicializar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Inicializar",
            Accessibility = Accessibility.Protected,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            ReturnType = null,
            Stereotype = null
        };
        var inicializarInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = inicializar.Id,
            ReferenceName = "this"
        };

        var esValidoEntrada = new Method
        {
            Id = Guid.NewGuid(),
            Name = "EsValido",
            Accessibility = Accessibility.Public,
            Methods = [inicializarInvoked],
            Parameters = [],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
            Stereotype = null
        };
        var esValidoEntradaInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = esValidoEntrada.Id,
            ReferenceName = "base"
        };

        var esValidoEntradaTexto = new Method
        {
            Id = Guid.NewGuid(),
            Name = "EsValido",
            Accessibility = Accessibility.Public,
            Methods = [esValidoEntradaInvoked, limpiarInvoked],
            Parameters = [],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
            Stereotype = null
        };
        var esValidoEntradaTextoInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = esValidoEntradaTexto.Id,
            ReferenceName = "base"
        };

        var esValidoEntradaTextoEspecial = new Method
        {
            Id = Guid.NewGuid(),
            Name = "EsValido",
            Accessibility = Accessibility.Public,
            Methods = [esValidoEntradaTextoInvoked],
            Parameters = [],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
            Stereotype = KeyWord.Sealed
        };
        var esValidoEntradaTextoEspecialInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = esValidoEntradaTextoEspecial.Id,
            ReferenceName = "input"
        };

        var entrada = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Entrada",
            Stereotype = null,
            HierarchyClass = null,
            Attributes = [],
            Methods = [esValidoEntrada, inicializar, limpiar]
        };

        var entradaTexto = new Class
        {
            Id = Guid.NewGuid(),
            Name = "EntradaTexto",
            Stereotype = null,
            HierarchyClass = entrada,
            Attributes = [],
            Methods = [esValidoEntradaTexto]
        };

        var entradaTextoEspecial = new Class
        {
            Id = Guid.NewGuid(),
            Name = "EntradaTextoEspecial",
            Stereotype = KeyWord.Sealed,
            HierarchyClass = entradaTexto,
            Attributes = [],
            Methods = [esValidoEntradaTextoEspecial]
        };

        var input = new LocalVariable
        {
            Id = Guid.NewGuid(),
            Name = "input",
            LocalVariableType = entradaTextoEspecial
        };

        var validar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Validar",
            Accessibility = Accessibility.Public,
            Methods = [esValidoEntradaTextoEspecialInvoked],
            Parameters = [],
            LocalVariables = [input],
            ReturnType = new Primitive(PrimitiveType.Bool),
            Stereotype = null
        };

        var validador = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Validador",
            Stereotype = null,
            HierarchyClass = null,
            Attributes = [],
            Methods = [validar]
        };

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([validador, entrada, entradaTexto, entradaTextoEspecial]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([validador, entrada, entradaTexto, entradaTextoEspecial]);
        _mockMethodRepository.Setup(repo => repo.Find(esValidoEntradaTextoEspecialInvoked.MethodId)).Returns(esValidoEntradaTextoEspecial);
        _mockMethodRepository.Setup(repo => repo.Find(esValidoEntradaTextoInvoked.MethodId)).Returns(esValidoEntradaTexto);
        _mockMethodRepository.Setup(repo => repo.Find(esValidoEntradaInvoked.MethodId)).Returns(esValidoEntrada);
        _mockMethodRepository.Setup(repo => repo.Find(inicializarInvoked.MethodId)).Returns(inicializar);
        _mockMethodRepository.Setup(repo => repo.Find(limpiarInvoked.MethodId)).Returns(limpiar);

        var result = _methodService.ExecuteMethod(validador.Id, validador.Id, validar.Id);

        var res =
            "Validador.Validar() -> Validador.Validar()\n" +
            "\tinput.EsValido() -> EntradaTextoEspecial.EsValido()\n" +
            "\t\tbase.EsValido() -> EntradaTexto.EsValido()\n" +
            "\t\t\tbase.EsValido() -> Entrada.EsValido()\n" +
            "\t\t\t\tthis.Inicializar() -> Entrada.Inicializar()\n" +
            "\t\t\tthis.Limpiar() -> Entrada.Limpiar()";
        Console.WriteLine(result);
        Assert.AreEqual(res, result);
    }

    [TestMethod]
    public void ExecuteMethodExample4_ShouldReturnMethodExecuted_WhenValidInputProvided()
    {
        var nombre = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Nombre",
            Accessibility = Accessibility.Public,
            Type = new Primitive(PrimitiveType.String),
        };

        var validar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Validar",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
        };
        var validarInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = validar.Id,
            ReferenceName = "usuario"
        };

        var mensaje = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "Mensaje",
            ParameterType = new Primitive(PrimitiveType.String),
        };

        var registrar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Registrar",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [mensaje],
            LocalVariables = [],
            ReturnType = null
        };
        var registrarInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = registrar.Id,
            ReferenceName = "loggerLocal"
        };

        var logger = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Logger",
            Stereotype = null,
            HierarchyClass = null,
            Attributes = [],
            Methods = [registrar],
        };

        var loggerLocal = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "loggerLocal",
            Accessibility = Accessibility.Private,
            Type = logger
        };

        var preparar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Preparar",
            Accessibility = Accessibility.Private,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            ReturnType = null,
        };
        var prepararInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = preparar.Id,
            ReferenceName = "this"
        };

        var usuario = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Usuario",
            Stereotype = null,
            HierarchyClass = null,
            Attributes = [nombre],
            Methods = [validar],
        };

        var usuarioParam = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "usuario",
            ParameterType = usuario
        };

        var validarCredencialesAutenticator = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ValidarCredenciales",
            Accessibility = Accessibility.Protected,
            Methods = [],
            Parameters = [usuarioParam],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
            Stereotype = KeyWord.Abstract
        };
        var validarCredencialesAutenticatorInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = validarCredencialesAutenticator.Id,
            ReferenceName = "this"
        };

        var logAcceso = new Method
        {
            Id = Guid.NewGuid(),
            Name = "LogAcceso",
            Accessibility = Accessibility.Protected,
            Methods = [],
            Parameters = [usuarioParam],
            LocalVariables = [],
            ReturnType = null,
        };
        var logAccesoInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = logAcceso.Id,
            ReferenceName = "this"
        };

        var autenticar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Autenticar",
            Accessibility = Accessibility.Public,
            Methods = [prepararInvoked, validarCredencialesAutenticatorInvoked, logAccesoInvoked, registrarInvoked],
            Parameters = [usuarioParam],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
        };

        var methodId4 = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
        var toString = new Method
        {
            Id = methodId4,
            Name = "ToString",
            Accessibility = Accessibility.Public,
            Stereotype = null,
        };
        var toStringInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = toString.Id,
            ReferenceName = "this"
        };

        var clavePrivada = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "clavePrivada",
            Accessibility = Accessibility.Private,
            Type = new Primitive(PrimitiveType.String),
            InvokedMethod = toStringInvoked
        };

        var validarCredencialesAP = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ValidarCredenciales",
            Accessibility = Accessibility.Protected,
            Methods = [validarInvoked, toStringInvoked],
            Parameters = [usuarioParam],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
        };

        var autenticador = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Autenticador",
            Stereotype = KeyWord.Abstract,
            Attributes = [loggerLocal],
            Methods = [autenticar, preparar, logAcceso, validarCredencialesAutenticator],
        };

        var autenticadorPassword = new Class
        {
            Id = Guid.NewGuid(),
            Name = "AutenticadorPassword",
            Stereotype = KeyWord.Sealed,
            Attributes = [clavePrivada],
            Methods = [validarCredencialesAP],
            HierarchyClass = autenticador
        };

        var stringPrimitive = new Primitive(PrimitiveType.String);

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([usuario, logger, autenticador, autenticadorPassword]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([usuario, logger, autenticador, autenticadorPassword, stringPrimitive]);
        _mockMethodRepository.Setup(repo => repo.Find(prepararInvoked.MethodId)).Returns(preparar);
        _mockMethodRepository.Setup(repo => repo.Find(validarInvoked.MethodId)).Returns(validar);
        _mockMethodRepository.Setup(repo => repo.Find(registrarInvoked.MethodId)).Returns(registrar);
        _mockMethodRepository.Setup(repo => repo.Find(validarCredencialesAutenticatorInvoked.MethodId)).Returns(validarCredencialesAutenticator);
        _mockMethodRepository.Setup(repo => repo.Find(logAccesoInvoked.MethodId)).Returns(logAcceso);
        _mockMethodRepository.Setup(repo => repo.Find(toStringInvoked.MethodId)).Returns(toString);

        var result = _methodService.ExecuteMethod(autenticador.Id, autenticadorPassword.Id, autenticar.Id);

        var res =
            "Autenticador.Autenticar(Usuario) -> Autenticador.Autenticar(Usuario)\n" +
            "\tthis.Preparar() -> Autenticador.Preparar()\n" +
            "\tthis.ValidarCredenciales(Usuario) -> AutenticadorPassword.ValidarCredenciales(Usuario)\n" +
            "\t\tusuario.Validar() -> Usuario.Validar()\n" +
            "\t\tthis.clavePrivada.ToString() -> String.ToString()\n" + // la S deberia ser mayuscula o minuscula ??
            "\tthis.LogAcceso(Usuario) -> Autenticador.LogAcceso(Usuario)\n" +
            "\tloggerLocal.Registrar(string) -> Logger.Registrar(string)"; // la S deberia ser mayuscula o minuscula ??
        Console.WriteLine(result);
        Assert.AreEqual(res, result);
    }

    [TestMethod]
    public void ExecuteMethodExample5_ShouldReturnMethodExecuted_WhenValidInputProvided()
    {
        var id = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Id",
            Accessibility = Accessibility.Public,
            Type = new Primitive(PrimitiveType.Int)
        };

        var validarIdentidad = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ValidarIdentidad",
            Accessibility = Accessibility.Public,
            LocalVariables = [],
            Methods = [],
            ReturnType = new Primitive(PrimitiveType.Bool)
        };
        var validarIdentidadInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = validarIdentidad.Id,
            ReferenceName = "cliente"
        };

        var cliente = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Cliente",
            Stereotype = null,
            Attributes = [id],
            Methods = [validarIdentidad],
            HierarchyClass = null,
        };

        var mensaje = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "mensaje",
            ParameterType = new Primitive(PrimitiveType.String)
        };

        var enviarNotificacion = new Method
        {
            Id = Guid.NewGuid(),
            Name = "EnviarNotificacion",
            Accessibility = Accessibility.Public,
            Methods = [],
            LocalVariables = [],
            Parameters = [mensaje],
            ReturnType = null,
        };
        var enviarNotificacionInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = enviarNotificacion.Id,
            ReferenceName = "notificador"
        };

        var comunicacion = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Comunicacion",
            Stereotype = null,
            Attributes = [],
            HierarchyClass = null,
            Methods = [enviarNotificacion],
        };

        var notificador = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "notificador",
            Type = comunicacion,
            Accessibility = Accessibility.Protected,
        };

        var clienteParam = new Parameter { Id = Guid.NewGuid(), Name = "cliente", ParameterType = cliente };

        var realizarProcesoProceso = new Method
        {
            Id = Guid.NewGuid(),
            Name = "RealizarProceso",
            Accessibility = Accessibility.Protected,
            Methods = [],
            LocalVariables = [],
            Parameters = [clienteParam],
            ReturnType = null,
            Stereotype = KeyWord.Abstract
        };
        var realizarProcesoProcesoInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = realizarProcesoProceso.Id,
            ReferenceName = "this"
        };

        var ejecutar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Ejecutar",
            Stereotype = null,
            Accessibility = Accessibility.Public,
            Methods = [realizarProcesoProcesoInvoked, enviarNotificacionInvoked],
            LocalVariables = [],
            Parameters = [clienteParam],
            ReturnType = null,
        };

        var validar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Validar",
            Stereotype = null,
            Accessibility = Accessibility.Protected,
            Methods = [validarIdentidadInvoked],
            LocalVariables = [],
            Parameters = [clienteParam],
            ReturnType = new Primitive(PrimitiveType.Bool),
        };
        var validarInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = validar.Id,
            ReferenceName = "this"
        };

        var proceso = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Proceso",
            Stereotype = KeyWord.Abstract,
            Methods = [ejecutar, validar, realizarProcesoProceso],
            Attributes = [notificador],
            HierarchyClass = null,
        };

        var inventarioDisponible = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "inventarioDisponible",
            Type = new Primitive(PrimitiveType.Bool),
            Accessibility = Accessibility.Private
        };

        var comprobarInventario = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ComprobarInventario",
            Stereotype = null,
            Accessibility = Accessibility.Private,
            Methods = [],
            LocalVariables = [],
            Parameters = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
        };
        var comprobarInventarioInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = comprobarInventario.Id,
            ReferenceName = "this"
        };

        var realizarProcesoPC = new Method
        {
            Id = Guid.NewGuid(),
            Name = "RealizarProceso",
            Accessibility = Accessibility.Protected,
            Methods = [validarInvoked, comprobarInventarioInvoked, enviarNotificacionInvoked],
            LocalVariables = [],
            Parameters = [clienteParam],
            ReturnType = null,
            Stereotype = KeyWord.Sealed
        };

        var procesoCompra = new Class
        {
            Id = Guid.NewGuid(),
            Name = "ProcesoCompra",
            Stereotype = KeyWord.Sealed,
            Methods = [realizarProcesoPC, comprobarInventario],
            Attributes = [inventarioDisponible],
            HierarchyClass = proceso,
        };

        var stringPrimitive = new Primitive(PrimitiveType.String);

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([cliente, comunicacion, proceso, procesoCompra]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([cliente, comunicacion, proceso, procesoCompra, stringPrimitive]);
        _mockMethodRepository.Setup(repo => repo.Find(validarInvoked.MethodId)).Returns(validar);
        _mockMethodRepository.Setup(repo => repo.Find(comprobarInventarioInvoked.MethodId)).Returns(comprobarInventario);
        _mockMethodRepository.Setup(repo => repo.Find(enviarNotificacionInvoked.MethodId)).Returns(enviarNotificacion);
        _mockMethodRepository.Setup(repo => repo.Find(validarIdentidadInvoked.MethodId)).Returns(validarIdentidad);
        _mockMethodRepository.Setup(repo => repo.Find(realizarProcesoProcesoInvoked.MethodId)).Returns(realizarProcesoProceso);

        var result = _methodService.ExecuteMethod(proceso.Id, procesoCompra.Id, ejecutar.Id);

        var res =
            "Proceso.Ejecutar(Cliente) -> Proceso.Ejecutar(Cliente)\n" +
            "\tthis.RealizarProceso(Cliente) -> ProcesoCompra.RealizarProceso(Cliente)\n" +
            "\t\tthis.Validar(Cliente) -> Proceso.Validar(Cliente)\n" +
            "\t\t\tcliente.ValidarIdentidad() -> Cliente.ValidarIdentidad()\n" +
            "\t\tthis.ComprobarInventario() -> ProcesoCompra.ComprobarInventario()\n" +
            "\t\tnotificador.EnviarNotificacion(string) -> Comunicacion.EnviarNotificacion(string)\n" +
            "\tnotificador.EnviarNotificacion(string) -> Comunicacion.EnviarNotificacion(string)";
        Console.WriteLine(result);
        Assert.AreEqual(res, result);
    }
}
