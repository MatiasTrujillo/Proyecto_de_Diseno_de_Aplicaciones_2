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
        _methodService = new MethodService(_mockMethodRepository.Object, _mockClassRepository.Object,
            _mockParameterRepository.Object, _mockLocalVariableRepository.Object, _mockInvokedMethodRepository.Object, _mockMethodSignatureRepository.Object);
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
    public void CreateMethod_ShouldCallRepositoryAdd_WithPrimitiveReturnType()
    {
        var classes = new List<Type>();
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns(classes);

        var primitiveTypeName = "int";
        var methodDto = new MethodDTO
        {
            Name = "TestMethod",
            Accessibility = Accessibility.Public,
            Stereotype = null,
            ReturnType = new TypeDTO
            {
                Name = primitiveTypeName,
                Kind = "primitive"
            }
        };

        var primitive = new Primitive(PrimitiveType.Int);
        primitive.Name = "int";
        classes.Add(primitive);

        _methodService.CreateMethod(methodDto);

        _mockMethodRepository.Verify(repo => repo.Add(
            It.Is<Method>(m =>
                m.Name.Equals(methodDto.Name) &&
                m.Accessibility == methodDto.Accessibility &&
                m.Stereotype == null &&
                m.ReturnType is Primitive)), Times.Once);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowException_WhenReturnTypeIsNull()
    {
        var classT = new TypeDTO { Name = "TestClass", Kind = "class" };
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
    public void CreateMethod_ShouldThrowException_WhenReturnTypePrimitiveIsNull()
    {
        var primitive = new TypeDTO { Name = "not", Kind = "primitive" };
        var method = new MethodDTO
        {
            Name = "TestMethod",
            Accessibility = Accessibility.Public,
            Stereotype = null,
            ReturnType = primitive
        };
        var classes = new List<Type>();
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns(classes);

        var exception = Assert.ThrowsException<InvalidOperationException>(() => _methodService.CreateMethod(method));
        Assert.AreEqual("Invalid primitive type name: not", exception.Message);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowException_WhenReturnTypeIsInvalid()
    {
        var classT = new TypeDTO { Name = "TestClass", Kind = "wrong" };
        var method = new MethodDTO
        {
            Name = "TestMethod",
            Accessibility = Accessibility.Public,
            Stereotype = null,
            ReturnType = classT
        };
        var classes = new List<Type>();
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns(classes);

        var exception = Assert.ThrowsException<InvalidOperationException>(() => _methodService.CreateMethod(method));
        Assert.AreEqual($"Unknown type kind: {classT.Kind}", exception.Message);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowException_WhenKeyAccessibilityIsNotValid()
    {
        var invalidAccessibility = (Accessibility)100;
        var classT = new TypeDTO { Name = "TestClass", Kind = "class" };
        var method = new MethodDTO
        {
            Name = "TestMethod",
            Accessibility = invalidAccessibility,
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
        var classT = new TypeDTO { Name = "TestClass", Kind = "class" };
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
    public void AssociateMethodWithClass_ShouldThrowException_WhenMethodIsOverrideAndFatherIsNotVirtual()
    {
        var methodParent = new Method { Id = Guid.NewGuid(), Name = "TestMethod" };
        var classParent = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [methodParent] };
        var methodChild = new Method { Id = Guid.NewGuid(), Name = "TestMethod", IsOverride = true };
        var classChild = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [], Parents = [classParent] };

        _mockMethodRepository.Setup(repo => repo.Find(methodParent.Id)).Returns(methodParent);
        _mockMethodRepository.Setup(repo => repo.Find(methodChild.Id)).Returns(methodChild);
        _mockClassRepository.Setup(repo => repo.Find(classParent.Id)).Returns(classParent);
        _mockClassRepository.Setup(repo => repo.Find(classChild.Id)).Returns(classChild);
        _mockClassRepository.Setup(repo => repo.Update(It.IsAny<Class>()));

        var exception = Assert.ThrowsException<MethodException>(() => _methodService.AssociateMethodWithClass(methodChild.Id, classChild.Id));
        Assert.AreEqual("Parent class must have this method as virtual.", exception.Message);
    }

    [TestMethod]
    public void AssociateMethodWithClass_ShouldThrowException_WhenMethodIsOverrideAndParentDoesNotHaveMethod()
    {
        var classParent = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [] };
        var methodChild = new Method { Id = Guid.NewGuid(), Name = "TestMethod", IsOverride = true };
        var classChild = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [], Parents = [classParent] };

        _mockMethodRepository.Setup(repo => repo.Find(methodChild.Id)).Returns(methodChild);
        _mockClassRepository.Setup(repo => repo.Find(classParent.Id)).Returns(classParent);
        _mockClassRepository.Setup(repo => repo.Find(classChild.Id)).Returns(classChild);
        _mockClassRepository.Setup(repo => repo.Update(It.IsAny<Class>()));

        var exception = Assert.ThrowsException<MethodException>(() => _methodService.AssociateMethodWithClass(methodChild.Id, classChild.Id));
        Assert.AreEqual("There is no parent class with this method.", exception.Message);
    }

    [TestMethod]
    public void AssociateMethodWithClass_ShouldAssociate_WhenMethodIsOverrideAndParentIsVirtual()
    {
        var methodParent = new Method { Id = Guid.NewGuid(), Name = "TestMethod", Stereotype = KeyWord.Virtual };
        var classParent = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [methodParent] };
        var methodChild = new Method { Id = Guid.NewGuid(), Name = "TestMethod", IsOverride = true };
        var classChild = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [], Parents = [classParent] };

        _mockMethodRepository.Setup(repo => repo.Find(methodParent.Id)).Returns(methodParent);
        _mockMethodRepository.Setup(repo => repo.Find(methodChild.Id)).Returns(methodChild);
        _mockClassRepository.Setup(repo => repo.Find(classParent.Id)).Returns(classParent);
        _mockClassRepository.Setup(repo => repo.Find(classChild.Id)).Returns(classChild);
        _mockClassRepository.Setup(repo => repo.Update(It.IsAny<Class>()));

        _methodService.AssociateMethodWithClass(methodChild.Id, classChild.Id);

        Assert.IsTrue(classChild.Methods.Contains(methodChild));
        _mockClassRepository.Verify(repo => repo.Update(classChild), Times.Once);
    }

    [TestMethod]
    public void AssociateMethodWithClass_ShouldAssociate_WhenMethodIsOverrideAndGrandparentIsVirtual()
    {
        var methodGrand = new Method { Id = Guid.NewGuid(), Name = "TestMethod", Stereotype = KeyWord.Virtual };
        var classGrand = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [methodGrand] };
        var classParent = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [], Parents = [classGrand] };
        var methodChild = new Method { Id = Guid.NewGuid(), Name = "TestMethod", IsOverride = true };
        var classChild = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [], Parents = [classParent] };

        _mockMethodRepository.Setup(repo => repo.Find(methodGrand.Id)).Returns(methodGrand);
        _mockMethodRepository.Setup(repo => repo.Find(methodChild.Id)).Returns(methodChild);
        _mockClassRepository.Setup(repo => repo.Find(classParent.Id)).Returns(classParent);
        _mockClassRepository.Setup(repo => repo.Find(classGrand.Id)).Returns(classGrand);
        _mockClassRepository.Setup(repo => repo.Find(classChild.Id)).Returns(classChild);
        _mockClassRepository.Setup(repo => repo.Update(It.IsAny<Class>()));

        _methodService.AssociateMethodWithClass(methodChild.Id, classChild.Id);

        Assert.IsTrue(classChild.Methods.Contains(methodChild));
        _mockClassRepository.Verify(repo => repo.Update(classChild), Times.Once);
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
    public void DeleteMethod_ShouldThrowException_WhenMethodIsAssociatedWithInvokedMethod2()
    {
        var methodId = Guid.NewGuid();
        var method1 = new InvokedMethod();
        var method = new Method { Id = methodId, Name = "TestMethod", Methods = [method1] };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([]);

        var invokedMethods = new List<InvokedMethod> { new InvokedMethod { Id = Guid.NewGuid(), MethodId = methodId } };

        _mockInvokedMethodRepository.Setup(repo => repo.FindAll()).Returns(invokedMethods);

        var exception = Assert.ThrowsException<InvalidMethodException>(() => _methodService.DeleteMethod(methodId));

        Assert.AreEqual("Method is associated with another method.", exception.Message);
        _mockMethodRepository.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Never);
    }

    [TestMethod]
    public void DeleteMethod_ShouldThrowException_IfMethodIsInAClass()
    {
        var methodId = Guid.NewGuid();
        var method = new Method { Id = methodId, Name = "TestMethod" };
        var classT = new Class { Id = Guid.NewGuid(), Name = "TestClass", Methods = [method] };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([classT]);

        var invokedMethods = new List<InvokedMethod> { new InvokedMethod { Id = Guid.NewGuid(), MethodId = methodId } };
        _mockInvokedMethodRepository.Setup(repo => repo.FindAll()).Returns(invokedMethods);

        var exception = Assert.ThrowsException<MethodInUseException>(() => _methodService.DeleteMethod(methodId));

        Assert.AreEqual("Method is associated with a class.", exception.Message);
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
        var invokedMethodDto = new InvokedMethodDTO
        {
            MethodId = Guid.NewGuid(),
            ReferenceName = "validReference"
        };

        _mockMethodRepository.Setup(repo => repo.Find(invokedMethodDto.MethodId))
            .Returns((Method)null!);

        var exception = Assert.ThrowsException<MethodNotFoundException>(() =>
            _methodService.AssociateInvokeMethod(invokedMethodDto));

        Assert.AreEqual("Method null.", exception.Message);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodNotFoundException))]
    public void AddAMethod_ShouldThrowMethodNotFoundException_WhenMethodIsNull()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        _mockMethodRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns((Method)null);
        _mockInvokedMethodRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns(new InvokedMethod { Id = invokedMethodId, MethodId = invokedMethodId });

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldThrowMethodException_WhenInvokedMethodReferenceNameIsInvalid()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var invalidReferenceName = "invalidReference";

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
            ReferenceName = invalidReferenceName,
            MethodId = methodId
        };

        var classWithMethod = new Class
        {
            Methods = [method],
            Parents = null
        };

        _mockMethodRepository.Setup(r => r.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(r => r.Find(invokedMethod.MethodId)).Returns(method);
        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classWithMethod]);

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(InvokedMethodNotFoundException))]
    public void AddAMethod_ShouldThrowInvokedMethodNotFoundException_WhenInvokedMethodIsNull()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        _mockMethodRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns(new Method { Id = methodId });
        _mockInvokedMethodRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns((InvokedMethod)null);

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
            Parameters = [new Parameter { Name = "SomeName" }],
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
        var referencedMethodId = Guid.NewGuid();

        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            ReferenceName = "InvalidReference",
            MethodId = referencedMethodId
        };

        var method = new Method
        {
            Id = methodId,
            Name = "MyMethod",
            Parameters = [],
            LocalVariables = [],
            Methods = []
        };

        var invokedRealMethod = new Method
        {
            Id = referencedMethodId,
            Name = "TargetMethod"
        };

        var classWithMethod = new Class
        {
            Methods = [method]
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(invokedRealMethod);
        _mockMethodRepository.Setup(repo => repo.Find(referencedMethodId)).Returns(invokedRealMethod);

        _mockInvokedMethodRepository.Setup(repo => repo.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([classWithMethod]);

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    public void AddAMethod_ShouldSucceed_WhenReferenceNameIsThis()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var methodName = "Test";

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
            ReferenceName = "this",
            MethodId = methodId
        };

        var classWithMethod = new Class
        {
            Methods = [method],
            Parents = null
        };

        _mockMethodRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns(method);
        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classWithMethod]);
        _mockMethodRepository.Setup(r => r.Update(It.IsAny<Method>()));

        _methodService.AddAMethod(methodId, invokedMethodId);

        _mockMethodRepository.Verify(r => r.Update(It.Is<Method>(m => m.Methods.Contains(invokedMethod))), Times.Once);
    }

    [TestMethod]
    public void AddAMethod_ShouldAddInvokedMethod_WhenReferenceIsBase()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var referenceName = "base";

        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Methods = []
        };

        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            ReferenceName = referenceName,
            MethodId = methodId
        };

        var baseClass = new Class
        {
            Methods = [new Method { Name = "TestMethod" }]
        };

        var classWithMethod = new Class
        {
            Methods = [method],
            Parents = [baseClass]
        };

        _mockMethodRepository.Setup(m => m.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(m => m.Find(invokedMethod.MethodId)).Returns(method);
        _mockInvokedMethodRepository.Setup(i => i.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(c => c.FindAll()).Returns([classWithMethod]);
        _mockMethodRepository.Setup(m => m.Update(method));

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    public void AddAMethod_ShouldAddInvokedMethod_WhenReferenceIsAttribute()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var referenceName = "attr1";
        var classId = Guid.NewGuid();
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Methods = []
        };
        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            ReferenceName = referenceName,
            MethodId = methodId
        };
        var classWithMethod = new Class
        {
            Methods = [method],
            Attributes =
            [
                new Attribute { Name = referenceName, ReferenceType = new Class { Id = classId } }
            ]
        };
        var classWithAttr = new Class { Id = classId, Methods = [method] };
        _mockMethodRepository.Setup(m => m.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(m => m.Find(invokedMethod.MethodId)).Returns(method);
        _mockInvokedMethodRepository.Setup(i => i.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(c => c.FindAll()).Returns([classWithMethod]);
        _mockClassRepository.Setup(c => c.Find(classId)).Returns(classWithAttr);
        _mockMethodRepository.Setup(m => m.Update(method));

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldThrowException_WhenReferenceNameIsInvalid2()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var referenceName = "param1";
        var classId = Guid.NewGuid();
        var classAuxId = Guid.NewGuid();
        var classAux = new Class { Id = classAuxId, Name = "classAux" };
        var param = new Parameter("param1", classAux);
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Methods = [],
            Parameters = [param]
        };
        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            ReferenceName = referenceName,
            MethodId = methodId
        };
        var classWithMethod = new Class
        {
            Methods = [method],
            Attributes =
            [
                new Attribute { Name = referenceName, ReferenceType = new Class { Id = classId } }
            ]
        };
        var classWithAttr = new Class { Id = classId, Methods = [method] };
        _mockMethodRepository.Setup(m => m.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(m => m.Find(invokedMethod.MethodId)).Returns(method);
        _mockInvokedMethodRepository.Setup(i => i.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(c => c.FindAll()).Returns([classWithMethod, classAux]);
        _mockClassRepository.Setup(c => c.Find(classId)).Returns(classWithAttr);
        _mockClassRepository.Setup(c => c.Find(classAuxId)).Returns(classAux);
        _mockMethodRepository.Setup(m => m.Update(method));

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldThrowException_WhenReferenceNameIsInvalid3()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var referenceName = "local1";
        var classId = Guid.NewGuid();
        var classAuxId = Guid.NewGuid();
        var classAux = new Class { Id = classAuxId, Name = "classAux" };
        var local = new LocalVariable("local1", classAux);
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Methods = [],
            LocalVariables = [local]
        };
        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            ReferenceName = referenceName,
            MethodId = methodId
        };
        var classWithMethod = new Class
        {
            Methods = [method],
            Attributes =
            [
                new Attribute { Name = referenceName, ReferenceType = new Class { Id = classId } }
            ]
        };
        var classWithAttr = new Class { Id = classId, Methods = [method] };
        _mockMethodRepository.Setup(m => m.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(m => m.Find(invokedMethod.MethodId)).Returns(method);
        _mockInvokedMethodRepository.Setup(i => i.Find(invokedMethodId)).Returns(invokedMethod);
        _mockClassRepository.Setup(c => c.FindAll()).Returns([classWithMethod, classAux]);
        _mockClassRepository.Setup(c => c.Find(classId)).Returns(classWithAttr);
        _mockClassRepository.Setup(c => c.Find(classAuxId)).Returns(classAux);
        _mockMethodRepository.Setup(m => m.Update(method));

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldThrow_WhenAttributeClassDoesNotContainMethod()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var methodToAdd = new Method
        {
            Id = methodId,
            Name = "MainMethod",
            Parameters = [],
            LocalVariables = [],
            Methods = []
        };
        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            MethodId = Guid.NewGuid(),
            ReferenceName = "MyAttr"
        };
        var invMethod = new Method { Id = invokedMethod.MethodId, Name = "Invoked" };
        var attrClass = new Class { Id = Guid.NewGuid(), Methods = [] };
        var attribute = new Simulator.Domain.Attribute { Name = "MyAttr", ReferenceType = attrClass };
        var classWithMethod = new Class { Methods = [methodToAdd], Attributes = [attribute] };

        _mockMethodRepository.Setup(r => r.Find(methodId)).Returns(methodToAdd);
        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockMethodRepository.Setup(r => r.Find(invokedMethod.MethodId)).Returns(invMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classWithMethod]);
        _mockClassRepository.Setup(r => r.Find(attrClass.Id)).Returns(attrClass);

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldThrow_WhenParameterClassDoesNotContainMethod()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var paramClass = new Class { Id = Guid.NewGuid(), Methods = [] };
        var methodToAdd = new Method
        {
            Id = methodId,
            Name = "MainMethod",
            Parameters = [new Parameter { Name = "MyParam", ReferenceType = paramClass }],
            LocalVariables = [],
            Methods = []
        };
        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            MethodId = Guid.NewGuid(),
            ReferenceName = "MyParam"
        };
        var invMethod = new Method { Id = invokedMethod.MethodId, Name = "Invoked" };
        var classWithMethod = new Class { Methods = [methodToAdd] };

        _mockMethodRepository.Setup(r => r.Find(methodId)).Returns(methodToAdd);
        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockMethodRepository.Setup(r => r.Find(invokedMethod.MethodId)).Returns(invMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classWithMethod]);
        _mockClassRepository.Setup(r => r.Find(paramClass.Id)).Returns(paramClass);

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    [ExpectedException(typeof(MethodException))]
    public void AddAMethod_ShouldThrow_WhenLocalVariableClassDoesNotContainMethod()
    {
        var methodId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();
        var localVarClass = new Class { Id = Guid.NewGuid(), Methods = [] };
        var methodToAdd = new Method
        {
            Id = methodId,
            Name = "MainMethod",
            Parameters = [],
            LocalVariables = [new LocalVariable { Name = "MyLocal", ReferenceType = localVarClass }],
            Methods = []
        };
        var invokedMethod = new InvokedMethod
        {
            Id = invokedMethodId,
            MethodId = Guid.NewGuid(),
            ReferenceName = "MyLocal"
        };
        var invMethod = new Method { Id = invokedMethod.MethodId, Name = "Invoked" };
        var classWithMethod = new Class { Methods = [methodToAdd] };

        _mockMethodRepository.Setup(r => r.Find(methodId)).Returns(methodToAdd);
        _mockInvokedMethodRepository.Setup(r => r.Find(invokedMethodId)).Returns(invokedMethod);
        _mockMethodRepository.Setup(r => r.Find(invokedMethod.MethodId)).Returns(invMethod);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classWithMethod]);
        _mockClassRepository.Setup(r => r.Find(localVarClass.Id)).Returns(localVarClass);

        _methodService.AddAMethod(methodId, invokedMethodId);
    }

    [TestMethod]
    public void GetMethods_ShouldReturnListOfMethodDTOs_WhenMethodsExist()
    {
        var methods = new List<Method>
        {
            new Method
            {
                Id = Guid.NewGuid(),
                Name = "TestMethod",
                Accessibility = Accessibility.Public,
                Stereotype = null,
                ReturnType = new Primitive(PrimitiveType.Int)
            }
        };
        _mockMethodRepository.Setup(r => r.FindAll()).Returns(methods);

        var result = _methodService.GetMethods();

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(methods[0].Id.ToString(), result[0].Id);
        Assert.AreEqual("TestMethod", result[0].Name);
        Assert.AreEqual(Accessibility.Public, result[0].Accessibility);
        Assert.AreEqual(null, result[0].Stereotype);
        Assert.IsNotNull(result[0].ReturnType);
    }

    [TestMethod]
    public void ToEntity_ShouldThrowArgumentNullException_WhenLocalVariableTypeIsNull()
    {
        var dto = new LocalVariableDTO { Name = "myVar", ReferenceType = null };

        var exception = Assert.ThrowsException<ArgumentNullException>(dto.ToEntity);

        Assert.AreEqual("The Type property cannot be null. (Parameter 'Type')", exception.Message);
    }

    [TestMethod]
    public void ToEntity_ShouldThrowArgumentNullException_WhenParameterTypeIsNull()
    {
        var dto = new ParameterDTO { Name = "myVar", ReferenceType = null };
        var exception = Assert.ThrowsException<ArgumentNullException>(dto.ToEntity);
        Assert.AreEqual("The Type property cannot be null. (Parameter 'ReferenceType')", exception.Message);
    }

    [TestMethod]
    public void ToEntity_ShouldThrowArgumentNullException_WhenReferenceAndInstanceTypeArgumentAreNull()
    {
        var dto = new AttributeDto { Name = "attr1", Accessibility = Accessibility.Private, ReferenceType = null };

        var exception = Assert.ThrowsException<ArgumentNullException>(dto.ToEntity);

        Assert.AreEqual("The Type property cannot be null. (Parameter 'ReferenceType')", exception.Message);
    }

    [TestMethod]
    public void ExecuteMethodExample1_ShouldReturnMethodExecuted_WhenValidInputProvided()
    {
        var encender = new Method
        {
            Id = Guid.NewGuid(), // 2ecb55dc-487c-4039-bc3a-b69588fc6ea6
            Name = "Encender",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            Stereotype = null
        };

        var encenderInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(), // 81a97a90-c88d-4fba-8560-93a8dc6c3a0c
            MethodId = encender.Id,
            ReferenceName = "motorAuto"
        };

        var apagar = new Method
        {
            Id = Guid.NewGuid(), // 4d44c93b-0212-41cf-8174-f54cc854ab74
            Name = "Apagar",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            Stereotype = null
        };

        var apagarInvoked = new InvokedMethod
        {
            Id = Guid.NewGuid(), // 90826973-b3e2-4dfa-8f68-cb6b5d21ab54
            MethodId = apagar.Id,
            ReferenceName = "motorAuto"
        };

        var iniciarViaje = new Method
        {
            Id = Guid.NewGuid(), // 1e4fea0f-d93a-440f-a555-df7d81b8747f
            Name = "IniciarViaje",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            Stereotype = null
        };

        var iniciarViajeInvoked = new InvokedMethod
        {
            Id = iniciarViaje.Id, // a23e5652-b8b3-4fec-9ff3-f4212270f7f1
            MethodId = iniciarViaje.Id,
            ReferenceName = "this"
        };

        iniciarViaje.Methods = [encenderInvoked, iniciarViajeInvoked];

        var detenerViaje = new Method
        {
            Id = Guid.NewGuid(), // 0267b17c-3388-40d2-b406-325e94336cb0
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
            Id = Guid.NewGuid(), // f23382e9-c58f-4041-ad59-7f77e206d856
            Name = "Motor",
            Stereotype = null,
            Parents = null,
            Attributes = [],
            Methods = [encender, apagar] // check
        };

        var motorAuto = new Attribute
        {
            Id = Guid.NewGuid(), // 4c52a563-8a5b-474f-9652-8a52a45d0b44
            Name = "motorAuto",
            Accessibility = Accessibility.Private,
            ReferenceType = motor
        };

        var auto = new Class
        {
            Id = Guid.NewGuid(), // eaa7898d-8ff4-4164-9b99-4b8779bae7db
            Name = "Auto",
            Stereotype = null,
            Parents = null,
            Attributes = [motorAuto],
            Methods = [iniciarViaje, detenerViaje]
        };

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([motor, auto]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([motor, auto]);
        _mockMethodRepository.Setup(repo => repo.FindAll()).Returns([iniciarViaje]);
        _mockMethodRepository.Setup(repo => repo.Find(encenderInvoked.MethodId)).Returns(encender);
        _mockMethodRepository.Setup(repo => repo.Find(iniciarViajeInvoked.MethodId)).Returns(iniciarViaje);

        var result = _methodService.ExecuteMethod(auto.Id, auto.Id, iniciarViaje.Id, []);
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
            Id = Guid.NewGuid(),
            Name = "Notificacion",
            Stereotype = KeyWord.Abstract,
            Parents = null,
            Attributes = [],
            Methods = [enviar, confirmar]
        };

        var email = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Email",
            Stereotype = null,
            Parents = [notificacion],
            Attributes = [],
            Methods = [enviarEmail]
        };

        var sms = new Class
        {
            Id = Guid.NewGuid(),
            Name = "SMS",
            Stereotype = null,
            Parents = [notificacion],
            Attributes = [],
            Methods = [enviarSMS]
        };

        var otroAviso = new LocalVariable()
        {
            Id = Guid.NewGuid(),
            Name = "otroAviso",
            ReferenceType = email
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
                    ReferenceType = notificacion
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
            Parents = null,
            Attributes = [],
            Methods = [notificar]
        };

        var notificacionType = new TypeDTO { Id = Guid.NewGuid(), Name = "Notificacion" };

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([notificador, sms, email, notificacion]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([notificador, sms, email, notificacion]);
        _mockMethodRepository.Setup(repo => repo.FindAll()).Returns([notificar, enviar]);
        _mockMethodRepository.Setup(repo => repo.Find(confirmarInvoked1.MethodId)).Returns(confirmar);
        _mockMethodRepository.Setup(repo => repo.Find(confirmarInvoked2.MethodId)).Returns(confirmar);
        _mockMethodRepository.Setup(repo => repo.Find(enviarInvoked1.MethodId)).Returns(enviar);
        _mockMethodRepository.Setup(repo => repo.Find(enviarInvoked2.MethodId)).Returns(enviarEmail);

        var result = _methodService.ExecuteMethod(notificador.Id, notificador.Id, notificar.Id, [notificacionType]);
        var result2 = _methodService.ExecuteMethod(notificacion.Id, sms.Id, enviar.Id, []);
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
            Parents = null,
            Attributes = [],
            Methods = [esValidoEntrada, inicializar, limpiar]
        };

        var entradaTexto = new Class
        {
            Id = Guid.NewGuid(),
            Name = "EntradaTexto",
            Stereotype = null,
            Parents = [entrada],
            Attributes = [],
            Methods = [esValidoEntradaTexto]
        };

        var entradaTextoEspecial = new Class
        {
            Id = Guid.NewGuid(),
            Name = "EntradaTextoEspecial",
            Stereotype = KeyWord.Sealed,
            Parents = [entradaTexto],
            Attributes = [],
            Methods = [esValidoEntradaTextoEspecial]
        };

        var input = new LocalVariable
        {
            Id = Guid.NewGuid(),
            Name = "input",
            ReferenceType = entradaTextoEspecial
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
            Parents = null,
            Attributes = [],
            Methods = [validar]
        };

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([validador, entrada, entradaTexto, entradaTextoEspecial]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([validador, entrada, entradaTexto, entradaTextoEspecial]);
        _mockMethodRepository.Setup(repo => repo.FindAll()).Returns([validar]);
        _mockMethodRepository.Setup(repo => repo.Find(esValidoEntradaTextoEspecialInvoked.MethodId)).Returns(esValidoEntradaTextoEspecial);
        _mockMethodRepository.Setup(repo => repo.Find(esValidoEntradaTextoInvoked.MethodId)).Returns(esValidoEntradaTexto);
        _mockMethodRepository.Setup(repo => repo.Find(esValidoEntradaInvoked.MethodId)).Returns(esValidoEntrada);
        _mockMethodRepository.Setup(repo => repo.Find(inicializarInvoked.MethodId)).Returns(inicializar);
        _mockMethodRepository.Setup(repo => repo.Find(limpiarInvoked.MethodId)).Returns(limpiar);

        var result = _methodService.ExecuteMethod(validador.Id, validador.Id, validar.Id, []);

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
            ReferenceType = new Primitive(PrimitiveType.String),
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
            ReferenceType = new Primitive(PrimitiveType.String),
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
            Parents = null,
            Attributes = [],
            Methods = [registrar],
        };

        var loggerLocal = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "loggerLocal",
            Accessibility = Accessibility.Private,
            ReferenceType = logger
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
            Parents = null,
            Attributes = [nombre],
            Methods = [validar],
        };

        var usuarioParam = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "usuario",
            ReferenceType = usuario
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
            ReferenceType = new Primitive(PrimitiveType.String),
            InvokedMethods = [toStringInvoked]
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
            Parents = [autenticador]
        };

        var usuarioType = new TypeDTO
        {
            Id = Guid.NewGuid(),
            Name = "Usuario",
        };

        var stringPrimitive = new Primitive(PrimitiveType.String);

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([usuario, logger, autenticador, autenticadorPassword]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([usuario, logger, autenticador, autenticadorPassword, stringPrimitive]);
        _mockMethodRepository.Setup(repo => repo.FindAll()).Returns([autenticar]);
        _mockMethodRepository.Setup(repo => repo.Find(prepararInvoked.MethodId)).Returns(preparar);
        _mockMethodRepository.Setup(repo => repo.Find(validarInvoked.MethodId)).Returns(validar);
        _mockMethodRepository.Setup(repo => repo.Find(registrarInvoked.MethodId)).Returns(registrar);
        _mockMethodRepository.Setup(repo => repo.Find(validarCredencialesAutenticatorInvoked.MethodId)).Returns(validarCredencialesAutenticator);
        _mockMethodRepository.Setup(repo => repo.Find(logAccesoInvoked.MethodId)).Returns(logAcceso);
        _mockMethodRepository.Setup(repo => repo.Find(toStringInvoked.MethodId)).Returns(toString);

        var result = _methodService.ExecuteMethod(autenticador.Id, autenticadorPassword.Id, autenticar.Id, [usuarioType]);

        var res =
            "Autenticador.Autenticar(Usuario) -> Autenticador.Autenticar(Usuario)\n" +
            "\tthis.Preparar() -> Autenticador.Preparar()\n" +
            "\tthis.ValidarCredenciales(Usuario) -> AutenticadorPassword.ValidarCredenciales(Usuario)\n" +
            "\t\tusuario.Validar() -> Usuario.Validar()\n" +
            "\t\tthis.clavePrivada.ToString() -> String.ToString()\n" +
            "\tthis.LogAcceso(Usuario) -> Autenticador.LogAcceso(Usuario)\n" +
            "\tloggerLocal.Registrar(string) -> Logger.Registrar(string)";
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
            ReferenceType = new Primitive(PrimitiveType.Int)
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
            Parents = null,
        };

        var mensaje = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "mensaje",
            ReferenceType = new Primitive(PrimitiveType.String)
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
            Parents = null,
            Methods = [enviarNotificacion],
        };

        var notificador = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "notificador",
            ReferenceType = comunicacion,
            Accessibility = Accessibility.Protected,
        };

        var clienteParam = new Parameter { Id = Guid.NewGuid(), Name = "cliente", ReferenceType = cliente };

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
            Parents = null,
        };

        var inventarioDisponible = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "inventarioDisponible",
            ReferenceType = new Primitive(PrimitiveType.Bool),
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
            Parents = [proceso],
        };

        var clienteType = new TypeDTO
        {
            Id = Guid.NewGuid(),
            Name = "Cliente",
        };

        var stringPrimitive = new Primitive(PrimitiveType.String);

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([cliente, comunicacion, proceso, procesoCompra]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([cliente, comunicacion, proceso, procesoCompra, stringPrimitive]);
        _mockMethodRepository.Setup(repo => repo.FindAll()).Returns([ejecutar]);
        _mockMethodRepository.Setup(repo => repo.Find(validarInvoked.MethodId)).Returns(validar);
        _mockMethodRepository.Setup(repo => repo.Find(comprobarInventarioInvoked.MethodId)).Returns(comprobarInventario);
        _mockMethodRepository.Setup(repo => repo.Find(enviarNotificacionInvoked.MethodId)).Returns(enviarNotificacion);
        _mockMethodRepository.Setup(repo => repo.Find(validarIdentidadInvoked.MethodId)).Returns(validarIdentidad);
        _mockMethodRepository.Setup(repo => repo.Find(realizarProcesoProcesoInvoked.MethodId)).Returns(realizarProcesoProceso);

        var result = _methodService.ExecuteMethod(proceso.Id, procesoCompra.Id, ejecutar.Id, [clienteType]);

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

    [TestMethod]
    public void CreateMethodSignature_ShouldAddMethodSignature_WhenMethodSignatureIsValid()
    {
        var classT = new TypeDTO { Name = "TestClass", Kind = "class" };

        var methodSignatureDto = new MethodSignatureDTO { Name = "Signature", ReturnType = classT };

        _mockMethodSignatureRepository
            .Setup(r => r.Add(It.Is<MethodSignature>(s =>
                s.Name == methodSignatureDto.Name && s.ReturnType.Name == classT.Name)));

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = "TestClass" }]);

        var result = _methodService.CreateMethodSignature(methodSignatureDto);

        Assert.AreNotEqual(Guid.Empty, result);
        _mockMethodSignatureRepository.Verify(r => r.Add(It.IsAny<MethodSignature>()), Times.Once);
    }

    [TestMethod]
    public void CreateMethodSignature_ShouldThrowException_WhenMethodSignatureIsNotValid()
    {
        var exception =
            Assert.ThrowsException<MethodSignatureNotFoundException>(() => _methodService.CreateMethodSignature(null));

        Assert.AreEqual("MethodSignature cannot be null.", exception.Message);
    }

    [TestMethod]
    public void CreateMethodSignature_ShouldThrowException_WhenMethodSignatureNameIsNotValid()
    {
        var exception =
            Assert.ThrowsException<InvalidMethodSignatureException>(() => _methodService.CreateMethodSignature(new MethodSignatureDTO()));

        Assert.AreEqual("MethodSignature name cannot be null.", exception.Message);
    }

    [TestMethod]
    public void GetInvokedMethodById_ShouldReturnAllInvokedMethodsAsDTOs()
    {
        var invokedMethods = new List<InvokedMethod>
        {
            new InvokedMethod { Id = Guid.NewGuid(), ReferenceName = "ref1", MethodId = Guid.NewGuid() },
            new InvokedMethod { Id = Guid.NewGuid(), ReferenceName = "ref2", MethodId = Guid.NewGuid() }
        };

        _mockInvokedMethodRepository.Setup(r => r.FindAll()).Returns(invokedMethods);

        var result = _methodService.GetInvokedMethodById();

        Assert.AreEqual(invokedMethods.Count, result.Count);
        for(var i = 0; i < invokedMethods.Count; i++)
        {
            Assert.AreEqual(invokedMethods[i].Id.ToString(), result[i].Id);
            Assert.AreEqual(invokedMethods[i].ReferenceName, result[i].ReferenceName);
            Assert.AreEqual(invokedMethods[i].MethodId, result[i].MethodId);
        }
    }

    [TestMethod]
    public void GetMethodSignatures_ShouldReturnListOfMethodSignatureDTOs_WhenSignaturesExist()
    {
        var methodSignatures = new List<MethodSignature>
        {
            new MethodSignature
            {
                Id = Guid.NewGuid(),
                Name = "Signature1",
                ReturnType = new Primitive(PrimitiveType.Int)
            },
            new MethodSignature
            {
                Id = Guid.NewGuid(),
                Name = "Signature2",
                ReturnType = new Primitive(PrimitiveType.String)
            }
        };

        _mockMethodSignatureRepository.Setup(r => r.FindAll()).Returns(methodSignatures);

        var result = _methodService.GetMethodSignatures();

        Assert.AreEqual(methodSignatures.Count, result.Count);
        for(var i = 0; i < methodSignatures.Count; i++)
        {
            Assert.AreEqual(methodSignatures[i].Id.ToString(), result[i].Id);
            Assert.AreEqual(methodSignatures[i].Name, result[i].Name);
            Assert.IsNotNull(result[i].ReturnType);
            Assert.AreEqual(methodSignatures[i].ReturnType.Name, result[i].ReturnType.Name);
        }
    }

    [TestMethod]
    public void ExecuteMethodExample6_ShouldReturnMethodExecuted_WhenValidInputProvided()
    {
        var nombre = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Nombre",
            Accessibility = Accessibility.Public,
            ReferenceType = new Primitive(PrimitiveType.String),
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
            ReferenceType = new Primitive(PrimitiveType.String),
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
            Parents = null,
            Attributes = [],
            Methods = [registrar],
        };

        var loggerLocal = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "loggerLocal",
            Accessibility = Accessibility.Private,
            ReferenceType = logger
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

        var usuarioInterface = new Interface
        {
            Id = Guid.NewGuid(),
            Name = "IUsuario",
            MethodSignatures =
            [
                new MethodSignature("Validar", new Primitive(PrimitiveType.Bool))
            ]
        };

        var usuario = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Usuario",
            Stereotype = null,
            Parents = [usuarioInterface],
            Attributes = [nombre],
            Methods = [validar],
        };

        var usuarioParamForFirst = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "usuario",
            ReferenceType = usuarioInterface
        };

        var usuarioParamForOthers = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "usuario",
            ReferenceType = usuario
        };

        var validarCredencialesAutenticator = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ValidarCredenciales",
            Accessibility = Accessibility.Protected,
            Methods = [],
            Parameters = [usuarioParamForOthers],
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
            Parameters = [usuarioParamForOthers],
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
            Parameters = [usuarioParamForFirst],
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
            ReferenceType = new Primitive(PrimitiveType.String),
            InvokedMethods = [toStringInvoked]
        };

        var validarCredencialesAP = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ValidarCredenciales",
            Accessibility = Accessibility.Protected,
            Methods = [validarInvoked, toStringInvoked],
            Parameters = [usuarioParamForOthers],
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
            Parents = [autenticador]
        };

        var usuarioType = new TypeDTO
        {
            Id = Guid.NewGuid(),
            Name = "Usuario",
        };

        var stringPrimitive = new Primitive(PrimitiveType.String);

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([usuario, logger, autenticador, autenticadorPassword]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([usuario, logger, autenticador, autenticadorPassword, stringPrimitive, usuarioInterface]);
        _mockMethodRepository.Setup(repo => repo.FindAll()).Returns([autenticar]);
        _mockMethodRepository.Setup(repo => repo.Find(prepararInvoked.MethodId)).Returns(preparar);
        _mockMethodRepository.Setup(repo => repo.Find(validarInvoked.MethodId)).Returns(validar);
        _mockMethodRepository.Setup(repo => repo.Find(registrarInvoked.MethodId)).Returns(registrar);
        _mockMethodRepository.Setup(repo => repo.Find(validarCredencialesAutenticatorInvoked.MethodId)).Returns(validarCredencialesAutenticator);
        _mockMethodRepository.Setup(repo => repo.Find(logAccesoInvoked.MethodId)).Returns(logAcceso);
        _mockMethodRepository.Setup(repo => repo.Find(toStringInvoked.MethodId)).Returns(toString);

        var result = _methodService.ExecuteMethod(autenticador.Id, autenticadorPassword.Id, autenticar.Id, [usuarioType]);

        var res =
            "Autenticador.Autenticar(IUsuario) -> Autenticador.Autenticar(Usuario)\n" +
            "\tthis.Preparar() -> Autenticador.Preparar()\n" +
            "\tthis.ValidarCredenciales(Usuario) -> AutenticadorPassword.ValidarCredenciales(Usuario)\n" +
            "\t\tusuario.Validar() -> Usuario.Validar()\n" +
            "\t\tthis.clavePrivada.ToString() -> String.ToString()\n" +
            "\tthis.LogAcceso(Usuario) -> Autenticador.LogAcceso(Usuario)\n" +
            "\tloggerLocal.Registrar(string) -> Logger.Registrar(string)";
        Console.WriteLine(result);
        Assert.AreEqual(res, result);
    }

    [TestMethod]
    public void ExecuteMethodExample7_ShouldReturnMethodExecuted_WhenValidInputProvided()
    {
        var nombre = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Nombre",
            Accessibility = Accessibility.Public,
            ReferenceType = new Primitive(PrimitiveType.String),
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
            ReferenceType = new Primitive(PrimitiveType.String),
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
            Parents = null,
            Attributes = [],
            Methods = [registrar],
        };

        var loggerLocal = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "loggerLocal",
            Accessibility = Accessibility.Private,
            ReferenceType = logger
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

        var persona = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Persona"
        };

        var usuario = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Usuario",
            Stereotype = null,
            Parents = [persona],
            Attributes = [nombre],
            Methods = [validar],
        };

        var usuarioParamForFirst = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "usuario",
            ReferenceType = persona
        };

        var usuarioParamForOthers = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "usuario",
            ReferenceType = usuario
        };

        var validarCredencialesAutenticator = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ValidarCredenciales",
            Accessibility = Accessibility.Protected,
            Methods = [],
            Parameters = [usuarioParamForOthers],
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
            Parameters = [usuarioParamForOthers],
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
            Parameters = [usuarioParamForFirst],
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
            ReferenceType = new Primitive(PrimitiveType.String),
            InvokedMethods = [toStringInvoked]
        };

        var validarCredencialesAP = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ValidarCredenciales",
            Accessibility = Accessibility.Protected,
            Methods = [validarInvoked, toStringInvoked],
            Parameters = [usuarioParamForOthers],
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
            Parents = [autenticador]
        };

        var usuarioType = new TypeDTO
        {
            Id = Guid.NewGuid(),
            Name = "Usuario",
        };

        var stringPrimitive = new Primitive(PrimitiveType.String);

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([usuario, logger, autenticador, autenticadorPassword, persona]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([usuario, logger, autenticador, autenticadorPassword, stringPrimitive, persona]);
        _mockMethodRepository.Setup(repo => repo.FindAll()).Returns([autenticar]);
        _mockMethodRepository.Setup(repo => repo.Find(prepararInvoked.MethodId)).Returns(preparar);
        _mockMethodRepository.Setup(repo => repo.Find(validarInvoked.MethodId)).Returns(validar);
        _mockMethodRepository.Setup(repo => repo.Find(registrarInvoked.MethodId)).Returns(registrar);
        _mockMethodRepository.Setup(repo => repo.Find(validarCredencialesAutenticatorInvoked.MethodId)).Returns(validarCredencialesAutenticator);
        _mockMethodRepository.Setup(repo => repo.Find(logAccesoInvoked.MethodId)).Returns(logAcceso);
        _mockMethodRepository.Setup(repo => repo.Find(toStringInvoked.MethodId)).Returns(toString);

        var result = _methodService.ExecuteMethod(autenticador.Id, autenticadorPassword.Id, autenticar.Id, [usuarioType]);

        var res =
            "Autenticador.Autenticar(Persona) -> Autenticador.Autenticar(Usuario)\n" +
            "\tthis.Preparar() -> Autenticador.Preparar()\n" +
            "\tthis.ValidarCredenciales(Usuario) -> AutenticadorPassword.ValidarCredenciales(Usuario)\n" +
            "\t\tusuario.Validar() -> Usuario.Validar()\n" +
            "\t\tthis.clavePrivada.ToString() -> String.ToString()\n" +
            "\tthis.LogAcceso(Usuario) -> Autenticador.LogAcceso(Usuario)\n" +
            "\tloggerLocal.Registrar(string) -> Logger.Registrar(string)";
        Console.WriteLine(result);
        Assert.AreEqual(res, result);
    }

    [TestMethod]
    public void ExecuteMethodExample8_ShouldReturnMethodExecuted_WhenValidInputProvided()
    {
        var nombre = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Nombre",
            Accessibility = Accessibility.Public,
            ReferenceType = new Primitive(PrimitiveType.String),
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
            ReferenceType = new Primitive(PrimitiveType.String),
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
            Parents = null,
            Attributes = [],
            Methods = [registrar],
        };

        var loggerLocal = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "loggerLocal",
            Accessibility = Accessibility.Private,
            ReferenceType = logger
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

        var usuario = new Primitive
        {
            Id = Guid.NewGuid(),
            Name = "Usuario",
            Methods = [validar],
            PrimitiveType = PrimitiveType.Bool
        };

        var usuarioParamForFirst = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "usuario",
            ReferenceType = usuario
        };

        var usuarioParamForOthers = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "usuario",
            ReferenceType = usuario
        };

        var validarCredencialesAutenticator = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ValidarCredenciales",
            Accessibility = Accessibility.Protected,
            Methods = [],
            Parameters = [usuarioParamForOthers],
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
            Parameters = [usuarioParamForOthers],
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
            Parameters = [usuarioParamForFirst],
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
            ReferenceType = new Primitive(PrimitiveType.String),
            InvokedMethods = [toStringInvoked]
        };

        var validarCredencialesAP = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ValidarCredenciales",
            Accessibility = Accessibility.Protected,
            Methods = [validarInvoked, toStringInvoked],
            Parameters = [usuarioParamForOthers],
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
            Parents = [autenticador]
        };

        var usuarioType = new TypeDTO
        {
            Id = Guid.NewGuid(),
            Name = "Usuario",
        };

        var stringPrimitive = new Primitive(PrimitiveType.String);

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([logger, autenticador, autenticadorPassword]);
        _mockClassRepository.Setup(repo => repo.FindAllTypes()).Returns([usuario, logger, autenticador, autenticadorPassword, stringPrimitive]);
        _mockMethodRepository.Setup(repo => repo.FindAll()).Returns([autenticar]);
        _mockMethodRepository.Setup(repo => repo.Find(prepararInvoked.MethodId)).Returns(preparar);
        _mockMethodRepository.Setup(repo => repo.Find(validarInvoked.MethodId)).Returns(validar);
        _mockMethodRepository.Setup(repo => repo.Find(registrarInvoked.MethodId)).Returns(registrar);
        _mockMethodRepository.Setup(repo => repo.Find(validarCredencialesAutenticatorInvoked.MethodId)).Returns(validarCredencialesAutenticator);
        _mockMethodRepository.Setup(repo => repo.Find(logAccesoInvoked.MethodId)).Returns(logAcceso);
        _mockMethodRepository.Setup(repo => repo.Find(toStringInvoked.MethodId)).Returns(toString);

        var result = _methodService.ExecuteMethod(autenticador.Id, autenticadorPassword.Id, autenticar.Id, [usuarioType]);

        var res =
            "Autenticador.Autenticar(bool) -> Autenticador.Autenticar(bool)\n" +
            "\tthis.Preparar() -> Autenticador.Preparar()\n" +
            "\tthis.ValidarCredenciales(bool) -> AutenticadorPassword.ValidarCredenciales(bool)\n" +
            "\t\tthis.clavePrivada.ToString() -> String.ToString()\n" +
            "\tthis.LogAcceso(bool) -> Autenticador.LogAcceso(bool)\n" +
            "\tloggerLocal.Registrar(string) -> Logger.Registrar(string)";
        Console.WriteLine(result);
        Assert.AreEqual(res, result);
    }
}
