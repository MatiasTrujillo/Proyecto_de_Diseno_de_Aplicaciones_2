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

    [TestInitialize]
    public void Initialize()
    {
        _mockInterfaceRepository = new Mock<IInterfaceRepository>(MockBehavior.Strict);
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);
        _mockInterfaceRepository
            .Setup(repo => repo.Add(It.IsAny<Interface>()));
        dto = new InterfaceDTO
        {
            Name = "Interfaz"
        };
        _interfaceService = new InterfaceService(_mockInterfaceRepository.Object, _mockClassRepository.Object);
    }

    [TestMethod]
    public void CreateInterface_ShouldCallRepositoryAdd_WithValidInterface()
    {
        List<Type> types = [];
        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns(types);

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
}
