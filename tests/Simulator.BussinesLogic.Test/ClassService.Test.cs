using Moq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class ClassServiceTest
{
    private Mock<IClassRepository> _mockClassRepository = null!;
    private Mock<IAttributeRepository> _mockAttributeRepository = null!;
    private Mock<IMethodRepository> _mockMethodRepository = null!;
    private ClassService _classService = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);
        _mockAttributeRepository = new Mock<IAttributeRepository>(MockBehavior.Strict);
        _mockMethodRepository = new Mock<IMethodRepository>(MockBehavior.Strict);
        _mockClassRepository
            .Setup(repo => repo.Add(It.IsAny<Class>()));
        _mockAttributeRepository.Setup(repo => repo.Add(It.IsAny<Attribute>()));
        _mockMethodRepository.Setup(repo => repo.Add(It.IsAny<Method>()));
        _classService = new ClassService(_mockClassRepository.Object, _mockAttributeRepository.Object, _mockMethodRepository.Object);
    }

    [TestMethod]
    public void CreateClass_ShouldCallRepositoryAdd_WithValidClass()
    {
        var dto = new ClassDTO
        {
            Name = "Pedro",
            Stereotype = KeyWord.Sealed,
        };

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns([]);

        _classService.CreateClass(dto);

        _mockClassRepository.Verify(repo => repo.Add(
            It.Is<Class>(a =>
                a.Name == dto.Name &&
                a.Stereotype == dto.Stereotype)), Times.Once);
    }

    [TestMethod]
    public void CreateClass_ShouldNotCallRepositoryAdd_WithRepeatedClassName()
    {
        var dto1 = new ClassDTO
        {
            Name = "Pedro",
            Stereotype = KeyWord.Sealed,
        };

        var dto2 = new ClassDTO
        {
            Name = "Pedro",
            Stereotype = null,
        };

        var classes = new List<Class> { dto1.ToEntity() };

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns(classes);

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
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
            Name = string.Empty,
            Stereotype = KeyWord.Sealed,
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
            Name = "class",
            Stereotype = KeyWord.Sealed,
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
            Name = "Class Name",
            Stereotype = KeyWord.Sealed,
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
        var original = new Class(KeyWord.Sealed, null, [], [], "OldName") { Id = id };
        var dto = new ClassDTO
        {
            Name = "NewName",
            Stereotype = KeyWord.Sealed,
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
        var original1 = new Class(KeyWord.Sealed, null, [], [], "Class1");
        var original2 = new Class(KeyWord.Sealed, null, [], [], "Class2");
        var dto = new ClassDTO
        {
            Name = "Class1",
            Stereotype = KeyWord.Abstract,
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

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
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
            Name = "Class1",
            Stereotype = KeyWord.Abstract,
        };

        _mockClassRepository
            .Setup(r => r.Find(id))
            .Returns<Class?>(null);

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns([]);

        _mockClassRepository
            .Setup(r => r.Update(It.IsAny<Class>()));

        var exception = Assert.ThrowsException<ClassNotFoundException>(() =>
        {
            _classService.UpdateNameClass(id, dto.Name);
        });

        Assert.AreEqual("There is no class with this id", exception.Message);
    }

    [TestMethod]
    public void UpdateClassStereotype_ShouldUpdate_WhenClassExistsAndStereotypeIsAbstract()
    {
        var classId = Guid.NewGuid();
        var existingClass = new Class(KeyWord.Sealed, null, null, [], "TestClass") { Id = classId };

        _mockClassRepository.Setup(repo => repo.Find(classId)).Returns(existingClass);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([existingClass]);
        _mockClassRepository.Setup(repo => repo.Update(It.IsAny<Class>()));

        _classService.UpdateClassStereotype(classId, "Abstract");

        Assert.AreEqual(KeyWord.Abstract, existingClass.Stereotype);
        _mockClassRepository.Verify(repo => repo.Update(existingClass), Times.Once);
    }

    [TestMethod]
    public void UpdateClassStereotype_ShouldUpdate_WhenClassExistsAndStereotypeIsSealed()
    {
        var classId = Guid.NewGuid();
        var existingClass = new Class(KeyWord.Abstract, null, null, [], "TestClass") { Id = classId };

        _mockClassRepository.Setup(repo => repo.Find(classId)).Returns(existingClass);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([existingClass]);
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
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([]);

        var exception = Assert.ThrowsException<ClassNotFoundException>(() =>
        {
            _classService.UpdateClassStereotype(classId, "Abstract");
        });

        Assert.AreEqual("There is no class with this id", exception.Message);
    }

    [TestMethod]
    public void UpdateClassStereotype_ShouldThrowException_WhenStereotypeIsInvalid()
    {
        var classId = Guid.NewGuid();
        var existingClass = new Class(KeyWord.Sealed, null, null, [], "TestClass") { Id = classId };

        _mockClassRepository.Setup(repo => repo.Find(classId)).Returns(existingClass);

        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([existingClass]);

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.UpdateClassStereotype(classId, "InvalidStereotype");
        });

        Assert.AreEqual("The stereotype is not valid", exception.Message);
    }

    [TestMethod]
    public void DeleteClass_ShouldDeleteClass_WhenValid()
    {
        var id = Guid.NewGuid();
        var original = new Class(KeyWord.Sealed, null, [], [], "Class2") { Id = id };

        _mockClassRepository.Setup(r => r.Find(id)).Returns(original);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([original]);
        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);
        _mockClassRepository.Setup(r => r.Delete(It.IsAny<Guid>()));

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

        var exception = Assert.ThrowsException<ClassNotFoundException>(() =>
        {
            _classService.DeleteClass(id);
        });

        Assert.AreEqual("There is no class with this id", exception.Message);
    }

    [TestMethod]
    public void UpdateClassInherits_ShouldUpdateHierarchy_WhenValid()
    {
        var classId = Guid.NewGuid();
        var hierarchyClassId = Guid.NewGuid();

        var existingClass = new Class(KeyWord.Sealed, null, [], [], "ClassA");
        existingClass.Id = classId;

        var hierarchyClass = new Class(KeyWord.Sealed, null, [], [], "ClassB");
        hierarchyClass.Id = hierarchyClassId;

        var dto = new ClassDTO
        {
            Name = "ClassB",
            Stereotype = KeyWord.Sealed,
        };

        _mockClassRepository.Setup(repo => repo.Find(classId)).Returns(existingClass);
        _mockClassRepository.Setup(repo => repo.Find(hierarchyClassId)).Returns(hierarchyClass);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([existingClass, hierarchyClass]);
        _mockAttributeRepository.Setup(repo => repo.FindAll()).Returns([]);
        _mockMethodRepository.Setup(repo => repo.FindAll()).Returns([]);
        _mockClassRepository.Setup(repo => repo.Update(It.IsAny<Class>()));

        _classService.UpdateClassInherits(classId, hierarchyClassId);

        Assert.AreEqual(hierarchyClass.Name, existingClass.HierarchyClass.Name);
        _mockClassRepository.Verify(repo => repo.Update(existingClass), Times.Once);
    }

    [TestMethod]
    public void UpdateClassInherits_ShouldThrowException_WhenClassIsInUse()
    {
        var existingClassId = Guid.NewGuid();
        var hierarchyClassId = Guid.NewGuid();
        var associatedClassId = Guid.NewGuid();

        var existingClass = new Class(KeyWord.Sealed, null, [], [], "ClassA")
        {
            Id = existingClassId
        };

        var hierarchyClass = new Class(KeyWord.Sealed, null, [], [], "ClassB")
        {
            Id = hierarchyClassId
        };

        var associatedClass = new Class(KeyWord.Sealed, existingClass, [], [], "AssociatedClass")
        {
            Id = associatedClassId
        };

        var classes = new List<Class> { existingClass, hierarchyClass, associatedClass };

        _mockClassRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns<Guid>(id =>
        {
            if (id == existingClassId)
            {
                return existingClass;
            }

            if (id == hierarchyClassId)
            {
                return hierarchyClass;
            }

            return null;
        });

        _mockClassRepository.Setup(r => r.FindAll()).Returns(classes);
        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);

        var exception = Assert.ThrowsException<ClassInUseException>(() =>
        {
            _classService.UpdateClassInherits(existingClassId, hierarchyClassId);
        });

        Assert.AreEqual("Class is associated with another class, attribute or method", exception.Message);
    }

    [TestMethod]
    public void UpdateClassInherits_ShouldThrowException_WhenHierarchyClassDoesNotExist()
    {
        var classId = Guid.NewGuid();

        var existingClass = new Class(KeyWord.Sealed, null, [], [], "ClassA") { Id = classId };

        var dto = new ClassDTO
        {
            Name = "NonExistentClass",
            Stereotype = KeyWord.Sealed,
        };

        _mockClassRepository.Setup(repo => repo.Find(classId)).Returns(existingClass);
        _mockClassRepository.Setup(repo => repo.FindAll()).Returns([existingClass]);
        _mockAttributeRepository.Setup(repo => repo.FindAll()).Returns([]);
        _mockMethodRepository.Setup(repo => repo.FindAll()).Returns([]);
        _mockClassRepository.Setup(repo => repo.Update(It.IsAny<Class>()));

        var exception = Assert.ThrowsException<ClassNotFoundException>(() =>
        {
            _classService.UpdateClassInherits(Guid.NewGuid(), Guid.Empty);
        });

        Assert.AreEqual("There is no class with this id", exception.Message);
    }
}
