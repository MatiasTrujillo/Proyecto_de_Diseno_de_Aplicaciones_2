using Moq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IDataAcces;
using Simulator.UpdateDTO;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class InterfaceService_Test
{
    private Mock<IInterfaceRepository> _mockInterfaceRepository = null!;
    private Mock<IClassRepository> _mockClassRepository = null!;
    private InterfaceService _interfaceService = null!;
    private InterfaceDTO dto = null!;
    private InterfaceDTO emptyNameDto = null!;
    private InterfaceDTO lowerCaseNameDto = null!;
    private InterfaceDTO nameWithSpacesDto = null!;
    private Mock<IMethodSignatureRepository> _mockMethodSignatureRepository = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mockInterfaceRepository = new Mock<IInterfaceRepository>(MockBehavior.Strict);
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);
        _mockInterfaceRepository
            .Setup(repo => repo.Add(It.IsAny<Interface>()));
        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);
        dto = new InterfaceDTO
        {
            Name = "Interfaz"
        };
        emptyNameDto = new InterfaceDTO
        {
            Name = string.Empty,
        };
        lowerCaseNameDto = new InterfaceDTO() { Name = "interface" };
        nameWithSpacesDto = new InterfaceDTO() { Name = "Hola " };
        _mockMethodSignatureRepository = new Mock<IMethodSignatureRepository>(MockBehavior.Strict);
        _interfaceService = new InterfaceService(_mockInterfaceRepository.Object, _mockClassRepository.Object, _mockMethodSignatureRepository.Object);
    }

    [TestMethod]
    public void CreateInterface_ShouldCallRepositoryAdd_WithValidInterface()
    {
        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);

        _interfaceService.CreateInterface(dto.ToEntity());

        _mockInterfaceRepository.Verify(repo => repo.Add(
            It.Is<Interface>(a =>
                a.Name == dto.Name)));
    }

    [TestMethod]
    public void CreateInterface_ShouldThrowException_WithRepeatedInterfaceName()
    {
        var types = new List<Type> { dto.ToEntity() };

        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns(types);

        var exception = Assert.ThrowsException<InvalidInterfaceException>(() =>
        {
            _interfaceService.CreateInterface(dto.ToEntity());
        });

        Assert.AreEqual("There is already a type with this name", exception.Message);
    }

    [TestMethod]
    public void CreateInterface_ShouldThrowException_WhenNameIsWhitespace()
    {
        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);

        var exception = Assert.ThrowsException<InterfaceException>(() =>
        {
            _interfaceService.CreateInterface(emptyNameDto.ToEntity());
        });

        Assert.AreEqual("The interface name cannot be empty.", exception.Message);
    }

    [TestMethod]
    public void CreateInterface_ShouldThrowException_WhenNameDoesNotStartWithUpperCase()
    {
        var exception = Assert.ThrowsException<InterfaceException>(() =>
        {
            _interfaceService.CreateInterface(lowerCaseNameDto.ToEntity());
        });

        Assert.AreEqual("The interface name must start with an uppercase letter.", exception.Message);
    }

    [TestMethod]
    public void CreateInterface_ShouldThrowException_WhenNameHasSpaces()
    {
        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);

        var exception = Assert.ThrowsException<InterfaceException>(() =>
        {
            _interfaceService.CreateInterface(nameWithSpacesDto.ToEntity());
        });

        Assert.AreEqual("The interface name must not contain spaces.", exception.Message);
    }

    [TestMethod]
    public void DeleteInterface_ShouldDeleteInterface_WhenValid()
    {
        var entity = dto.ToEntity();
        var id = entity.Id;
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([entity]);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);

        _interfaceService.CreateInterface(dto.ToEntity());

        _mockInterfaceRepository.Setup(r => r.Delete(It.IsAny<Guid>()));
        _interfaceService.DeleteInterface(id);

        _mockInterfaceRepository.Verify(r => r.Delete(id), Times.Once);
    }

    [TestMethod]
    public void DeleteInterface_ShouldThrowException_WhenInterfaceNotExists()
    {
        var entity = dto.ToEntity();
        var id = entity.Id;

        _mockInterfaceRepository.Setup(r => r.Delete(It.IsAny<Guid>()));
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([]);
        var exception = Assert.ThrowsException<InterfaceNotFoundException>(() =>
        {
            _interfaceService.DeleteInterface(id);
        });

        Assert.AreEqual("There is no interface with this id.", exception.Message);
    }

    [TestMethod]
    public void DeleteInterface_ShouldThrowException_WhenInterfaceIsBeingUsed()
    {
        var entity = dto.ToEntity();
        var id = entity.Id;
        var aClass = new Class();
        aClass.Parents = [entity];

        _mockInterfaceRepository.Setup(r => r.Delete(It.IsAny<Guid>()));
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([entity]);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([aClass]);
        var exception = Assert.ThrowsException<InterfaceInUseException>(() =>
        {
            _interfaceService.DeleteInterface(id);
        });

        Assert.AreEqual("Interface is associated with a class.", exception.Message);
    }

    [TestMethod]
    public void UpdateInterface_ShouldUpdateInterface_WhenValidMethodSignatureIdGiven()
    {
        var entity = dto.ToEntity();
        var id = entity.Id;

        var parameter = new Parameter();
        var type = new Primitive(PrimitiveType.String);
        var signature = new MethodSignature("ASignature", type, [parameter]);
        var updateInterfaceDto = new UpdateInterfaceDTO { IdMethodSignature = signature.Id };

        _mockMethodSignatureRepository.Setup(r => r.Find(It.IsAny<Guid>()))
            .Returns(signature);
        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);
        _mockInterfaceRepository.Setup(r => r.Find(id)).Returns(entity);
        _mockInterfaceRepository.Setup(r => r.Exist(id)).Returns(true);
        _mockInterfaceRepository.Setup(r => r.Update(It.IsAny<Interface>()));

        _interfaceService.CreateInterface(dto.ToEntity());
        _interfaceService.UpdateInterface(id, updateInterfaceDto);

        _mockInterfaceRepository.Verify(repo => repo.Update(
            It.Is<Interface>(i =>
                i.Id == id &&
                i.MethodSignatures.Any(ms => ms.Id == signature.Id))), Times.Once);
    }

    [TestMethod]
    public void UpdateInterface_ShouldThrowInterfaceNotFoundException_WhenNonExistingInterfaceIdGiven()
    {
        var entity = dto.ToEntity();
        var id = entity.Id;

        var parameter = new Parameter();
        var type = new Primitive(PrimitiveType.String);
        var signature = new MethodSignature("ASignature", type, [parameter]);
        var updateInterfaceDto = new UpdateInterfaceDTO { IdMethodSignature = signature.Id };

        _mockMethodSignatureRepository.Setup(r => r.Find(It.IsAny<Guid>()))
            .Returns(signature);
        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);
        _mockInterfaceRepository.Setup(r => r.Find(id)).Returns(entity);
        _mockInterfaceRepository.Setup(r => r.Exist(id)).Returns(false);
        _mockInterfaceRepository.Setup(r => r.Update(It.IsAny<Interface>()));

        _interfaceService.CreateInterface(dto.ToEntity());

        var exception = Assert.ThrowsException<InterfaceNotFoundException>(() =>
        {
            _interfaceService.UpdateInterface(id, updateInterfaceDto);
        });

        Assert.AreEqual("Interface not found", exception.Message);
    }

    [TestMethod]
    public void UpdateInterface_ShouldThrowMethodSignatureNotFound_WhenNonExistingMethodSignatureIdGiven()
    {
        var entity = dto.ToEntity();
        var id = entity.Id;
        var updateInterfaceDto = new UpdateInterfaceDTO { IdMethodSignature = Guid.NewGuid() };

        _mockMethodSignatureRepository
            .Setup(r => r.Find(It.IsAny<Guid>()))
            .Returns(((MethodSignature?)null)!);
        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);
        _mockInterfaceRepository.Setup(r => r.Find(id)).Returns(entity);
        _mockInterfaceRepository.Setup(r => r.Exist(id)).Returns(true);
        _mockInterfaceRepository.Setup(r => r.Update(It.IsAny<Interface>()));

        _interfaceService.CreateInterface(dto.ToEntity());
        var exception = Assert.ThrowsException<MethodSignatureNotFoundException>(() =>
        {
            _interfaceService.UpdateInterface(id, updateInterfaceDto);
        });

        Assert.AreEqual("Method signature id not found.", exception.Message);
    }

    [TestMethod]
    public void UpdateInterface_ShouldThrowInterfaceNotFoundException_WhenMethodSignatureAlreadyExists()
    {
        var entity = dto.ToEntity();
        var id = entity.Id;

        var parameter = new Parameter();
        var type = new Primitive(PrimitiveType.String);
        var signature = new MethodSignature("ASignature", type, [parameter]);
        entity.MethodSignatures = [signature];

        var updateInterfaceDto = new UpdateInterfaceDTO { IdMethodSignature = signature.Id };

        _mockMethodSignatureRepository.Setup(r => r.Find(signature.Id)).Returns(signature);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([]);
        _mockInterfaceRepository.Setup(r => r.Find(id)).Returns(entity);
        _mockInterfaceRepository.Setup(r => r.Exist(id)).Returns(true);
        _mockInterfaceRepository.Setup(r => r.Update(It.IsAny<Interface>()));

        _interfaceService.CreateInterface(dto.ToEntity());

        var exception = Assert.ThrowsException<InterfaceNotFoundException>(() =>
        {
            _interfaceService.UpdateInterface(id, updateInterfaceDto);
        });

        Assert.AreEqual("Interface already have this methodsignature.", exception.Message);
    }

    [TestMethod]
    public void GetInterfaces_ShouldReturnListOfInterfaces()
    {
        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);
        _mockInterfaceRepository.Setup(r => r.FindAll())
            .Returns([dto.ToEntity()]);

        _interfaceService.CreateInterface(dto.ToEntity());
        var result = _interfaceService.GetInterfaces();

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.IsTrue(result.Any(i => i.Name == dto.Name));
        _mockInterfaceRepository.Verify(repo => repo.FindAll(), Times.Once);
    }
}
