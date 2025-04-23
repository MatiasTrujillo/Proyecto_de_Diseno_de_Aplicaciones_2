using Moq;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class MethodServiceTest
{
    private Mock<IMethodRepository> _mockMethodRepository = null!;
    private Mock<IClassRepository> _mockClassRepository = null!;
    private MethodService _methodService = null!;
    private MethodDTO methodDto = new MethodDTO();
    private MethodDTO otherMethodDto = new MethodDTO();

    private readonly Method _method = new Method();

    [TestInitialize]
    public void Initialize()
    {
        methodDto = new MethodDTO()
        {
            Id = Guid.NewGuid(),
            Name = "First Method Dto",
            Accessibility = Accessibility.Private,
            Stereotype = null
        };
        otherMethodDto = new MethodDTO()
        {
            Id = Guid.NewGuid(),
            Name = "Second Method Dto",
            Accessibility = Accessibility.Public,
            Stereotype = KeyWord.Abstract
        };
        _mockMethodRepository = new Mock<IMethodRepository>(MockBehavior.Strict);
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);
        _mockMethodRepository
            .Setup(repo => repo.Add(It.IsAny<Method>()));
        _mockMethodRepository
            .Setup(repo => repo.FindAll())
            .Returns([]);
        _methodService = new MethodService(_mockMethodRepository.Object, _mockClassRepository.Object);
    }

    [TestMethod]
    public void CreateMethod_ShouldCallRepositoryAdd_WithValidMethod()
    {
        _methodService.CreateMethod(methodDto);

        _mockMethodRepository.Verify(repo => repo.Add(
            It.Is<Method>(m =>
                m.Name.Equals(methodDto.Name) &&
                m.Accessibility == methodDto.Accessibility &&
                m.Stereotype == null)), Times.Once);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowException_WhenMethodIsNull()
    {
        MethodDTO nullMethodDto = null!;

        var exception = Assert.ThrowsException<Exception>(() => _methodService.CreateMethod(nullMethodDto));
        Assert.AreEqual("Method cannot be null.", exception.Message);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowException_WhenMethodNameAlreadyExists()
    {
        var existingMethod = new Method
        {
            Id = Guid.NewGuid(),
            Name = methodDto.Name,
            Accessibility = Accessibility.Private,
            Stereotype = null
        };

        _mockMethodRepository
            .Setup(repo => repo.FindAll())
            .Returns([existingMethod]);

        var exception = Assert.ThrowsException<Exception>(() => _methodService.CreateMethod(methodDto));
        Assert.AreEqual("Method with the same name  already exists.", exception.Message);
    }

    [TestMethod]
    public void AssociateMethodWithClass_ShouldThrowException_WhenMethodNotFound()
    {
        _mockMethodRepository.Setup(repo => repo.Find(It.IsAny<Guid>())).Returns((Method)null!);

        var exception = Assert.ThrowsException<Exception>(() =>
            _methodService.AssociateMethodWithClass(Guid.NewGuid(), Guid.NewGuid()));
        Assert.AreEqual("Method null.", exception.Message);
    }

    [TestMethod]
    public void AssociateMethodWithClass_ShouldThrowException_WhenClassNotFound()
    {
        _mockMethodRepository.Setup(repo => repo.Find(It.IsAny<Guid>())).Returns(new Method());
        _mockClassRepository.Setup(repo => repo.Find(It.IsAny<Guid>())).Returns((Class)null!);

        var exception = Assert.ThrowsException<Exception>(() =>
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
        var classT = new Class
        {
            Id = Guid.NewGuid(),
            Name = "TestClass",
            Methods = [existingMethod]
        };

        _mockMethodRepository.Setup(repo => repo.Find(method.Id)).Returns(method);
        _mockClassRepository.Setup(repo => repo.Find(classT.Id)).Returns(classT);

        var exception = Assert.ThrowsException<Exception>(() =>
            _methodService.AssociateMethodWithClass(method.Id, classT.Id));
        Assert.AreEqual("A method with the same name already exists in the class.", exception.Message);
    }

    [TestMethod]
    public void AddAMethod_ShouldAddMethod_WhenValidInputsProvided()
    {
        var methodId = Guid.NewGuid();
        var methodToAddId = Guid.NewGuid();
        var method = new Method { Id = methodId, Name = "ParentMethod", Methods = [] };
        var methodToAdd = new Method { Id = methodToAddId, Name = "ChildMethod" };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(repo => repo.Find(methodToAddId)).Returns(methodToAdd);
        _mockMethodRepository.Setup(repo => repo.Update(It.IsAny<Method>()));

        _methodService.AddAMethod(methodId, methodToAddId);

        Assert.IsTrue(method.Methods.Contains(methodToAdd));
        _mockMethodRepository.Verify(repo => repo.Update(method), Times.Once);
    }

    [TestMethod]
    public void AddAMethod_ShouldThrowException_WhenMethodIsAlreadyAssociated()
    {
        var methodId = Guid.NewGuid();
        var methodToAddId = Guid.NewGuid();
        var methodToAdd = new Method { Id = methodToAddId, Name = "ChildMethod" };
        var method = new Method
        {
            Id = methodId,
            Name = "ParentMethod",
            Methods = [methodToAdd]
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(repo => repo.Find(methodToAddId)).Returns(methodToAdd);

        var exception = Assert.ThrowsException<Exception>(() =>
            _methodService.AddAMethod(methodId, methodToAddId));
        Assert.AreEqual("Method is already associated with this method.", exception.Message);
    }

    [TestMethod]
    public void ValidateMethodIsNotInUse_ShouldThrowException_WhenMethodIsAssociatedWithOtherMethods()
    {
        var methodId = Guid.NewGuid();
        var associatedMethod = new Method { Id = Guid.NewGuid(), Name = "AssociatedMethod" };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Methods = [associatedMethod]
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([]);

        var exception = Assert.ThrowsException<Exception>(() =>
            _methodService.DeleteMethod(methodId));
        Assert.AreEqual("Method is associated with another method.", exception.Message);
    }

    [TestMethod]
    public void AddLocalVariableToMethod_ShouldAddLocalVariable_WhenValidInputsProvided()
    {
        var methodId = Guid.NewGuid();
        var localVariable = new LocalVariableDTO { Name = "TestVariable" };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            LocalVariables = [],
            Parameters = []
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(repo => repo.Update(It.IsAny<Method>()));

        _methodService.AddLocalVariableToMethod(methodId, localVariable);

        Assert.IsTrue(method.LocalVariables.Any(v => v.Name == localVariable.Name));
        _mockMethodRepository.Verify(repo => repo.Update(method), Times.Once);
    }

    [TestMethod]
    public void AddLocalVariableToMethod_ShouldThrowException_WhenLocalVariableNameConflictsWithExistingVariable()
    {
        var methodId = Guid.NewGuid();
        var localVariable = new LocalVariableDTO { Name = "TestVariable" };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            LocalVariables = [new LocalVariable { Name = "TestVariable" }],
            Parameters = []
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);

        var exception = Assert.ThrowsException<Exception>(() =>
            _methodService.AddLocalVariableToMethod(methodId, localVariable));
        Assert.AreEqual("A variable or parameter with the same name already exists.", exception.Message);
    }

    [TestMethod]
    public void AddLocalVariableToMethod_ShouldThrowException_WhenLocalVariableNameConflictsWithExistingParameter()
    {
        var methodId = Guid.NewGuid();
        var localVariable = new LocalVariableDTO { Name = "TestParameter" };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            LocalVariables = [],
            Parameters = [new Parameter { Name = "TestParameter" }]
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);

        var exception = Assert.ThrowsException<Exception>(() =>
            _methodService.AddLocalVariableToMethod(methodId, localVariable));
        Assert.AreEqual("A variable or parameter with the same name already exists.", exception.Message);
    }

    [TestMethod]
    public void AddParameterToMethod_ShouldThrowException_WhenMethodNotFound()
    {
        var methodId = Guid.NewGuid();
        var parameter = new ParameterDTO { Name = "TestParameter" };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns((Method)null!);

        var exception = Assert.ThrowsException<Exception>(() =>
            _methodService.AddParameterToMethod(methodId, parameter));
        Assert.AreEqual("Method null.", exception.Message);
    }

    [TestMethod]
    public void AddParameterToMethod_ShouldThrowException_WhenParameterNameConflictsWithExistingLocalVariable()
    {
        var methodId = Guid.NewGuid();
        var parameter = new ParameterDTO { Name = "TestVariable" };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            LocalVariables = [new LocalVariable { Name = "TestVariable" }],
            Parameters = []
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);

        var exception = Assert.ThrowsException<Exception>(() =>
            _methodService.AddParameterToMethod(methodId, parameter));
        Assert.AreEqual("A variable or parameter with the same name already exists.", exception.Message);
    }

    [TestMethod]
    public void AddParameterToMethod_ShouldThrowException_WhenParameterNameConflictsWithExistingParameter()
    {
        var methodId = Guid.NewGuid();
        var parameter = new ParameterDTO { Name = "TestParameter" };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            LocalVariables = [],
            Parameters = [new Parameter { Name = "TestParameter" }]
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);

        var exception = Assert.ThrowsException<Exception>(() =>
            _methodService.AddParameterToMethod(methodId, parameter));
        Assert.AreEqual("A variable or parameter with the same name already exists.", exception.Message);
    }

    [TestMethod]
    public void AddParameterToMethod_ShouldAddParameter_WhenValidInputsProvided()
    {
        var methodId = Guid.NewGuid();
        var parameter = new ParameterDTO { Name = "TestParameter" };
        var method = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            LocalVariables = [],
            Parameters = []
        };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(repo => repo.Update(It.IsAny<Method>()));

        _methodService.AddParameterToMethod(methodId, parameter);

        Assert.IsTrue(method.Parameters.Any(p => p.Name == parameter.Name));
        _mockMethodRepository.Verify(repo => repo.Update(method), Times.Once);
    }

    [TestMethod]
    public void ExecuteMethodExample1_SholddReturnMethodExecuted()
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

        var iniciarViaje = new Method
        {
            Id = Guid.NewGuid(),
            Name = "IniciarViaje",
            Accessibility = Accessibility.Public,
            Methods = [encender],
            Parameters = [],
            LocalVariables = [],
            Stereotype = null
        };

        iniciarViaje.Methods = [encender, iniciarViaje];

        var detenerViaje = new Method
        {
            Id = Guid.NewGuid(),
            Name = "DetenerViaje",
            Accessibility = Accessibility.Public,
            Methods = [apagar],
            Parameters = [],
            LocalVariables = [],
            Stereotype = null
        };

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

        var auto = new Class()
        {
            Id = Guid.NewGuid(),
            Name = "Auto",
            Stereotype = null,
            HierarchyClass = null,
            Attributes = [motorAuto],
            Methods = [iniciarViaje, detenerViaje]
        };

        var result = _method.ExecuteMethod(auto, auto, iniciarViaje);
        Console.WriteLine(result);
        var result2 =
            "Auto.IniciarViaje() -> Auto.IniciarViaje()\n" +
            "motorAuto.Encender() -> Motor.Encender()\n" +
            "this.IniciarViaje() -> Auto.IniciarViaje()";
        Assert.AreEqual(result, result2);
    }

    [TestMethod]
    public void ExecuteMethodExample2_SholddReturnMethodExecuted()
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
            Methods = [enviar]
        };

        var sms = new Class
        {
            Id = id,
            Name = "SMS",
            Stereotype = null,
            HierarchyClass = notificacion,
            Attributes = [],
            Methods = [enviar]
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
            Methods = [enviar, confirmar],
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

        var result = _method.ExecuteMethod(notificador, notificador, notificar);
        var result2 = _method.ExecuteMethod(notificacion, sms, enviar);
        Console.WriteLine(result2);
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(result);
        Assert.AreEqual(result2, "Notificacion.Enviar() -> SMS.Enviar()");
        var res =
            "Notificador.Notificar() -> Notificador.Notificar()\n" +
            "aviso.Enviar() -> Notificacion.Enviar()\n" +
            "aviso.Confirmar() -> Notificacion.Confirmar()\n" +
            "otroAviso.Enviar() -> Email.Enviar()\n" +
            "otroAviso.Confirmar() -> Notificacion.Confirmar()";
        Assert.AreEqual(res, result);
    }
}
