using DTO;
using Moq;
using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class ClassService_Test
{
    private Mock<IClassRepository> _mockClassRepository = null!;
    private ClassService _classService = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);

        _mockClassRepository
            .Setup(repo => repo.Add(It.IsAny<Class>()));

        _classService = new ClassService(_mockClassRepository.Object);
    }

    [TestMethod]
    public void CreateClass_ShouldCallRepositoryAdd_WithValidClass()
    {
        var dto = new ClassDTO
        {
            Id = Guid.NewGuid(),
            Name = "Pedro",
            Stereotype = KeyWord.Sealed,
            HirarchyClass = null,
            Attributes = []
        };

        _classService.CreateClass(dto);

        _mockClassRepository.Verify(repo => repo.Add(
            It.Is<Class>(a =>
                a.Id == dto.Id &&
                a.Name == dto.Name &&
                a.Stereotype == dto.Stereotype)), Times.Once);
    }
}
