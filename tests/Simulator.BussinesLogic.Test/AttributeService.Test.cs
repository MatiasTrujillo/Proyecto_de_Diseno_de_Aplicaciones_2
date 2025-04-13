using Moq;
using Simulator.BussinesLogic.DTO;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class AttributeServiceTest
{
    private Mock<IAttributeRepository> _mockAttributeRepository = null!;
    private Mock<IClassRepository> _mockClassRepository = null!;
    private AttributeService _attributeService = null!;
    private Guid _classId;

    [TestInitialize]
    public void Initialize()
    {
        _mockAttributeRepository = new Mock<IAttributeRepository>(MockBehavior.Strict);
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);

        _mockAttributeRepository
            .Setup(repo => repo.Add(It.IsAny<Attribute>()));

        _attributeService = new AttributeService(_mockAttributeRepository.Object, _mockClassRepository.Object);
        _classId = Guid.NewGuid();
    }

    [TestMethod]
    public void CreateAttribute_ShouldCallRepositoryAdd_WithValidAttribute()
    {
        var dto = new AttributeDTO
        {
            Id = Guid.NewGuid(),
            Name = "Strength",
            Accessibility = Accessibility.Private
        };

        _attributeService.CreateAttribute(dto);

        _mockAttributeRepository.Verify(repo => repo.Add(
            It.Is<Attribute>(a =>
                a.Id == dto.Id &&
                a.Name == dto.Name &&
                a.Accessibility == dto.Accessibility)), Times.Once);
    }

    [TestMethod]
    public void AsociateAtributeToClass_ShouldAddAttribute_WhenValid()
    {
        var classId = Guid.NewGuid();
        var attributeDTO = new AttributeDTO
        {
            Id = Guid.NewGuid(),
            Name = "Agility",
            Accessibility = Accessibility.Public
        };

        var classEntity = new Class
        {
            Id = classId,
            Name = "Warrior",
            Attributes = new List<Attribute>()
        };

        _mockClassRepository
            .Setup(repo => repo.Find(classId))
            .Returns(classEntity);

        _attributeService.AsociateAtributeToClass(classId, attributeDTO);

        Assert.AreEqual(1, classEntity.Attributes.Count);
        Assert.AreEqual(attributeDTO.Name, classEntity.Attributes[0].Name);
    }

    [TestMethod]
    public void AsociateAtributeToClass_ShouldThrowException_WhenAttributeNameAlreadyExistsInClass()
    {
        var classId = Guid.NewGuid();
        var existingAttribute = new Attribute(Guid.NewGuid(), "Intelligence", Accessibility.Private);

        var classEntity = new Class
        {
            Id = classId,
            Name = "Mage",
            Attributes = new List<Attribute> { existingAttribute }
        };

        var dto = new AttributeDTO
        {
            Id = Guid.NewGuid(),
            Name = "Intelligence",
            Accessibility = Accessibility.Public
        };

        _mockClassRepository
            .Setup(repo => repo.Find(classId))
            .Returns(classEntity);

        var ex = Assert.ThrowsException<AttributeException>(() =>
            _attributeService.AsociateAtributeToClass(classId, dto));

        Assert.AreEqual("An attribute with the name 'Intelligence' already exists in the class", ex.Message);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowException_WhenClassNotFound()
    {
        var dto = new AttributeDTO
        {
            Id = Guid.NewGuid(),
            Name = "Agility",
            Accessibility = Accessibility.Protected
        };

        _mockClassRepository
            .Setup(repo => repo.Find(Guid.Empty))
            .Returns((Class?)null);

        var exception = Assert.ThrowsException<AttributeException>(() =>
        {
            _attributeService.AsociateAtributeToClass(Guid.Empty, dto);
        });

        Assert.AreEqual("Class not found", exception.Message);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowArgumentException_WhenNameIsEmpty()
    {
        var dto = new AttributeDTO
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Accessibility = Accessibility.Public
        };

        var classEntity = new Class
        {
            Id = _classId,
            Name = "SomeClass",
            Attributes = new List<Attribute>()
        };

        _mockClassRepository
            .Setup(repo => repo.Find(_classId))
            .Returns(classEntity);

        var ex = Assert.ThrowsException<ArgumentException>(() =>
        {
            _attributeService.CreateAttribute(dto);
        });

        Assert.AreEqual("Name cannot be null or empty", ex.Message);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowArgumentException_WhenNameExceedsMaxLength()
    {
        var longName = new string('A', 51);
        var dto = new AttributeDTO
        {
            Id = Guid.NewGuid(),
            Name = longName,
            Accessibility = Accessibility.Public
        };

        var classEntity = new Class
        {
            Id = _classId,
            Name = "SomeClass",
            Attributes = new List<Attribute>()
        };

        _mockClassRepository
            .Setup(repo => repo.Find(_classId))
            .Returns(classEntity);

        var ex = Assert.ThrowsException<ArgumentException>(() =>
        {
            _attributeService.CreateAttribute(dto);
        });

        Assert.AreEqual("Name cannot exceed 50 characters", ex.Message);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldUpdateAttribute_WhenValid()
    {
        var id = Guid.NewGuid();
        var original = new Attribute(id, "OldName", Accessibility.Private);
        var dto = new AttributeDTO
        {
            Id = id,
            Name = "NewName",
            Accessibility = Accessibility.Public
        };

        _mockAttributeRepository
            .Setup(r => r.Find(id))
            .Returns(original);

        _mockAttributeRepository
            .Setup(r => r.Update(It.IsAny<Attribute>()));

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns(new List<Class>());

        _attributeService.UpdateAttribute(id, dto);

        Assert.AreEqual("NewName", original.Name);
        Assert.AreEqual(Accessibility.Public, original.Accessibility);
        _mockAttributeRepository.Verify(r => r.Update(original), Times.Once);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldThrowAttributeException_WhenAttributeIsInClass()
    {
        var id = Guid.NewGuid();
        var attribute = new Attribute(id, "SomeName", Accessibility.Private);
        var dto = new AttributeDTO
        {
            Id = id,
            Name = "UpdatedName",
            Accessibility = Accessibility.Public
        };

        var classWithAttribute = new Class
        {
            Id = Guid.NewGuid(),
            Name = "ClassA",
            Attributes = new List<Attribute> { attribute }
        };

        _mockAttributeRepository
            .Setup(r => r.Find(id))
            .Returns(attribute);

        _mockClassRepository
            .Setup(r => r.FindAll())
            .Returns(new List<Class> { classWithAttribute });

        var exception = Assert.ThrowsException<AttributeException>(() =>
        {
            _attributeService.UpdateAttribute(id, dto);
        });

        Assert.AreEqual("Attribute is associated with a class", exception.Message);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldThrowAttributeException_WhenAttributeDoesNotExist()
    {
        var id = Guid.NewGuid();
        var dto = new AttributeDTO
        {
            Id = id,
            Name = "NewName",
            Accessibility = Accessibility.Private
        };

        _mockAttributeRepository
            .Setup(r => r.Find(id))
            .Returns((Attribute)null);

        var exception = Assert.ThrowsException<AttributeException>(() =>
        {
            _attributeService.UpdateAttribute(id, dto);
        });

        Assert.AreEqual("Attribute not found", exception.Message);
    }

    [TestMethod]
    public void DeleteAttribute_ShouldDeleteAttribute_WhenAttributeIsValidAndNotInClass()
    {
        var id = Guid.NewGuid();
        var attribute = new Attribute { Id = id, Name = "Test", Accessibility = Accessibility.Public };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns(new List<Simulator.Domain.Class>()); // No asociado a clases
        _mockAttributeRepository.Setup(r => r.Delete(id));

        _attributeService.DeleteAttribute(id);

        _mockAttributeRepository.Verify(r => r.Delete(id), Times.Once);
    }
}
