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
        _mockAttributeRepository
            .Setup(repo => repo.Update(It.IsAny<Attribute>()));

        _attributeService = new AttributeService(_mockAttributeRepository.Object, _mockClassRepository.Object);
        _classId = Guid.NewGuid();
    }

    [TestMethod]
    public void CreateAttribute_ShouldCallRepositoryAdd_WithValidAttribute()
    {
        var dto = new AttributeDto
        {
            Name = "Agility",
            Accessibility = Accessibility.Public,
            Type = new TypeDTO { Name = "TestType" }
        };

        var attribute = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Strength",
            Accessibility = Accessibility.Private,
            Type = new Type { Name = dto.Type.Name }
        };

        _mockClassRepository
            .Setup(repo => repo.FindAll())
            .Returns([new Class { Name = dto.Type.Name }]);

        _mockAttributeRepository
            .Setup(repo => repo.Find(It.IsAny<Guid>()))
            .Returns(attribute);

        _mockAttributeRepository
            .Setup(repo => repo.Add(It.IsAny<Attribute>()));

        _attributeService.CreateAttribute(dto);

        _mockAttributeRepository.Verify(repo => repo.Add(
            It.Is<Attribute>(a =>
                a.Name == dto.Name &&
                a.Accessibility == dto.Accessibility &&
                a.Type != null &&
                a.Type.Name == dto.Type.Name)), Times.Once);
    }

    [TestMethod]
    public void AsociateAtributeToClass_ShouldAddAttribute_WhenValid()
    {
        var classId = Guid.NewGuid();
        var attributeId = Guid.NewGuid();

        var attribute = new Attribute
        {
            Id = attributeId,
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
            .Setup(repo => repo.Find(It.IsAny<Guid>()))
            .Returns(classEntity);

        _mockAttributeRepository
            .Setup(repo => repo.Find(It.IsAny<Guid>()))
            .Returns(attribute);

        _mockClassRepository
            .Setup(repo => repo.Update(It.IsAny<Class>()));

        _attributeService.AsociateAttribute(classId, attributeId);

        Assert.AreEqual(1, classEntity.Attributes.Count);
        Assert.AreEqual(attribute.Name, classEntity.Attributes[0].Name);

        _mockClassRepository.Verify(repo => repo.Update(classEntity), Times.Once);
    }

    [TestMethod]
    public void AsociateAtributeToClass_ShouldThrowException_WhenAttributeNameAlreadyExistsInClass()
    {
        var classId = Guid.NewGuid();
        var existingAttribute = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Agility",
            Accessibility = Accessibility.Public
        };

        var classEntity = new Class
        {
            Id = classId,
            Name = "Mage",
            Attributes = [existingAttribute]
        };

        var newAttribute = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Agility",
            Accessibility = Accessibility.Public
        };

        _mockClassRepository
            .Setup(repo => repo.Find(It.IsAny<Guid>()))
            .Returns(classEntity);

        _mockAttributeRepository
            .Setup(repo => repo.Find(It.IsAny<Guid>()))
            .Returns(newAttribute);

        var ex = Assert.ThrowsException<InvalidAttributeException>(() =>
            _attributeService.AsociateAttribute(classId, newAttribute.Id));

        Assert.AreEqual("An attribute with the name 'Agility' already exists in the class", ex.Message);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowException_WhenClassNotFound()
    {
        var dto = new AttributeDto
        {
            Name = "Agility",
            Accessibility = Accessibility.Public,
            Type = new TypeDTO { Name = "TestType" }
        };

        var attribute = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Agility",
            Accessibility = Accessibility.Public,
            Type = new Type { Name = dto.Type.Name }
        };

        _mockClassRepository
            .Setup(repo => repo.FindAll())
            .Returns(new List<Class>());

        _mockAttributeRepository
            .Setup(repo => repo.Find(It.IsAny<Guid>()))
            .Returns(attribute);

        var exception = Assert.ThrowsException<MethodException>(() =>
            _attributeService.CreateAttribute(dto));

        Assert.AreEqual("Return type not found.", exception.Message);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowArgumentException_WhenNameIsEmpty()
    {
        var dto = new AttributeDto
        {
            Name = string.Empty,
            Accessibility = Accessibility.Public
        };

        var classEntity = new Class
        {
            Id = _classId,
            Name = "SomeClass",
            Attributes = []
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
        var dto = new AttributeDto
        {
            Name = longName,
            Accessibility = Accessibility.Public
        };

        var classEntity = new Class
        {
            Id = _classId,
            Name = "SomeClass",
            Attributes = []
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
    public void UpdateAttributeName_ShouldUpdateAttribute_WhenValid()
    {
        var id = Guid.NewGuid();
        var original = new Attribute("OldName", Accessibility.Private, null);
        var dto = new AttributeDto
        {
            Name = "NewName",
            Accessibility = Accessibility.Public
        };

        _mockAttributeRepository
            .Setup(r => r.Find(id))
            .Returns(original);

        _mockAttributeRepository
            .Setup(r => r.Update(It.IsAny<Attribute>()));

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns([]);

        _attributeService.UpdateAttributeName(id, dto.Name);

        Assert.AreEqual("NewName", original.Name);
        _mockAttributeRepository.Verify(r => r.Update(original), Times.Once);
    }

    [TestMethod]
    public void UpdateAttributeAccessibility_ShouldUpdateAttribute_WhenValid()
    {
        var id = Guid.NewGuid();
        var original = new Attribute("OldName", Accessibility.Private, null);
        var dto = new AttributeDto
        {
            Name = "NewName",
            Accessibility = Accessibility.Public
        };

        _mockAttributeRepository
            .Setup(r => r.Find(id))
            .Returns(original);

        _mockAttributeRepository
            .Setup(r => r.Update(It.IsAny<Attribute>()));

        _mockClassRepository.Setup(r => r.FindAll())
            .Returns([]);

        _attributeService.UpdateAttributeAccessibility(id, "Public");

        Assert.AreEqual(Accessibility.Public, original.Accessibility);
        _mockAttributeRepository.Verify(r => r.Update(original), Times.Once);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldThrowAttributeException_WhenAttributeDoesNotExist()
    {
        var id = Guid.NewGuid();
        var dto = new AttributeDto
        {
            Name = "NewName",
            Accessibility = Accessibility.Private
        };

        _mockAttributeRepository
            .Setup(r => r.Find(id))
            .Returns((Attribute)null);

        var exception = Assert.ThrowsException<InvalidAttributeException>(() =>
        {
            _attributeService.UpdateAttributeName(id, dto.Name);
        });

        Assert.AreEqual("Attribute not found", exception.Message);
    }

    [TestMethod]
    public void DeleteAttribute_ShouldDeleteAttribute_WhenAttributeIsValidAndNotInClass()
    {
        var id = Guid.NewGuid();
        var attribute = new Attribute { Id = id, Name = "Test", Accessibility = Accessibility.Public };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);
        _mockAttributeRepository.Setup(r => r.Delete(id));

        _attributeService.DeleteAttribute(id);

        _mockAttributeRepository.Verify(r => r.Delete(id), Times.Once);
    }

    [TestMethod]
    public void DeleteAttribute_ShouldThrowInvalidAttributeException_WhenAttributeDoesNotExist()
    {
        var id = Guid.NewGuid();
        _mockAttributeRepository.Setup(r => r.Find(id)).Returns((Attribute)null);

        var ex = Assert.ThrowsException<InvalidAttributeException>(() =>
        {
            _attributeService.DeleteAttribute(id);
        });

        Assert.AreEqual("Attribute not found", ex.Message);
    }

    [TestMethod]
    public void DeleteAttribute_ShouldThrowAttributeException_WhenAttributeIsInClass()
    {
        var id = Guid.NewGuid();
        var attribute = new Attribute { Id = id, Name = "InUse", Accessibility = Accessibility.Public };
        var classWithAttribute = new Simulator.Domain.Class { Attributes = [attribute] };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classWithAttribute]);

        var ex = Assert.ThrowsException<InvalidAttributeException>(() =>
        {
            _attributeService.DeleteAttribute(id);
        });

        Assert.AreEqual("Attribute is associated with a class", ex.Message);
    }
}
