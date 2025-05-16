using Moq;
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

    [TestInitialize]
    public void Initialize()
    {
        _mockInterfaceRepository = new Mock<IInterfaceRepository>(MockBehavior.Strict);
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);
    }

    [TestMethod]
    public void CreateInterface_ShouldCallRepositoryAdd_WithValidInterface()
    {
        _interfaceService = new InterfaceService(_mockInterfaceRepository.Object, _mockClassRepository.Object);
        var dto = new InterfaceDTO
        {
            Name = "Interfaz"
        };

        List<Type> existingClasses = [];

        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns(existingClasses);

        _interfaceService.CreateInterface(dto);

        _mockInterfaceRepository.Verify(repo => repo.Add(
            It.Is<Interface>(a =>
                a.Name == dto.Name)));
    }
}
