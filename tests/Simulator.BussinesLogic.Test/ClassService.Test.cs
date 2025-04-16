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

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns([]);

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

    [TestMethod]
    public void UpdateClass_ShouldUpdateClass_WhenValid()
    {
        var id = Guid.NewGuid();
        var original = new Class(id, "OldName", KeyWord.Sealed, null, []);
        var dto = new ClassDTO
        {
            Id = id,
            Name = "NewName",
            Stereotype = KeyWord.Sealed,
            HirarchyClass = null,
            Attributes = []
        };

        List<Class> classes = new List<Class>();
        classes.Add(original);

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns(classes);

        _mockClassRepository
            .Setup(r => r.Find(id))
            .Returns(original);

        _mockClassRepository
            .Setup(r => r.Update(It.IsAny<Class>()));

        _classService.UpdateClass(id, dto);

        Assert.AreEqual("NewName", original.Name);
        Assert.AreEqual(KeyWord.Sealed, original.Stereotype);
        _mockClassRepository.Verify(r => r.Update(original), Times.Once);
    }

    [TestMethod]
    public void UpdateClass_ShouldNotUpdateClass_WhenNameIsRepeated()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var original1 = new Class(id1, "name1", KeyWord.Sealed, null, []);
        var original2 = new Class(id2, "name2", KeyWord.Sealed, null, []);
        var dto = new ClassDTO
        {
            Id = id2,
            Name = "name1",
            Stereotype = KeyWord.Abstract,
            HirarchyClass = null,
            Attributes = []
        };

        _mockClassRepository
            .Setup(r => r.Find(id1))
            .Returns(original1);
        _mockClassRepository
            .Setup(r => r.Find(id2))
            .Returns(original2);

        List<Class> classes = new List<Class>();
        classes.Add(original1);
        classes.Add(original2);

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns(classes);

        _mockClassRepository
            .Setup(r => r.Update(It.IsAny<Class>()));

        var exception = Assert.ThrowsException<ClassException>(() =>
        {
            _classService.UpdateClass(id2, dto);
        });

        Assert.AreEqual("There is already a class with this name", exception.Message);
    }
}
