using Moq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IDataAcces;
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
            ParameterType = new TypeDTO { Name = "TestClass", }
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
        var classT = new TypeDTO
        {
            Name = "TestClass"
        };
        var method = new MethodDTO
        {
            Name = "TestMethod",
            Accessibility = Accessibility.Public,
            Stereotype = null,
            ReturnType = classT
        };
        var classes = new List<Type>();
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns(classes);

        var exception = Assert.ThrowsException<MethodException>(() => _methodService.CreateMethod(method));
        Assert.AreEqual("Method return type not found.", exception.Message);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowException_WhenKeyAccessibilityIsNotValid()
    {
        var invalidAccessibility = (Accessibility)100;
        var classT = new TypeDTO
        {
            Name = "TestClass"
        };
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
        var method = new Method
        {
            Id = Guid.NewGuid(),
            Name = "TestMethod"
        };
        var classT = new Class
        {
            Id = Guid.NewGuid(),
            Name = "TestClass",
            Methods = []
        };

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
        var method = new Method
        {
            Id = Guid.NewGuid(),
            Name = "TestMethod"
        };
        var existingMethod = new Method
        {
            Id = Guid.NewGuid(),
            Name = "TestMethod"
        };
        var classT = new Class
        {
            Id = Guid.NewGuid(),
            Name = "TestClass",
            Methods = [existingMethod]
        };

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
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Methods = []
        };

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
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod"
        };

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
        var localVariable = new LocalVariable
        {
            Id = localVariableId,
            Name = "TestLocalVariable"
        };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            LocalVariables = []
        };

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
        var parameter = new Parameter
        {
            Id = parameterId,
            Name = "TestParameter"
        };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Parameters = []
        };

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
        var parameter = new Parameter
        {
            Id = parameterId,
            Name = "TestParameter"
        };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            LocalVariables = [],
            Parameters = [parameter]
        };

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
        var parameter = new Parameter
        {
            Id = parameterId,
            Name = "TestParameter"
        };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            LocalVariables = [],
            Parameters = [new Parameter
            {
                Name = "TestParameter"
            }

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

        var exception = Assert.ThrowsException<LocalVariableNotFoundException>(() =>
            _methodService.CreateParameter(parameterDto));

        Assert.AreEqual("Parameter Name cannot be empty.", exception.Message);
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

    [TestMethod]
    public void AssociateInvokeMethod_ShouldReturnId_WhenValidInput()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodDto = new InvokedMethodDTO { MethodId = methodId, ReferenceName = "someVariable" };

        var method = new Method
        {
            Id = methodId,
            Parameters = [new Parameter
            {
                Name = "someVariable"
            }

            ],
            LocalVariables = [],
        };

        var classT = new Class
        {
            Methods = [method],
            Attributes = []
        };

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

        // Setup para evitar MockException del repositorio de métodos
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
            Parameters = [new Parameter
            {
                Name = "SomeName"
            }

            ],
            Methods = []
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
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
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([classWithMethod]);

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    public void AddAMethod_ShouldAddInvokedMethod_WhenValid()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            ReferenceName = "TestReference"
        };
        var method = new Method
        {
            Id = methodId,
            Name = "MyMethod",
            Parameters = [new Parameter
            {
                Name = "TestReference"
            }

            ],
            Methods = []
        };
        var classWithMethod = new Class
        {
            Methods = [method],
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([classWithMethod]);
        _mockMethodRepository.Setup(repo => repo.Update(It.IsAny<Method>()));

        _methodService.AddAMethod(methodId, invokedMethodId);

        _mockMethodRepository.Verify(repo => repo.Update(It.Is<Method>(m => m.Methods.Contains(invokedMethod))),
            Times.Once);
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

        var classT = new Class { Methods = [new Method { Id = methodId, Name = "TestMethod" }], Attributes = [] };

        _mockMethodRepository.Setup(r => r.Find(methodId)).Returns(method);
        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT]);
        _mockMethodRepository.Setup(r => r.Update(It.IsAny<Method>()));

        _methodService.AddAMethod(methodId, invokedMethodId);

        _mockMethodRepository.Verify(r => r.Update(It.Is<Method>(m => m.Methods.Contains(invokedMethod))), Times.Once);
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

        var hierarchyClass = new Class { Methods = [new Method { Name = methodName }] };

        var classWithMethod = new Class { Methods = [method], HierarchyClass = hierarchyClass };

        _mockMethodRepository.Setup(r => r.Find(methodId)).Returns(method);
        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classWithMethod]);
        _mockMethodRepository.Setup(r => r.Update(It.IsAny<Method>()));

        _methodService.AddAMethod(methodId, invokedMethodId);

        _mockMethodRepository.Verify(r => r.Update(It.Is<Method>(m => m.Methods.Contains(invokedMethod))), Times.Once);
    }

    [TestMethod]
    public void SetInvokeMethodToALocalVariable_ShouldAssociateInvokedMethodToLocalVariable()
    {
        var localVariableId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var localVariable = new LocalVariable
        {
            Id = localVariableId,
            Name = "myVar"
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

        _methodService.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId);

        Assert.AreEqual(invokedMethod, localVariable.InvokedMethod);
        _mockLocalVariableRepository.Verify(repo => repo.Update(localVariable), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(InvokedMethodNotFoundException))]
    public void SetInvokeMethodToLocalVariable_ShouldThrowException_WhenInvokedMethodNotFound()
    {
        var localVariableId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var localVariable = new LocalVariable
        {
            Id = localVariableId,
            Name = "myVar"
        };

        _mockLocalVariableRepository.Setup(repo => repo.Find(localVariableId)).Returns(localVariable);
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns((InvokedMethod?)null);

        _methodService.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId);
    }

    [TestMethod]
    public void SetInvokeMethodToParameter_ShouldAssociateInvokedMethodToParameter()
    {
        var parameterId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var parameter = new Parameter
        {
            Id = parameterId,
            Name = "param"
        };

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

        var parameter = new Parameter
        {
            Id = parameterId,
            Name = "param"
        };

        _mockParameterRepository.Setup(repo => repo.Find(parameterId)).Returns(parameter);
        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns((InvokedMethod?)null);

        _methodService.SetInvokeMethodToParameter(parameterId, invokedMethodId);
    }

    [TestMethod]
    public void SetInvokeMethodToParameter_ShouldAssociateInvokedMethod_WhenBothExist()
    {
        var parameterId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var parameter = new Parameter
        {
            Id = parameterId
        };
        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId
        };

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
        var dto = new LocalVariableDTO
        {
            Name = "myVar",
            LocalVariableType = null
        };

        var exception = Assert.ThrowsException<ArgumentNullException>(dto.ToEntity);

        Assert.AreEqual("The Type property cannot be null. (Parameter 'Type')", exception.Message);
    }

    [TestMethod]
    public void ToEntity_ShouldThrowArgumentNullException_WhenParameterTypeIsNull()
    {
        var dto = new ParameterDTO
        {
            Name = "myVar",
            ParameterType = null
        };
        var exception = Assert.ThrowsException<ArgumentNullException>(dto.ToEntity);
        Assert.AreEqual("The Type property cannot be null. (Parameter 'Type')", exception.Message);
    }

    [TestMethod]
    public void ToEntity_ShouldThrowArgumentNullException_WhenTypeArgumentIsNull()
    {
        var dto = new AttributeDto
        {
            Name = "attr1",
            Accessibility = Accessibility.Private,
            Type = null
        };

        var exception = Assert.ThrowsException<ArgumentNullException>(dto.ToEntity);

        Assert.AreEqual("The Type property cannot be null. (Parameter 'Type')", exception.Message);
    }
}
