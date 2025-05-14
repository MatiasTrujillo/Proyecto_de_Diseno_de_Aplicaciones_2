using Moq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;
using Type = Simulator.Domain.Type;

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

        var objectClass = new Class { Name = "Object" };
        var existingClasses = new List<Type> { objectClass };

        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns(existingClasses);

        _classService.CreateClass(dto);

        _mockClassRepository.Verify(repo => repo.Add(
            It.Is<Class>(a =>
                a.Name == dto.Name &&
                a.Stereotype == dto.Stereotype &&
                a.HierarchyClass == objectClass)), Times.Once);
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

        var classes = new List<Type> { dto1.ToEntity() };

        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns(classes);

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.CreateClass(dto2);
        });

        Assert.AreEqual("There is already a type with this name", exception.Message);
    }

    [TestMethod]
    public void CreateClass_ShouldThrowException_WhenKeyWordIsNotValid()
    {
        var invalidStereoType = (KeyWord)99;
        var dto = new ClassDTO
        {
            Name = "Class",
            Stereotype = invalidStereoType,
        };

        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.CreateClass(dto);
        });

        Assert.AreEqual("Invalid stereotype value.", exception.Message);
    }

    [TestMethod]
    public void CreateClass_ShouldThrowException_WhenNameIsWhitespace()
    {
        var dto = new ClassDTO
        {
            Name = string.Empty,
            Stereotype = KeyWord.Sealed,
        };

        _mockClassRepository.Setup(r => r.FindAllTypes())
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

        _mockClassRepository.Setup(r => r.FindAllTypes())
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

        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);

        var exception = Assert.ThrowsException<ClassException>(() =>
        {
            _classService.CreateClass(dto);
        });

        Assert.AreEqual("The class name must not contain spaces.", exception.Message);
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
    public void DeleteClass_ShouldThrowException_WhenDeletingObjectClass()
    {
        var objectClass = new Class { Id = Guid.NewGuid(), Name = "Object" };

        _mockClassRepository.Setup(r => r.Find(objectClass.Id)).Returns(objectClass);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([objectClass]);

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.DeleteClass(objectClass.Id);
        });

        Assert.AreEqual("The 'Object' class cannot be deleted.", exception.Message);
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
    public void UpdateClass_WithValidData_UpdatesSuccessfully()
    {
        var classId = Guid.NewGuid();
        var heritageId = Guid.NewGuid();

        var existingClass = new Class { Id = classId, Name = "OldName" };
        var newHeritageClass = new Class { Id = heritageId, Name = "ParentClass" };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([existingClass, newHeritageClass]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([existingClass, newHeritageClass]);
        _mockClassRepository.Setup(r => r.Find(classId)).Returns(existingClass);
        _mockClassRepository.Setup(r => r.Find(heritageId)).Returns(newHeritageClass);
        _mockClassRepository.Setup(r => r.Update(It.IsAny<Class>()));

        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);

        var dto = new UpdateClassDTO
        {
            Name = "NewName",
            IdClass = heritageId
        };

        _classService.UpdateClass(classId, dto);

        Assert.AreEqual("NewName", existingClass.Name);
        Assert.AreEqual(newHeritageClass, existingClass.HierarchyClass);
    }

    [TestMethod]
    public void UpdateClass_ShouldThrowException_WhenAbstractClassInheritsFromConcrete()
    {
        var classId = Guid.NewGuid();
        var heritageId = Guid.NewGuid();

        var existingClass = new Class { Id = classId, Name = "OldName", Stereotype = KeyWord.Abstract };
        var concreteParentClass = new Class { Id = heritageId, Name = "ConcreteClass", Stereotype = null };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([existingClass, concreteParentClass]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([existingClass, concreteParentClass]);
        _mockClassRepository.Setup(r => r.Find(classId)).Returns(existingClass);
        _mockClassRepository.Setup(r => r.Find(heritageId)).Returns(concreteParentClass);

        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);

        var dto = new UpdateClassDTO
        {
            Name = "NewName",
            IdClass = heritageId
        };

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.UpdateClass(classId, dto);
        });

        Assert.AreEqual("An abstract class cannot inherit from a concrete class.", exception.Message);
    }

    [TestMethod]
    public void UpdateClass_ShouldThrowException_WhenSealedClassTriesToInherit()
    {
        var classId = Guid.NewGuid();
        var heritageId = Guid.NewGuid();

        var sealedClass = new Class
        {
            Id = classId,
            Name = "OldName",
            Stereotype = KeyWord.Sealed
        };

        var parentClass = new Class
        {
            Id = heritageId,
            Name = "ParentClass",
            Stereotype = null
        };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([sealedClass, parentClass]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([sealedClass, parentClass]);
        _mockClassRepository.Setup(r => r.Find(classId)).Returns(sealedClass);
        _mockClassRepository.Setup(r => r.Find(heritageId)).Returns(parentClass);

        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);

        var dto = new UpdateClassDTO
        {
            Name = "NewName",
            IdClass = heritageId
        };

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.UpdateClass(classId, dto);
        });

        Assert.AreEqual("A sealed class cannot inherit from any class.", exception.Message);
    }

    [TestMethod]
    public void UpdateClass_ShouldThrowException_WhenUpdatingObjectClass()
    {
        var objectClassId = Guid.NewGuid();
        var objectClass = new Class
        {
            Id = objectClassId,
            Name = "Object",
            Stereotype = KeyWord.Abstract
        };

        var updateDto = new UpdateClassDTO
        {
            Name = "NewName",
        };

        _mockClassRepository.Setup(r => r.Find(objectClassId)).Returns(objectClass);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([objectClass]);

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.UpdateClass(objectClassId, updateDto);
        });

        Assert.AreEqual("The 'Object' class cannot be updated.", exception.Message);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidClassException))]
    public void UpdateClass_WithInvalidName_ThrowsException()
    {
        var id = Guid.NewGuid();
        var classT = new Class { Id = id, Name = "OldName" };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT]);
        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);

        var dto = new UpdateClassDTO { Name = "invalidName" };

        _classService.UpdateClass(id, dto);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidClassException))]
    public void UpdateClass_WithInvalidNameSpace_ThrowsException()
    {
        var id = Guid.NewGuid();
        var classT = new Class { Id = id, Name = "OldName" };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT]);
        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);

        var dto = new UpdateClassDTO { Name = "invalid  ame" };

        _classService.UpdateClass(id, dto);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidClassException))]
    public void UpdateClass_WithInvalidNameEmpty_ThrowsException()
    {
        var id = Guid.NewGuid();
        var classT = new Class { Id = id, Name = "OldName" };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT]);
        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);
        _mockClassRepository.Setup(r => r.Update(It.IsAny<Class>()));

        var dto = new UpdateClassDTO { Name = string.Empty };

        _classService.UpdateClass(id, dto);
    }

    [TestMethod]
    public void UpdateClass_WithDuplicateName_ThrowsException()
    {
        var id = Guid.NewGuid();
        var classT = new Class { Id = id, Name = "OldName" };
        var duplicate = new Class { Id = Guid.NewGuid(), Name = "NewName" };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT, duplicate]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([classT, duplicate]);
        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);

        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);

        var dto = new UpdateClassDTO { Name = "NewName" };

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.UpdateClass(id, dto);
        });

        Assert.AreEqual("There is already a type with this name", exception.Message);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidClassException))]
    public void UpdateClass_WithInvalidStereotype_ThrowsException()
    {
        var id = Guid.NewGuid();
        var classT = new Class { Id = id, Name = "OldName" };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT]);
        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);

        var dto = new UpdateClassDTO
        {
            Stereotype = "invalid"
        };

        _classService.UpdateClass(id, dto);
    }

    [TestMethod]
    [ExpectedException(typeof(ClassNotFoundException))]
    public void UpdateClass_WithNonExistentClassId_ThrowsException()
    {
        var id = Guid.NewGuid();

        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);

        var dto = new UpdateClassDTO();

        _classService.UpdateClass(id, dto);
    }

    [TestMethod]
    [ExpectedException(typeof(ClassInUseException))]
    public void UpdateClass_WithClassInUse_ThrowsException()
    {
        var id = Guid.NewGuid();
        var heritageId = Guid.NewGuid();

        var classT = new Class { Id = id, Name = "OldName", HierarchyClass = null };
        var classInUse = new Class { Id = Guid.NewGuid(), HierarchyClass = new Class { Id = id } };
        var heritageClass = new Class { Id = heritageId };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT, classInUse, heritageClass]);

        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);
        _mockClassRepository.Setup(r => r.Find(heritageId)).Returns(heritageClass);

        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);

        var dto = new UpdateClassDTO
        {
            IdClass = heritageId
        };

        _classService.UpdateClass(id, dto);
    }

    [TestMethod]
    public void CreateClass_ShouldThrowException_WhenKeyWordStatic()
    {
        var invalidStereoType = KeyWord.Static;
        var dto = new ClassDTO
        {
            Name = "Class",
            Stereotype = invalidStereoType,
        };

        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.CreateClass(dto);
        });

        Assert.AreEqual("Invalid stereotype value.", exception.Message);
    }
}
