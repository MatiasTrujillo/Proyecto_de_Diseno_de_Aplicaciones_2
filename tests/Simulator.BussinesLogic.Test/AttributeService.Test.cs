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
    private Mock<IInvokedMethodRepository> _mockInvokeMethodRepository = null!;
    private AttributeService _attributeService = null!;
    private Guid _classId;

    [TestInitialize]
    public void Initialize()
    {
        _mockAttributeRepository = new Mock<IAttributeRepository>(MockBehavior.Strict);
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);
        _mockInvokeMethodRepository = new Mock<IInvokedMethodRepository>(MockBehavior.Strict);

        _mockAttributeRepository
            .Setup(repo => repo.Add(It.IsAny<Attribute>()));
        _mockAttributeRepository
            .Setup(repo => repo.Update(It.IsAny<Attribute>()));

        _attributeService = new AttributeService(_mockAttributeRepository.Object, _mockClassRepository.Object,
            _mockInvokeMethodRepository.Object);
        _classId = Guid.NewGuid();
    }

    [TestMethod]
    public void CreateAttribute_ShouldCallRepositoryAdd_WithValidAttribute()
    {
        var dto = new AttributeDto
        {
            Name = "Agility",
            Accessibility = Accessibility.Public,
            ReferenceType = new TypeDTO
            {
                Name = "TestType"
            },
            InstanceType = new TypeDTO
            {
                Name = "TestType"
            }
        };

        var attribute = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Strength",
            Accessibility = Accessibility.Private,
            ReferenceType = new Type { Name = dto.ReferenceType.Name },
            InstanceType = new Type { Name = dto.ReferenceType.Name }
        };

        _mockClassRepository
            .Setup(repo => repo.FindAll())
            .Returns([new Class { Name = dto.ReferenceType.Name }]);

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = dto.ReferenceType.Name }]);

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
                a.ReferenceType != null &&
                a.ReferenceType.Name == dto.ReferenceType.Name)), Times.Once);
    }

    [TestMethod]
    public void AsociateAtributeToClass_ShouldAddAttribute_WhenValid()
    {
        var classId = Guid.NewGuid();
        var attributeId = Guid.NewGuid();

        var attribute = new Attribute { Id = attributeId, Name = "Agility", Accessibility = Accessibility.Public };

        var classEntity = new Class { Id = classId, Name = "Warrior", Attributes = [] };

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

        var classEntity = new Class { Id = classId, Name = "Mage", Attributes = [existingAttribute] };

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
            ReferenceType = new TypeDTO { Name = "TestType" },
            InstanceType = new TypeDTO { Name = "TestType" }
        };

        var attribute = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Agility",
            Accessibility = Accessibility.Public,
            ReferenceType = new Type { Name = dto.ReferenceType.Name },
            InstanceType = new Type { Name = dto.ReferenceType.Name }
        };

        _mockClassRepository
            .Setup(repo => repo.FindAll())
            .Returns([new Class { Name = dto.ReferenceType.Name }]);

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([]);

        _mockAttributeRepository
            .Setup(repo => repo.Find(It.IsAny<Guid>()))
            .Returns(attribute);

        var exception = Assert.ThrowsException<TypeNotFoundException>(() =>
            _attributeService.CreateAttribute(dto));

        Assert.AreEqual("Return type not found.", exception.Message);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowException_WhenInstanceTypeNotSubclassOfReferenceType()
    {
        var aClass1 = new Class { Id = Guid.NewGuid(), Name = "class1" };
        var aClass2 = new Class { Id = Guid.NewGuid(), Name = "class2" };
        var dto = new AttributeDto
        {
            Name = "Agility",
            Accessibility = Accessibility.Public,
            ReferenceType = new TypeDTO { Name = aClass1.Name },
            InstanceType = new TypeDTO { Name = aClass2.Name },
        };

        var attribute = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Agility",
            Accessibility = Accessibility.Public,
            ReferenceType = new Type { Name = dto.ReferenceType.Name },
            InstanceType = new Type { Name = dto.InstanceType.Name }
        };

        _mockClassRepository
            .Setup(repo => repo.FindAll())
            .Returns([aClass1, aClass2]);

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([aClass1, aClass2]);

        var exception = Assert.ThrowsException<InvalidAttributeException>(() =>
            _attributeService.CreateAttribute(dto));

        Assert.AreEqual("Instance Type is not a subclass of Reference Type", exception.Message);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowException_WhenInstanceTypeNotSubclassOfReferenceType2()
    {
        var aClass3 = new Class { Id = Guid.NewGuid(), Name = "class3" };
        var aClass1 = new Class { Id = Guid.NewGuid(), Name = "class1" };
        var aClass2 = new Class { Id = Guid.NewGuid(), Name = "class2", HierarchyClass = aClass3 };
        var dto = new AttributeDto
        {
            Name = "Agility",
            Accessibility = Accessibility.Public,
            ReferenceType = new TypeDTO { Name = aClass1.Name },
            InstanceType = new TypeDTO { Name = aClass2.Name },
        };

        _mockClassRepository
            .Setup(repo => repo.FindAll())
            .Returns([aClass1, aClass2]);

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([aClass1, aClass2]);

        var exception = Assert.ThrowsException<InvalidAttributeException>(() =>
            _attributeService.CreateAttribute(dto));

        Assert.AreEqual("Instance Type is not a subclass of Reference Type", exception.Message);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowArgumentException_WhenNameIsEmpty()
    {
        var dto = new AttributeDto
        {
            Name = string.Empty,
            Accessibility = Accessibility.Public,
            ReferenceType = new TypeDTO { Name = "TestType" },
            InstanceType = new TypeDTO { Name = "TestType" }
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

        var ex = Assert.ThrowsException<AttributeException>(() =>
        {
            _attributeService.CreateAttribute(dto);
        });

        Assert.AreEqual("Name cannot be null or empty", ex.Message);
    }

    [DataTestMethod]
    [DataRow("base")]
    [DataRow("this")]
    [DataRow("Object")]
    public void CreateAttribute_ShouldThrowAttributeException_WhenNameIsReservedKeyword(string reservedName)
    {
        var dto = new AttributeDto
        {
            Name = reservedName,
            Accessibility = Accessibility.Public,
            ReferenceType = new TypeDTO { Name = "TestType" },
            InstanceType = new TypeDTO { Name = "TestType" }
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

        var ex = Assert.ThrowsException<AttributeException>(() =>
        {
            _attributeService.CreateAttribute(dto);
        });

        Assert.AreEqual("The name is a reserved keyword and is not allowed.", ex.Message);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowArgumentException_WhenNameExceedsMaxLength()
    {
        var longName = new string('A', 51);
        var dto = new AttributeDto
        {
            Name = longName,
            Accessibility = Accessibility.Public,
            ReferenceType = new TypeDTO { Name = "TestType" },
            InstanceType = new TypeDTO { Name = "TestType" }
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

        var ex = Assert.ThrowsException<AttributeException>(() =>
        {
            _attributeService.CreateAttribute(dto);
        });

        Assert.AreEqual("Name cannot exceed 50 characters", ex.Message);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowInvalidAttributeException_WhenAccesibilityIsWrong()
    {
        var invalidAccessibility = (Accessibility)99;
        var dto = new AttributeDto
        {
            Name = "Name",
            Accessibility = invalidAccessibility,
            ReferenceType = new TypeDTO { Name = "SomeClass" },
            InstanceType = new TypeDTO { Name = "SomeClass" }
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
        _mockClassRepository
            .Setup(repo => repo.FindAll())
            .Returns([]);

        var ex = Assert.ThrowsException<InvalidAttributeException>(() =>
        {
            _attributeService.CreateAttribute(dto);
        });

        Assert.AreEqual("Invalid accessibility value.", ex.Message);
    }

    [TestMethod]
    public void DeleteAttribute_ShouldDeleteAttribute_WhenAttributeIsValidAndNotInClass()
    {
        var id = Guid.NewGuid();
        var attribute = new Attribute { Id = id, Name = "Test", Accessibility = Accessibility.Public };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([]);
        _mockAttributeRepository.Setup(r => r.Delete(id));

        _attributeService.DeleteAttribute(id);

        _mockAttributeRepository.Verify(r => r.Delete(id), Times.Once);
    }

    [TestMethod]
    public void DeleteAttribute_ShouldThrowInvalidAttributeException_WhenAttributeDoesNotExist()
    {
        var id = Guid.NewGuid();
        _mockAttributeRepository.Setup(r => r.Find(id)).Returns((Attribute)null);

        var ex = Assert.ThrowsException<AttributeNotFoundException>(() =>
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

        var ex = Assert.ThrowsException<AttributeInUseExpetion>(() =>
        {
            _attributeService.DeleteAttribute(id);
        });

        Assert.AreEqual("Attribute is associated with a class", ex.Message);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldUpdate_WhenDataIsValid()
    {
        var attributeId = Guid.NewGuid();
        var referenceTypeId = Guid.NewGuid();
        var instanceTypeId = Guid.NewGuid();
        var attribute = new Attribute { Id = attributeId, Name = "OldName", Accessibility = Accessibility.Private };
        var dto = new UpdateAttributeDTO { Name = "NewName", Accessibility = "public", IdReferenceType = referenceTypeId, IdInstanceType = instanceTypeId };
        var auxClass1 = new Class { Id = referenceTypeId, Name = "class1" };
        var auxClass2 = new Class { Id = instanceTypeId, Name = "class2", HierarchyClass = auxClass1 };

        _mockAttributeRepository.Setup(r => r.Find(attributeId)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([auxClass1, auxClass2]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([auxClass1, auxClass2]);
        _mockAttributeRepository.Setup(r => r.Update(attribute));

        _attributeService.UpdateAttribute(attributeId, dto);

        _mockAttributeRepository.Verify(r => r.Update(attribute), Times.Once);
        Assert.AreEqual("NewName", attribute.Name);
        Assert.AreEqual(Accessibility.Public, attribute.Accessibility);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldThrowException_WhenInstanceTypeNotSubclassOfReferenceType()
    {
        var attributeId = Guid.NewGuid();
        var referenceTypeId = Guid.NewGuid();
        var instanceTypeId = Guid.NewGuid();
        var attribute = new Attribute { Id = attributeId, Name = "OldName", Accessibility = Accessibility.Private };
        var dto = new UpdateAttributeDTO { Name = "NewName", Accessibility = "public", IdReferenceType = referenceTypeId, IdInstanceType = instanceTypeId };
        var auxClass1 = new Class { Id = referenceTypeId, Name = "class1" };
        var auxClass2 = new Class { Id = instanceTypeId, Name = "class2" };

        _mockAttributeRepository.Setup(r => r.Find(attributeId)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([auxClass1, auxClass2]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([auxClass1, auxClass2]);
        _mockAttributeRepository.Setup(r => r.Update(attribute));

        var ex = Assert.ThrowsException<InvalidAttributeException>(() =>
            _attributeService.UpdateAttribute(attributeId, dto));

        Assert.AreEqual("Instance Type is not a subclass of Reference Type", ex.Message);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldThrow_WhenNameIsEmpty()
    {
        var id = Guid.NewGuid();
        var attribute = new Attribute { Id = id, Name = "Attribute" };
        var dto = new UpdateAttributeDTO { Name = string.Empty, Accessibility = "private" };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);

        var ex = Assert.ThrowsException<AttributeException>(() =>
            _attributeService.UpdateAttribute(id, dto));

        Assert.AreEqual("Name cannot be empty", ex.Message);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldThrow_WhenNameIsTooLong()
    {
        var id = Guid.NewGuid();
        var attribute = new Attribute { Id = id, Name = "Attr" };
        var dto = new UpdateAttributeDTO { Name = new string('X', 51), Accessibility = "private" };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);

        var ex = Assert.ThrowsException<AttributeException>(() =>
            _attributeService.UpdateAttribute(id, dto));

        Assert.AreEqual("Name cannot exceed 50 characters", ex.Message);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldThrow_WhenAttributeIsInClass()
    {
        var id = Guid.NewGuid();
        var attribute = new Attribute { Id = id, Name = "Attr", Accessibility = Accessibility.Private };
        var classWithAttr = new Class { Attributes = [attribute] };
        var dto = new UpdateAttributeDTO { Name = "Name", Accessibility = "public" };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([classWithAttr]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([classWithAttr]);

        var ex = Assert.ThrowsException<AttributeInUseExpetion>(() =>
            _attributeService.UpdateAttribute(id, dto));

        Assert.AreEqual("Attribute is associated with a class", ex.Message);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldThrow_WhenAttributeNotFound()
    {
        var id = Guid.NewGuid();
        var dto = new UpdateAttributeDTO { Name = "Name", Accessibility = "public" };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns((Attribute)null!);

        var ex = Assert.ThrowsException<AttributeNotFoundException>(() =>
            _attributeService.UpdateAttribute(id, dto));

        Assert.AreEqual("Attribute not found", ex.Message);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldUpdateAccessibility_WhenDataIsValid()
    {
        var id = Guid.NewGuid();
        var referenceTypeId = Guid.NewGuid();
        var instanceTypeId = Guid.NewGuid();
        var existingAttribute = new Attribute { Id = id, Name = "Attr", Accessibility = Accessibility.Private };

        var dto = new UpdateAttributeDTO { Name = "Attr", Accessibility = "protected", IdReferenceType = referenceTypeId, IdInstanceType = instanceTypeId };
        var auxClass1 = new Class { Id = referenceTypeId, Name = "class1" };
        var auxClass2 = new Class { Id = instanceTypeId, Name = "class2", HierarchyClass = auxClass1 };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(existingAttribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([auxClass1, auxClass2]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([auxClass1, auxClass2]);

        _attributeService.UpdateAttribute(id, dto);

        Assert.AreEqual(Accessibility.Protected, existingAttribute.Accessibility);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldUpdateAccessibility_WhenDataIsValidPrivate()
    {
        var id = Guid.NewGuid();
        var referenceTypeId = Guid.NewGuid();
        var instanceTypeId = Guid.NewGuid();
        var existingAttribute = new Attribute { Id = id, Name = "Attr", Accessibility = Accessibility.Private };

        var dto = new UpdateAttributeDTO { Name = "Attr", Accessibility = "private", IdReferenceType = referenceTypeId, IdInstanceType = instanceTypeId };
        var auxClass1 = new Class { Id = referenceTypeId, Name = "class1" };
        var auxClass2 = new Class { Id = instanceTypeId, Name = "class2", HierarchyClass = auxClass1 };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(existingAttribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([auxClass1, auxClass2]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([auxClass1, auxClass2]);

        _attributeService.UpdateAttribute(id, dto);

        Assert.AreEqual(Accessibility.Private, existingAttribute.Accessibility);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldUpdateAccessibility_WhenDataIsValidPublic()
    {
        var id = Guid.NewGuid();
        var referenceTypeId = Guid.NewGuid();
        var instanceTypeId = Guid.NewGuid();
        var existingAttribute = new Attribute { Id = id, Name = "Attr", Accessibility = Accessibility.Private };

        var dto = new UpdateAttributeDTO { Name = "Attr", Accessibility = "public", IdReferenceType = referenceTypeId, IdInstanceType = instanceTypeId };
        var auxClass1 = new Class { Id = referenceTypeId, Name = "class1" };
        var auxClass2 = new Class { Id = instanceTypeId, Name = "class2", HierarchyClass = auxClass1 };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(existingAttribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([auxClass1, auxClass2]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([auxClass1, auxClass2]);

        _attributeService.UpdateAttribute(id, dto);

        Assert.AreEqual(Accessibility.Public, existingAttribute.Accessibility);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldThrowException_WhenDataIsValidAccesibilityWrong()
    {
        var id = Guid.NewGuid();
        var existingAttribute = new Attribute { Id = id, Name = "Attr", Accessibility = Accessibility.Private };

        var dto = new UpdateAttributeDTO { Name = "Attr", Accessibility = "test" };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(existingAttribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);
        var ex = Assert.ThrowsException<InvalidAttributeException>(() => _attributeService.UpdateAttribute(id, dto));

        Assert.AreEqual("Invalid accessibility type", ex.Message);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldThrow_WhenIdTypeNotFound()
    {
        var id = Guid.NewGuid();
        var fakeTypeId = Guid.NewGuid();
        var attribute = new Attribute { Id = id, Name = "Attr" };
        var dto = new UpdateAttributeDTO { Name = "ValidName", Accessibility = "public", IdReferenceType = fakeTypeId };

        _mockAttributeRepository.Setup(r => r.Find(id)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([new Class { Id = Guid.NewGuid() }]);

        var ex = Assert.ThrowsException<TypeNotFoundException>(() =>
            _attributeService.UpdateAttribute(id, dto));

        Assert.AreEqual("Return type not found.", ex.Message);
    }

    [TestMethod]
    public void SetInvokeMethod_ShouldAssignInvokedMethodAndUpdateAttribute()
    {
        var attributeId = Guid.NewGuid();
        var invokeId = Guid.NewGuid();

        var attribute = new Attribute { Id = attributeId, Name = "attr1" };
        var invoked = new InvokedMethod { Id = invokeId, ReferenceName = "refName" };

        _mockAttributeRepository.Setup(r => r.Find(attributeId)).Returns(attribute);
        _mockInvokeMethodRepository.Setup(r => r.Find(invokeId)).Returns(invoked);

        _attributeService.SetInvokeMethodToAttribute(attributeId, invokeId);

        Assert.AreEqual(invoked, attribute.InvokedMethod);
        _mockAttributeRepository.Verify(r => r.Update(attribute), Times.Once);
    }

    [TestMethod]
    public void CreateAttribute_ShouldCreateAttributeWithStereotype_WithCorrectInput()
    {
        var dto = new AttributeDto
        {
            Name = "Agility",
            Accessibility = Accessibility.Public,
            ReferenceType = new TypeDTO
            {
                Name = "TestType"
            },
            InstanceType = new TypeDTO
            {
                Name = "TestType"
            },
            Stereotype = KeyWord.Static
        };

        var attribute = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Strength",
            Accessibility = Accessibility.Private,
            ReferenceType = new Type { Name = dto.ReferenceType.Name },
            InstanceType = new Type { Name = dto.ReferenceType.Name },
            Stereotype = KeyWord.Static
        };

        _mockClassRepository
            .Setup(repo => repo.FindAll())
            .Returns([new Class { Name = dto.ReferenceType.Name }]);

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = dto.ReferenceType.Name }]);

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
                a.ReferenceType != null &&
                a.ReferenceType.Name == dto.ReferenceType.Name)), Times.Once);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowException_WithInvalidStereotype()
    {
        var dto = new AttributeDto
        {
            Name = "Agility",
            Accessibility = Accessibility.Public,
            ReferenceType = new TypeDTO
            {
                Name = "TestType"
            },
            InstanceType = new TypeDTO
            {
                Name = "TestType"
            },
            Stereotype = KeyWord.Abstract
        };

        var attribute = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "Strength",
            Accessibility = Accessibility.Private,
            ReferenceType = new Type { Name = dto.ReferenceType.Name },
            InstanceType = new Type { Name = dto.ReferenceType.Name },
            Stereotype = KeyWord.Static
        };

        _mockClassRepository
            .Setup(repo => repo.FindAll())
            .Returns([new Class { Name = dto.ReferenceType.Name }]);

        _mockClassRepository
            .Setup(repo => repo.FindAllTypes())
            .Returns([new Class { Name = dto.ReferenceType.Name }]);

        _mockAttributeRepository
            .Setup(repo => repo.Find(It.IsAny<Guid>()))
            .Returns(attribute);

        _mockAttributeRepository
            .Setup(repo => repo.Add(It.IsAny<Attribute>()));

        var ex = Assert.ThrowsException<InvalidAttributeException>(() => _attributeService.CreateAttribute(dto));

        Assert.AreEqual("Invalid stereotype value. Attribute stereotype can only be null or static.", ex.Message);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldUpdate_WhenStereotypeIsCorrect()
    {
        var attributeId = Guid.NewGuid();
        var referenceTypeId = Guid.NewGuid();
        var instanceTypeId = Guid.NewGuid();
        var attribute = new Attribute { Id = attributeId, Name = "OldName", Accessibility = Accessibility.Private };
        var dto = new UpdateAttributeDTO { Name = "NewName", Accessibility = "public", Stereotype = "static", IdReferenceType = referenceTypeId, IdInstanceType = instanceTypeId };
        var auxClass1 = new Class { Id = referenceTypeId, Name = "class1" };
        var auxClass2 = new Class { Id = instanceTypeId, Name = "class2", HierarchyClass = auxClass1 };

        _mockAttributeRepository.Setup(r => r.Find(attributeId)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([auxClass1, auxClass2]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([auxClass1, auxClass2]);
        _mockAttributeRepository.Setup(r => r.Update(attribute));

        _attributeService.UpdateAttribute(attributeId, dto);

        _mockAttributeRepository.Verify(r => r.Update(attribute), Times.Once);
        Assert.AreEqual("NewName", attribute.Name);
        Assert.AreEqual(Accessibility.Public, attribute.Accessibility);
        Assert.AreEqual(KeyWord.Static, attribute.Stereotype);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldThrowException_WhenStereotypeIsWrong()
    {
        var attributeId = Guid.NewGuid();
        var typeId = Guid.NewGuid();
        var attribute = new Attribute { Id = attributeId, Name = "OldName", Accessibility = Accessibility.Private };
        var dto = new UpdateAttributeDTO { Name = "NewName", Accessibility = "public", IdReferenceType = typeId, Stereotype = "abstract" };

        _mockAttributeRepository.Setup(r => r.Find(attributeId)).Returns(attribute);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([new Class { Id = typeId }]);
        _mockAttributeRepository.Setup(r => r.Update(attribute));

        var ex = Assert.ThrowsException<InvalidAttributeException>(() => _attributeService.UpdateAttribute(attributeId, dto));

        Assert.AreEqual("Invalid stereotype value. Attribute stereotype can only be null or static.", ex.Message);
    }
}
