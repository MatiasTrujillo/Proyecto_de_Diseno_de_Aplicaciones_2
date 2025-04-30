using Moq;
using Simulator.Domain;
using Simulator.Domain.Exception;
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
    private MethodDTO _methodDto = new MethodDTO();
    private MethodDTO _otherMethodDto = new MethodDTO();

    [TestInitialize]
    public void Initialize()
    {
        _methodDto = new MethodDTO()
        {
            Name = "First Method Dto",
            Accessibility = Accessibility.Private,
            Stereotype = null
        };
        _otherMethodDto = new MethodDTO()
        {
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
        _methodService.CreateMethod(_methodDto);

        _mockMethodRepository.Verify(repo => repo.Add(
            It.Is<Method>(m =>
                m.Name.Equals(_methodDto.Name) &&
                m.Accessibility == _methodDto.Accessibility &&
                m.Stereotype == null)), Times.Once);
    }

    [TestMethod]
    public void CreateMethod_ShouldThrowException_WhenMethodIsNull()
    {
        MethodDTO nullMethodDto = null!;

        var exception = Assert.ThrowsException<MethodException>(() => _methodService.CreateMethod(nullMethodDto));
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
            Stereotype = null
        };

        _mockMethodRepository
            .Setup(repo => repo.FindAll())
            .Returns([existingMethod]);

        var exception = Assert.ThrowsException<MethodException>(() => _methodService.CreateMethod(_methodDto));
        Assert.AreEqual("Method with the same name  already exists.", exception.Message);
    }

    [TestMethod]
    public void AssociateMethodWithClass_ShouldThrowException_WhenMethodNotFound()
    {
        _mockMethodRepository.Setup(repo => repo.Find(It.IsAny<Guid>())).Returns((Method)null!);

        var exception = Assert.ThrowsException<MethodException>(() =>
            _methodService.AssociateMethodWithClass(Guid.NewGuid(), Guid.NewGuid()));
        Assert.AreEqual("Method null.", exception.Message);
    }

    [TestMethod]
    public void AssociateMethodWithClass_ShouldThrowException_WhenClassNotFound()
    {
        _mockMethodRepository.Setup(repo => repo.Find(It.IsAny<Guid>())).Returns(new Method());
        _mockClassRepository.Setup(repo => repo.Find(It.IsAny<Guid>())).Returns((Class)null!);

        var exception = Assert.ThrowsException<MethodException>(() =>
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

        var exception = Assert.ThrowsException<MethodException>(() =>
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
        var method = new Method { Id = methodId, Name = "ParentMethod", Methods = [methodToAdd] };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockMethodRepository.Setup(repo => repo.Find(methodToAddId)).Returns(methodToAdd);

        var exception = Assert.ThrowsException<MethodException>(() =>
            _methodService.AddAMethod(methodId, methodToAddId));
        Assert.AreEqual("Method is already associated with this method.", exception.Message);
    }

    [TestMethod]
    public void ValidateMethodIsNotInUse_ShouldThrowException_WhenMethodIsAssociatedWithOtherMethods()
    {
        var methodId = Guid.NewGuid();
        var associatedMethod = new Method { Id = Guid.NewGuid(), Name = "AssociatedMethod" };
        var method = new Method { Id = methodId, Name = "TestMethod", Methods = [associatedMethod] };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns(method);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([]);

        var exception = Assert.ThrowsException<MethodException>(() =>
            _methodService.DeleteMethod(methodId));
        Assert.AreEqual("Method is associated with another method.", exception.Message);
    }

    [TestMethod]
    public void AddLocalVariableToMethod_ShouldAddLocalVariable_WhenValidInputsProvided()
    {
        var methodId = Guid.NewGuid();
        var localVariable = new LocalVariableDTO { Name = "TestVariable" };
        var method = new Method { Id = methodId, Name = "TestMethod", LocalVariables = [], Parameters = [] };

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

        var exception = Assert.ThrowsException<MethodException>(() =>
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

        var exception = Assert.ThrowsException<MethodException>(() =>
            _methodService.AddLocalVariableToMethod(methodId, localVariable));
        Assert.AreEqual("A variable or parameter with the same name already exists.", exception.Message);
    }

    [TestMethod]
    public void AddParameterToMethod_ShouldThrowException_WhenMethodNotFound()
    {
        var methodId = Guid.NewGuid();
        var parameter = new ParameterDTO { Name = "TestParameter" };

        _mockMethodRepository.Setup(repo => repo.Find(methodId)).Returns((Method)null!);

        // Comprobamos si se lanza la excepción esperada
        var exception = Assert.ThrowsException<MethodException>(() =>
            _methodService.AddParameterToMethod(methodId, parameter));

        // Verificamos que el mensaje de la excepción sea el correcto
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

        var exception = Assert.ThrowsException<MethodException>(() =>
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

        var exception = Assert.ThrowsException<MethodException>(() =>
            _methodService.AddParameterToMethod(methodId, parameter));
        Assert.AreEqual("A variable or parameter with the same name already exists.", exception.Message);
    }

    [TestMethod]
    public void AddParameterToMethod_ShouldAddParameter_WhenValidInputsProvided()
    {
        var methodId = Guid.NewGuid();
        var parameter = new ParameterDTO { Name = "TestParameter" };  // Nombre del parámetro es válido
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

        var result = _methodService.ExecuteMethod(auto, auto, iniciarViaje);
        Console.WriteLine(result);
        var result2 =
            "Auto.IniciarViaje() -> Auto.IniciarViaje()\n" +
            "\tmotorAuto.Encender() -> Motor.Encender()\n" +
            "\tthis.IniciarViaje() -> Auto.IniciarViaje()";
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

        var result = _methodService.ExecuteMethod(notificador, notificador, notificar);
        var result2 = _methodService.ExecuteMethod(notificacion, sms, enviar);
        Console.WriteLine(result2);
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(result);
        Assert.AreEqual(result2, "Notificacion.Enviar() -> SMS.Enviar()");
        var res =
            "Notificador.Notificar() -> Notificador.Notificar()\n" +
            "\taviso.Enviar() -> Notificacion.Enviar()\n" +
            "\taviso.Confirmar() -> Notificacion.Confirmar()\n" +
            "\totroAviso.Enviar() -> Email.Enviar()\n" +
            "\totroAviso.Confirmar() -> Notificacion.Confirmar()";
        Assert.AreEqual(res, result);
    }

    [TestMethod]
    public void ExecuteMethodExample4_ShouldReturnMethodExecuted()
    {
        var str = new Class
        {
            Id = Guid.NewGuid(),
            Name = "string",
            Stereotype = null,
            HierarchyClass = null,
            Attributes = [],
            Methods = [],
        };

        var toString = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ToString",
            Accessibility = Accessibility.Public,
            Methods = [],
            Parameters = [],
            LocalVariables = [],
            ReturnType = str,
        };
        str.Methods.Add(toString);

        var nombre = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Nombre",
            Accessibility = Accessibility.Public,
            Type = str
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

        var mensaje = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "Mensaje",
            ParameterType = str
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
            Name = "Usuario",
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

        var autenticar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Autenticar",
            Accessibility = Accessibility.Public,
            Methods = [preparar, validarCredencialesAutenticator, logAcceso, registrar],
            Parameters = [usuarioParam],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
        };

        var clavePrivada = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "ClavePrivada",
            Accessibility = Accessibility.Private,
            Type = str
        };

        var validarCredencialesAP = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ValidarCredenciales",
            Accessibility = Accessibility.Protected,
            Methods = [validar, toString],
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
            HierarchyClass = null,
            Methods = [autenticar, preparar, logAcceso, validarCredencialesAutenticator],
        };

        var autenticadorPassword = new Class
        {
            Id = Guid.NewGuid(),
            Name = "AutenticadorPassword",
            Stereotype = KeyWord.Sealed,
            Attributes = [clavePrivada],
            Methods = [validarCredencialesAP]
        };

        var result = _methodService.ExecuteMethod(autenticador, autenticadorPassword, autenticar);

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
    public void ExecuteMethodExample5_ShouldReturnMethodExecuted()
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

        var cliente = new Class
        {
            Id = Guid.NewGuid(),
            Name = "Cliente",
            Stereotype = null,
            Attributes = [id],
            Methods = [validarIdentidad],
            HierarchyClass = null,
        };

        var str = new Class
        {
            Id = Guid.NewGuid(),
            Name = "string",
            Stereotype = null,
            HierarchyClass = null,
            Attributes = [],
            Methods = [],
        };

        var mensaje = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "mensaje",
            ParameterType = str
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

        var clienteParam = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "cliente",
            ParameterType = cliente
        };

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

        var ejecutar = new Method
        {
            Id = Guid.NewGuid(),
            Name = "Ejecutar",
            Stereotype = null,
            Accessibility = Accessibility.Public,
            Methods = [realizarProcesoProceso, enviarNotificacion],
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
            Methods = [validarIdentidad],
            LocalVariables = [],
            Parameters = [clienteParam],
            ReturnType = new Primitive(PrimitiveType.Bool),
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

        var realizarProcesoPC = new Method
        {
            Id = Guid.NewGuid(),
            Name = "RealizarProceso",
            Accessibility = Accessibility.Protected,
            Methods = [validar, comprobarInventario, enviarNotificacion],
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

        var result = _methodService.ExecuteMethod(proceso, procesoCompra, ejecutar);

        var res =
            "Proceso.Ejecutar(Cliente) -> Proceso.Ejecutar(Cliente)\n" +
            "\tthis.RealizarProceso(Cliente) -> ProcesoCompra.RealizarProceso(Cliente)\n" +
            "\t\tthis.Validar(Cliente) -> Cliente.Validar(Cliente)\n" +
            "\t\t\tcliente.ValidarIdentidad() -> Cliente.ValidarIdentidad()\n" +
            "\t\tthis.ComprobarInventario() -> ProcesoCompra.ComprobarInventario()\n" +
            "\t\tnotificador.EnviarNotificacio(string) -> Comunicacion.EnviarNotificacio(string)\n" +
            "\tnotificador.EnviarNotificacion(string) -> Comunicacion.EnviarNotificacion(string)";
        Console.WriteLine(result);
        Assert.AreEqual(res, result);
    }

    [TestMethod]
    public void ExecuteMethodExample3_SholddReturnMethodExecuted()
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

        var esValidoEntrada = new Method
        {
            Id = Guid.NewGuid(),
            Name = "EsValido",
            Accessibility = Accessibility.Public,
            Methods = [inicializar],
            Parameters = [],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
            Stereotype = null
        };

        var esValidoTexto = new Method
        {
            Id = Guid.NewGuid(),
            Name = "EsValido",
            Accessibility = Accessibility.Public,
            Methods = [esValidoEntrada, limpiar],
            Parameters = [],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
            Stereotype = null
        };

        var esValido = new Method
        {
            Id = Guid.NewGuid(),
            Name = "EsValido",
            Accessibility = Accessibility.Public,
            Methods = [esValidoTexto],
            Parameters = [],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
            Stereotype = KeyWord.Sealed
        };

        var metodoEsValido = new Method
        {
            Id = Guid.NewGuid(),
            Name = "EsValido",
            Accessibility = Accessibility.Public,
            Methods = [esValido],
            Parameters = [],
            LocalVariables = [],
            ReturnType = new Primitive(PrimitiveType.Bool),
            Stereotype = KeyWord.Sealed
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
            Methods = [esValidoTexto, metodoEsValido]
        };

        var entradaTextoEspecial = new Class
        {
            Id = Guid.NewGuid(),
            Name = "EntradaTextoEspecial",
            Stereotype = KeyWord.Sealed,
            HierarchyClass = entradaTexto,
            Attributes = [],
            Methods = [metodoEsValido, esValidoEntrada]
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
            Methods = [esValido],
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

        var result = _methodService.ExecuteMethod(validador, validador, validar);

        var res =
            "Validador.Validar() -> Validador.Validar()\n" +
            "\tinput.EsValido() -> EntradaTextoEspecial.EsValido()\n" +
            "\t\tbase.EsValido() -> EntradaTexto.EsValido()\n" +
            "\t\t\tbase.EsValido() -> Entrada.EsValido()\n" +
            "\t\t\t\tthis.Inicializar() -> Entrada.Inicializar()\n" +
            "\t\t\t\tthis.Limpiar() -> Entrada.Limpiar()";
        Console.WriteLine(result);
        Assert.AreEqual(res, result);
    }
}
