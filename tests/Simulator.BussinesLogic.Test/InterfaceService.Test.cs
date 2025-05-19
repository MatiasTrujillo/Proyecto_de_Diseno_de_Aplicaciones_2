using Moq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IDataAcces;
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
        lowerCaseNameDto = new InterfaceDTO() { Name = "interfaz" };
        nameWithSpacesDto = new InterfaceDTO() { Name = "Hola " };
        _interfaceService = new InterfaceService(_mockInterfaceRepository.Object, _mockClassRepository.Object);
    }

    [TestMethod]
    public void CreateInterface_ShouldCallRepositoryAdd_WithValidInterface()
    {
        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);

        _interfaceService.CreateInterface(dto);

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
            _interfaceService.CreateInterface(dto);
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
            _interfaceService.CreateInterface(emptyNameDto);
        });

        Assert.AreEqual("The interface name cannot be empty.", exception.Message);
    }

    [TestMethod]
    public void CreateInterface_ShouldThrowException_WhenNameDoesNotStartWithUpperCase()
    {
        var exception = Assert.ThrowsException<InterfaceException>(() =>
        {
            _interfaceService.CreateInterface(lowerCaseNameDto);
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
            _interfaceService.CreateInterface(nameWithSpacesDto);
        });

        Assert.AreEqual("The interface name must not contain spaces.", exception.Message);
    }

    [TestMethod]
    public void DeleteInterface_ShouldDeleteInterface_WhenValid()
    {
        var entity = dto.ToEntity();
        var id = entity.Id;
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([entity]);

        _interfaceService.CreateInterface(dto);

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
        aClass.HierarchyClass = entity;

        _mockInterfaceRepository.Setup(r => r.Delete(It.IsAny<Guid>()));
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([]);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([aClass]);
        var exception = Assert.ThrowsException<InterfaceNotFoundException>(() =>
        {
            _interfaceService.DeleteInterface(id);
        });

        Assert.AreEqual("Interface is associated with a class", exception.Message);
    }
}
