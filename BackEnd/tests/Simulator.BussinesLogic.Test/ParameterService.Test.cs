using Moq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
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
            ParameterType = new TypeDTO
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
        var classT = new TypeDTO { Name = "TestClass", Kind = "class" };

        var parameterDto = new ParameterDTO { Name = "TestParameter", ParameterType = classT };

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
    public void CreateParameter_ShouldThrowException_WhenParameterNameIsEmpty()
    {
        var classT = new TypeDTO { Name = "TestClass", Kind = "class" };

        var parameterDto = new ParameterDTO { Name = string.Empty, ParameterType = classT };

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = "TestClass" }]);

        var exception = Assert.ThrowsException<ParameterException>(() =>
            _parameterService.CreateParameter(parameterDto));

        Assert.AreEqual("Name cannot be null or empty", exception.Message);
    }

    [DataTestMethod]
    [DataRow("base")]
    [DataRow("this")]
    [DataRow("Object")]
    public void CreateParameter_ShouldThrowException_WhenNameIsReservedKeyword(string reservedName)
    {
        var dto = new ParameterDTO { Name = reservedName, ParameterType = new TypeDTO { Name = "SomeType", Kind = "class" }, };

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = "SomeType", Id = Guid.NewGuid() }]);

        var ex = Assert.ThrowsException<ParameterException>(() =>
        {
            _parameterService.CreateParameter(dto);
        });

        Assert.AreEqual("The name is a reserved keyword and is not allowed.", ex.Message);
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
    public void SetInvokeMethodToAParameter_ShouldThrowException_WhenTypeIsNotPrimitiveorClass()
    {
        var id = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        var parameter = new Parameter
        {
            Id = id,
            Name = "myVar",
            ReferenceType = new Interface()
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

        var exception = Assert.ThrowsException<InvalidAttributeException>(() => _parameterService.SetInvokeMethodToParameter(id, invokedMethodId));

        Assert.AreEqual("In v2 of this program only Class and Primitive types are accepted.", exception.Message);
    }
}
