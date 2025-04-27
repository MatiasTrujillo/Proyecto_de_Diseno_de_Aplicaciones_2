using Moq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
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

        var classes = new List<Class> { dto1.ToEntity() };

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns(classes);

        var exception = Assert.ThrowsException<ClassException>(() =>
        {
            _classService.CreateClass(dto2);
        });

        Assert.AreEqual("There is already a class with this name", exception.Message);
    }

    [TestMethod]
    public void CreateClass_ShouldThrowException_WhenNameIsWhitespace()
    {
        var dto = new ClassDTO
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Stereotype = KeyWord.Sealed,
            HirarchyClass = null,
            Attributes = []
        };

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns([]);

        var exception = Assert.ThrowsException<ClassException>(() =>
        {
            _classService.CreateClass(dto);
        });

        Assert.AreEqual("The class name cannot be empty.", exception.Message);
    }

    [TestMethod]
    public void CreateClass_ShouldThrowException_WhenNameDoesNotBeginWithACapitalLetter()
    {
        var dto = new ClassDTO
        {
            Id = Guid.NewGuid(),
            Name = "class",
            Stereotype = KeyWord.Sealed,
            HirarchyClass = null,
            Attributes = []
        };

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns([]);

        var exception = Assert.ThrowsException<ClassException>(() =>
        {
            _classService.CreateClass(dto);
        });

        Assert.AreEqual("The class name must start with an uppercase letter.", exception.Message);
    }

    [TestMethod]
    public void CreateClass_ShouldThrowException_WhenNameCannotHaveSpacesDoesNot()
    {
        var dto = new ClassDTO
        {
            Id = Guid.NewGuid(),
            Name = "Class Name",
            Stereotype = KeyWord.Sealed,
            HirarchyClass = null,
            Attributes = []
        };

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns([]);

        var exception = Assert.ThrowsException<ClassException>(() =>
        {
            _classService.CreateClass(dto);
        });

        Assert.AreEqual("The class name must not contain spaces.", exception.Message);
    }

    [TestMethod]
    public void UpdateClass_ShouldUpdateClass_WhenValid()
    {
        var id = Guid.NewGuid();
        var original = new Class("OldName", KeyWord.Sealed, null, [], []);
        original.Id = id;
        var dto = new ClassDTO
        {
            Id = id,
            Name = "NewName",
            Stereotype = KeyWord.Sealed,
            HirarchyClass = null,
            Attributes = []
        };

        var classes = new List<Class> { original };

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns(classes);

        _mockClassRepository
            .Setup(r => r.Find(id))
            .Returns(original);

        _mockClassRepository
            .Setup(r => r.Update(It.IsAny<Class>()));

        _classService.UpdateNameClass(id, dto.Name);

        Assert.AreEqual("NewName", original.Name);
        Assert.AreEqual(KeyWord.Sealed, original.Stereotype);
        _mockClassRepository.Verify(r => r.Update(original), Times.Once);
    }

    [TestMethod]
    public void UpdateClass_ShouldNotUpdateClass_WhenNameIsRepeated()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var original1 = new Class("Class1", KeyWord.Sealed, null, [], []);
        var original2 = new Class("Class2", KeyWord.Sealed, null, [], []);
        var dto = new ClassDTO
        {
            Id = id2,
            Name = "Class1",
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

        var classes = new List<Class> { original1, original2 };

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns(classes);

        _mockClassRepository
            .Setup(r => r.Update(It.IsAny<Class>()));

        var exception = Assert.ThrowsException<ClassException>(() =>
        {
            _classService.UpdateNameClass(id2, dto.Name);
        });

        Assert.AreEqual("There is already a class with this name", exception.Message);
    }

    [TestMethod]
    public void UpdateClass_ShouldNotUpdateClass_WhenClassDoesNotExist()
    {
        var id = Guid.NewGuid();
        var dto = new ClassDTO
        {
            Id = id,
            Name = "Class1",
            Stereotype = KeyWord.Abstract,
            HirarchyClass = null,
            Attributes = []
        };

        _mockClassRepository
            .Setup(r => r.Find(id))
            .Returns<Class?>(null);

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns([]);

        _mockClassRepository
            .Setup(r => r.Update(It.IsAny<Class>()));

        var exception = Assert.ThrowsException<ClassException>(() =>
        {
            _classService.UpdateNameClass(id, dto.Name);
        });

        Assert.AreEqual("There is no class with this id", exception.Message);
    }

    [TestMethod]
    public void UpdateClassStereotype_ShouldUpdate_WhenClassExistsAndStereotypeIsAbstract()
    {
        var classId = Guid.NewGuid();
        var existingClass = new Class("TestClass", KeyWord.Sealed, null, null, new List<Method>());
        existingClass.Id = classId;

        _mockClassRepository.Setup(repo => repo.Find(classId)).Returns(existingClass);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns(new List<Class> { existingClass });
        _mockClassRepository.Setup(repo => repo.Update(It.IsAny<Class>()));

        _classService.UpdateClassStereotype(classId, "Abstract");

        Assert.AreEqual(KeyWord.Abstract, existingClass.Stereotype);
        _mockClassRepository.Verify(repo => repo.Update(existingClass), Times.Once);
    }

    [TestMethod]
    public void UpdateClassStereotype_ShouldUpdate_WhenClassExistsAndStereotypeIsSealed()
    {
        var classId = Guid.NewGuid();
        var existingClass = new Class("TestClass", KeyWord.Abstract, null, null, new List<Method>());
        existingClass.Id = classId;

        _mockClassRepository.Setup(repo => repo.Find(classId)).Returns(existingClass);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns(new List<Class> { existingClass });
        _mockClassRepository.Setup(repo => repo.Update(It.IsAny<Class>()));

        _classService.UpdateClassStereotype(classId, "Sealed");

        Assert.AreEqual(KeyWord.Sealed, existingClass.Stereotype);
        _mockClassRepository.Verify(repo => repo.Update(existingClass), Times.Once);
    }

    [TestMethod]
    public void UpdateClassStereotype_ShouldThrowException_WhenClassDoesNotExist()
    {
        var classId = Guid.NewGuid();

        _mockClassRepository.Setup(repo => repo.Find(classId)).Returns<Class?>(null);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns(new List<Class>());

        var exception = Assert.ThrowsException<ClassException>(() =>
        {
            _classService.UpdateClassStereotype(classId, "Abstract");
        });

        Assert.AreEqual("There is no class with this id", exception.Message);
    }

    [TestMethod]
    public void UpdateClassStereotype_ShouldThrowException_WhenStereotypeIsInvalid()
    {
        var classId = Guid.NewGuid();
        var existingClass = new Class("TestClass", KeyWord.Sealed, null, null, new List<Method>());
        existingClass.Id = classId;

        _mockClassRepository.Setup(repo => repo.Find(classId)).Returns(existingClass);

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns(new List<Class> { existingClass });

        var exception = Assert.ThrowsException<InvalidClassStereotypeException>(() =>
        {
            _classService.UpdateClassStereotype(classId, "InvalidStereotype");
        });

        Assert.AreEqual("The stereotype is not valid", exception.Message);
    }

    [TestMethod]
    public void DeleteClass_ShouldDeleteClass_WhenValid()
    {
        var id = Guid.NewGuid();
        var original = new Class("Class2", KeyWord.Sealed, null, [], []);
        original.Id = id;

        _mockClassRepository
            .Setup(r => r.Find(id))
            .Returns(original);

        _mockClassRepository
            .Setup(r => r.FindAll())
            .Returns([original]);

        _mockClassRepository
            .Setup(r => r.Delete(It.IsAny<Guid>()));

        _classService.DeleteClass(id);

        _mockClassRepository.Verify(r => r.Delete(id), Times.Once);
    }

    [TestMethod]
    public void DeleteClass_ShouldNotDeleteClass_WhenIdDoesNotExist()
    {
        var id = Guid.NewGuid();

        _mockClassRepository
            .Setup(r => r.Find(id))
            .Returns<Class?>(null);

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns([]);

        _mockClassRepository
            .Setup(r => r.Delete(It.IsAny<Guid>()));

        var exception = Assert.ThrowsException<ClassException>(() =>
        {
            _classService.DeleteClass(id);
        });

        Assert.AreEqual("There is no class with this id", exception.Message);
    }
}
