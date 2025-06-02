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
    private Mock<IInterfaceRepository> _mockInterfaceRepository = null!;
    private ClassService _classService = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);
        _mockAttributeRepository = new Mock<IAttributeRepository>(MockBehavior.Strict);
        _mockMethodRepository = new Mock<IMethodRepository>(MockBehavior.Strict);
        _mockInterfaceRepository = new Mock<IInterfaceRepository>(MockBehavior.Strict);
        _mockClassRepository
            .Setup(repo => repo.Add(It.IsAny<Class>()));
        _mockAttributeRepository.Setup(repo => repo.Add(It.IsAny<Attribute>()));
        _mockMethodRepository.Setup(repo => repo.Add(It.IsAny<Method>()));
        _classService = new ClassService(_mockClassRepository.Object, _mockAttributeRepository.Object,
            _mockMethodRepository.Object, _mockInterfaceRepository.Object);
    }

    [TestMethod]
    public void CreateClass_ShouldCallRepositoryAdd_WithValidClass()
    {
        var dto = new ClassDTO { Name = "Pedro", Stereotype = KeyWord.Sealed, };

        var objectClass = new Class { Name = "Object" };
        var existingClasses = new List<Type> { objectClass };

        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns(existingClasses);

        _classService.CreateClass(dto);

        _mockClassRepository.Verify(repo => repo.Add(
            It.Is<Class>(a =>
                a.Name == dto.Name &&
                a.Stereotype == dto.Stereotype &&
                a.Parents.Contains(objectClass) &&
                a.Parents.Count == 1)), Times.Once);
    }

    [TestMethod]
    public void CreateClass_ShouldNotCallRepositoryAdd_WithRepeatedClassName()
    {
        var dto1 = new ClassDTO { Name = "Pedro", Stereotype = KeyWord.Sealed };

        var dto2 = new ClassDTO { Name = "Pedro", Stereotype = null };

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
        var dto = new ClassDTO { Name = "Class", Stereotype = invalidStereoType };

        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.CreateClass(dto);
        });

        Assert.AreEqual("Invalid stereotype value. Classes stereotypes can only be null, abstract or sealed.",
            exception.Message);
    }

    [TestMethod]
    public void CreateClass_ShouldThrowException_WhenNameIsWhitespace()
    {
        var dto = new ClassDTO { Name = string.Empty, Stereotype = KeyWord.Sealed };

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
        var dto = new ClassDTO { Name = "class", Stereotype = KeyWord.Sealed };

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
        var dto = new ClassDTO { Name = "Class Name", Stereotype = KeyWord.Sealed };

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

        var dto = new UpdateClassDTO { Name = "NewName", IdClass = heritageId.ToString() };

        _classService.UpdateClass(classId, dto);

        Assert.AreEqual("NewName", existingClass.Name);
        Assert.AreEqual(newHeritageClass, existingClass.Parents[0]);
    }

    [TestMethod]
    public void UpdateClass_WithValidData_UpdatesSuccessfullyVirtual()
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

        var dto = new UpdateClassDTO { Name = "NewName", IdClass = heritageId.ToString(), Stereotype = "virtual" };

        _classService.UpdateClass(classId, dto);

        Assert.AreEqual("NewName", existingClass.Name);
        Assert.AreEqual(newHeritageClass, existingClass.Parents[0]);
    }

    [TestMethod]
    public void UpdateClass_WithValidData_UpdatesSuccessfullyNoChange()
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

        var dto = new UpdateClassDTO { Name = "NewName", IdClass = heritageId.ToString(), Stereotype = "no change" };

        _classService.UpdateClass(classId, dto);

        Assert.AreEqual("NewName", existingClass.Name);
        Assert.AreEqual(newHeritageClass, existingClass.Parents[0]);
    }

    [TestMethod]
    public void UpdateClass_ShouldThrowException_WhenSealedClassTriesToInherit()
    {
        var classId = Guid.NewGuid();
        var heritageId = Guid.NewGuid();

        var sealedClass = new Class { Id = classId, Name = "OldName", Stereotype = KeyWord.Sealed };

        var parentClass = new Class { Id = heritageId, Name = "ParentClass", Stereotype = KeyWord.Sealed };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([sealedClass, parentClass]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([sealedClass, parentClass]);
        _mockClassRepository.Setup(r => r.Find(classId)).Returns(sealedClass);
        _mockClassRepository.Setup(r => r.Find(heritageId)).Returns(parentClass);

        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);

        var dto = new UpdateClassDTO { Name = "NewName", IdClass = heritageId.ToString(), Stereotype = "sealed" };

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.UpdateClass(classId, dto);
        });

        Assert.AreEqual("A sealed class cannot inherit from any class.", exception.Message);
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
        _mockClassRepository.Setup(r => r.Update(It.IsAny<Class>()));

        var dto = new UpdateClassDTO { Name = "NewName", IdClass = heritageId.ToString(), Stereotype = "no change" };

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.UpdateClass(classId, dto);
        });

        Assert.AreEqual("An abstract class cannot inherit from a concrete class.", exception.Message);
    }

    [TestMethod]
    public void UpdateClass_ShouldThrowException_WhenUpdatingObjectClass()
    {
        var objectClassId = Guid.NewGuid();
        var objectClass = new Class { Id = objectClassId, Name = "Object", Stereotype = KeyWord.Abstract };

        var updateDto = new UpdateClassDTO { Name = "NewName", Stereotype = "sealed", };

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

        var dto = new UpdateClassDTO { Name = "I nvalidName" };

        _classService.UpdateClass(id, dto);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidClassException))]
    public void UpdateClass_WithInvalidNameSpaces_ThrowsException()
    {
        var id = Guid.NewGuid();
        var classT = new Class { Id = id, Name = "OldName" };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT]);
        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);

        var dto = new UpdateClassDTO { Name = "inval idName" };

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

        var dto = new UpdateClassDTO { Stereotype = "invalid" };

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

        var classT = new Class { Id = id, Name = "OldName", Parents = null };
        var classInUse = new Class { Id = Guid.NewGuid(), Parents = [new Class { Id = id }] };
        var heritageClass = new Class { Id = heritageId };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT, classInUse, heritageClass]);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([classT, classInUse, heritageClass]);

        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);
        _mockClassRepository.Setup(r => r.Find(heritageId)).Returns(heritageClass);
        _mockClassRepository.Setup(r => r.Update(It.IsAny<Class>()));

        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);

        var dto = new UpdateClassDTO { IdClass = heritageId.ToString() };

        _classService.UpdateClass(id, dto);
    }

    [TestMethod]
    public void CreateClass_ShouldThrowException_WhenKeyWordStatic()
    {
        var invalidStereoType = KeyWord.Static;
        var dto = new ClassDTO { Name = "Class", Stereotype = invalidStereoType };

        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.CreateClass(dto);
        });

        Assert.AreEqual("Invalid stereotype value. Classes stereotypes can only be null, abstract or sealed.",
            exception.Message);
    }

    [TestMethod]
    public void UpdateClass_ShouldThrowException_WhenKeyWordStatic()
    {
        var id = Guid.NewGuid();
        var classT = new Class { Id = id, Name = "OldName" };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT]);
        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);

        var dto = new UpdateClassDTO { Stereotype = KeyWord.Static + string.Empty, };

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.UpdateClass(id, dto);
        });

        Assert.AreEqual("The stereotype is not valid", exception.Message);
    }

    [TestMethod]
    public void UpdateClass_ShouldUpdateStereotypeToSealed()
    {
        var id = Guid.NewGuid();
        var classT = new Class { Id = id, Name = "OldName" };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT]);
        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);
        _mockClassRepository.Setup(r => r.Update(It.IsAny<Class>()));

        var dto = new UpdateClassDTO { Stereotype = "sealed" };

        _classService.UpdateClass(id, dto);

        _mockClassRepository.Verify(r => r.Update(It.Is<Class>(c =>
            c.Id == id)), Times.Once);
    }

    [TestMethod]
    public void UpdateClass_ShouldUpdateStereotypeToAbstract()
    {
        var id = Guid.NewGuid();
        var classT = new Class { Id = id, Name = "OldName" };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT]);
        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);
        _mockClassRepository.Setup(r => r.Update(It.IsAny<Class>()));

        var dto = new UpdateClassDTO { Stereotype = "abstract" };

        _classService.UpdateClass(id, dto);

        _mockClassRepository.Verify(r => r.Update(It.Is<Class>(c =>
            c.Id == id)), Times.Once);
    }

    [TestMethod]
    public void UpdateClass_ShouldUpdateStereotypeToVoid()
    {
        var id = Guid.NewGuid();
        var classT = new Class { Id = id, Name = "OldName" };

        _mockClassRepository.Setup(r => r.FindAll()).Returns([classT]);
        _mockClassRepository.Setup(r => r.Find(id)).Returns(classT);
        _mockClassRepository.Setup(r => r.Update(It.IsAny<Class>()));

        var dto = new UpdateClassDTO { Stereotype = null };

        _classService.UpdateClass(id, dto);

        _mockClassRepository.Verify(r => r.Update(It.Is<Class>(c =>
            c.Id == id)), Times.Once);
    }

    [TestMethod]
    public void CreateClass_ShouldThrowException_WhenKeyWordVirtual()
    {
        var invalidStereoType = KeyWord.Virtual;
        var dto = new ClassDTO { Name = "Class", Stereotype = invalidStereoType };

        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([]);

        var exception = Assert.ThrowsException<InvalidClassException>(() =>
        {
            _classService.CreateClass(dto);
        });

        Assert.AreEqual("Invalid stereotype value. Classes stereotypes can only be null, abstract or sealed.",
            exception.Message);
    }

    [TestMethod]
    public void UpdateClass_WithValidInterface_UpdatesSuccessfullyWithInterfaceParent()
    {
        var classId = Guid.NewGuid();
        var heritageId = Guid.NewGuid();

        var existingClass = new Class { Id = classId, Name = "OldName" };
        var newHeritageInterface = new Interface { Id = heritageId, Name = "ParentInterface" };

        _mockInterfaceRepository.Setup(r => r.Find(heritageId)).Returns(newHeritageInterface);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([existingClass, newHeritageInterface]);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([existingClass]);
        _mockClassRepository.Setup(r => r.Find(classId)).Returns(existingClass);
        _mockInterfaceRepository.Setup(r => r.Find(heritageId)).Returns(newHeritageInterface);
        _mockClassRepository.Setup(r => r.Update(It.IsAny<Class>()));

        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);

        var dto = new UpdateClassDTO { Name = "NewName", IdClass = heritageId.ToString() };

        _classService.UpdateClass(classId, dto);

        Assert.AreEqual("NewName", existingClass.Name);
        Assert.AreEqual(newHeritageInterface, existingClass.Parents[0]);
        Assert.AreEqual(1, existingClass.Parents.Count);
    }

    [TestMethod]
    public void UpdateClass_WithValidParents_UpdatesSuccessfullyWithMultiplesParents()
    {
        var classId = Guid.NewGuid();
        var oneHeritageId = Guid.NewGuid();
        var secondHeritageId = Guid.NewGuid();

        var existingClass = new Class { Id = classId, Name = "OldName" };
        var oneHeritageParent = new Interface { Id = oneHeritageId, Name = "ParentInterface" };
        var secondHeritageParent = new Class { Id = secondHeritageId, Name = "ParentClass" };

        _mockInterfaceRepository.Setup(r => r.Find(oneHeritageId)).Returns(oneHeritageParent);
        _mockClassRepository.Setup(r => r.FindAllTypes())
            .Returns([existingClass, oneHeritageParent, secondHeritageParent]);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([existingClass, secondHeritageParent]);
        _mockClassRepository.Setup(r => r.Find(classId)).Returns(existingClass);
        _mockClassRepository.Setup(r => r.Find(secondHeritageId)).Returns(secondHeritageParent);
        _mockInterfaceRepository.Setup(r => r.Find(oneHeritageId)).Returns(oneHeritageParent);
        _mockClassRepository.Setup(r => r.Update(It.IsAny<Class>()));

        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);

        var dto = new UpdateClassDTO { Name = "NewName", IdClass = oneHeritageId.ToString() };
        var secondDto = new UpdateClassDTO { Name = "NewName", IdClass = secondHeritageId.ToString() };

        _classService.UpdateClass(classId, dto);
        _classService.UpdateClass(classId, secondDto);

        Assert.AreEqual("NewName", existingClass.Name);
        Assert.AreEqual(2, existingClass.Parents.Count);
        Assert.AreEqual(oneHeritageParent, existingClass.Parents[0]);
        Assert.AreEqual(secondHeritageParent, existingClass.Parents[1]);
    }

    [TestMethod]
    public void UpdateClass_WithValidInterface_UpdatesUnimplementedMethodsFromInterfacesSignatures()
    {
        var classId = Guid.NewGuid();
        var oneHeritageId = Guid.NewGuid();
        var aSignatureId = Guid.NewGuid();

        var existingClass = new Class { Id = classId, Name = "OldName" };
        var aSignature = new MethodSignature
        {
            Id = aSignatureId,
            Name = "SignatureName",
            ReturnType = new Primitive(),
            Parameters = [new Parameter("Parameter", new Primitive())]
        };
        var oneHeritageParent =
            new Interface { Id = oneHeritageId, Name = "ParentInterface", MethodSignatures = [aSignature] };

        _mockInterfaceRepository.Setup(r => r.Find(oneHeritageId)).Returns(oneHeritageParent);
        _mockClassRepository.Setup(r => r.FindAllTypes()).Returns([existingClass, oneHeritageParent]);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([existingClass]);
        _mockClassRepository.Setup(r => r.Find(classId)).Returns(existingClass);
        _mockInterfaceRepository.Setup(r => r.Find(oneHeritageId)).Returns(oneHeritageParent);
        _mockClassRepository.Setup(r => r.Update(It.IsAny<Class>()));

        _mockAttributeRepository.Setup(r => r.FindAll()).Returns([]);
        _mockMethodRepository.Setup(r => r.FindAll()).Returns([]);

        var dto = new UpdateClassDTO { Name = "NewName", IdClass = oneHeritageId.ToString() };

        _classService.UpdateClass(classId, dto);

        Assert.AreEqual("NewName", existingClass.Name);
        Assert.AreEqual(1, existingClass.Parents.Count);
        Assert.AreEqual(oneHeritageParent, existingClass.Parents[0]);
        Assert.AreEqual(existingClass.Methods.Count, 1);
        Assert.AreEqual(existingClass.Methods[0].Name, aSignature.Name);
        Assert.AreEqual(existingClass.Methods[0].ReturnType, aSignature.ReturnType);
        Assert.AreEqual(existingClass.Methods[0].Parameters.Count, aSignature.Parameters.Count);
        Assert.AreEqual(existingClass.Methods[0].Parameters[0].Name, aSignature.Parameters[0].Name);
        Assert.AreEqual(existingClass.Methods[0].Parameters[0].ParameterType, aSignature.Parameters[0].ParameterType);
    }

    [TestMethod]
    public void GetTypes_ShouldReturnListOfTypes()
    {
        var expectedTypes = new List<Type>
        {
            new Class(null, null, null!, null!, "TestClass"),
            new Interface("ITestInterface"),
            new Primitive { Name = "int" }
        };

        _mockClassRepository.Setup(repo => repo.FindAllTypes())
            .Returns(expectedTypes);

        var result = _classService.GetTypes();

        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Count);
        Assert.IsTrue(result.Any(t => t.Name == "TestClass"));
        Assert.IsTrue(result.Any(t => t.Name == "ITestInterface"));
        Assert.IsTrue(result.Any(t => t.Name == "int"));

        _mockClassRepository.Verify(repo => repo.FindAllTypes(), Times.Once);
    }

    [TestMethod]
    public void GetClass_ShouldReturnListOfTypes()
    {
        var expectedTypes = new List<Class>
        {
            new Class(null, null, null!, null!, "TestClass"), new Class(null, null, null!, null!, "TestClass2"),
        };

        _mockClassRepository.Setup(repo => repo.FindAll())
            .Returns(expectedTypes);

        var result = _classService.GetClasses();

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(t => t.Name == "TestClass"));
        Assert.IsTrue(result.Any(t => t.Name == "TestClass2"));

        _mockClassRepository.Verify(repo => repo.FindAll(), Times.Once);
    }
}
