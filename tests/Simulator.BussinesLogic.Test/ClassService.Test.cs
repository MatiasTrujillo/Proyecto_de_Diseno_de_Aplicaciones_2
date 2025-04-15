using DTO;
using Moq;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class ClassServiceTest
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

    [TestMethod]
    public void CreateClass_ShouldNotCallRepositoryAdd_WithRepeatedClassName()
    {
        var dto1 = new ClassDTO
        {
            Id = Guid.NewGuid(),
            Name = "Pedro",
            Stereotype = KeyWord.Sealed,
            HirarchyClass = null,
            Attributes = []
        };

        var dto2 = new ClassDTO
        {
            Id = Guid.NewGuid(),
            Name = "Pedro",
            Stereotype = null,
            HirarchyClass = null,
            Attributes = []
        };

        List<Class> classes = new List<Class>();
        classes.Add(dto1.ToEntity());

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns(classes);

        var exception = Assert.ThrowsException<ClassException>(() =>
        {
            _classService.CreateClass(dto2);
        });

        Assert.AreEqual("There is already a class with this name", exception.Message);
    }
}
