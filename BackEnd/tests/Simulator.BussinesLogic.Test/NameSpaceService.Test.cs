using Moq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class NameSpaceService_Test
{
    private Mock<INameSpaceRepository> _mockNameSpaceRepository = null!;
    private Mock<IClassRepository> _mockClassRepository = null!;
    private Mock<IInterfaceRepository> _mockInterfaceRepository = null!;
    private NameSpaceService _nameSpaceService = null!;
    private NameSpaceDTO _dto = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mockNameSpaceRepository = new Mock<INameSpaceRepository>(MockBehavior.Strict);
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);
        _mockInterfaceRepository = new Mock<IInterfaceRepository>(MockBehavior.Strict);
        _nameSpaceService = new NameSpaceService(_mockNameSpaceRepository.Object, _mockClassRepository.Object,
            _mockInterfaceRepository.Object);

        _dto = new NameSpaceDTO { Name = "MyNamespace" };
    }

    [TestMethod]
    public void CreateNameSpace_ShouldCallRepositoryAdd_AndReturnId()
    {
        var entity = _dto.ToEntity();

        _mockNameSpaceRepository.Setup(repo => repo.FindAll()).Returns([]);
        _mockNameSpaceRepository.Setup(repo => repo.Add(It.IsAny<NameSpace>()))
            .Callback<NameSpace>(ns => entity = ns);

        var resultId = _nameSpaceService.CreateNameSpace(_dto);

        _mockNameSpaceRepository.Verify(repo => repo.Add(It.Is<NameSpace>(ns => ns.Name == _dto.Name)), Times.Once);
        Assert.AreEqual(entity.Id, resultId);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void CreateNameSpace_ShouldThrowException_WhenNameAlreadyExists()
    {
        _mockNameSpaceRepository.Setup(repo => repo.FindAll()).Returns([new NameSpace { Name = "MyNamespace" }]);

        _nameSpaceService.CreateNameSpace(_dto);
    }

    [TestMethod]
    public void AddFatherToNameSpace_ShouldSetFatherAndUpdate()
    {
        var childId = Guid.NewGuid();
        var fatherId = Guid.NewGuid();

        var child = new NameSpace { Id = childId, Name = "Child" };
        var father = new NameSpace { Id = fatherId, Name = "Father" };

        _mockNameSpaceRepository.Setup(repo => repo.Find(childId)).Returns(child);
        _mockNameSpaceRepository.Setup(repo => repo.Find(fatherId)).Returns(father);
        _mockNameSpaceRepository.Setup(repo => repo.Update(It.IsAny<NameSpace>()));
        _mockNameSpaceRepository.Setup(repo => repo.FindAllNamespaceComponents()).Returns([child]);

        _nameSpaceService.AddFatherToNameSpace(childId, fatherId);

        Assert.AreEqual(father, child.Father);
        _mockNameSpaceRepository.Verify(repo => repo.Update(father), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException), "NameSpace does not exist")]
    public void AddFatherToNameSpace_ShouldThrowException_WhenChildDoesNotExist()
    {
        var childId = Guid.NewGuid();
        var fatherId = Guid.NewGuid();

        _mockNameSpaceRepository.Setup(repo => repo.Find(childId)).Returns((NameSpace?)null);
        _mockNameSpaceRepository.Setup(repo => repo.FindAllNamespaceComponents()).Returns([]);

        _nameSpaceService.AddFatherToNameSpace(childId, fatherId);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException), "Cannot add the same NameSpace as a father")]
    public void AddFatherToNameSpace_ShouldThrowException_WhenChildIsFather()
    {
        var childId = Guid.NewGuid();
        var fatherId = childId;

        var child = new NameSpace { Id = childId, Name = "Child" };
        var father = new NameSpace { Id = fatherId, Name = "Father" };

        _mockNameSpaceRepository.Setup(repo => repo.Find(childId)).Returns(child);
        _mockNameSpaceRepository.Setup(repo => repo.Find(fatherId)).Returns(father);
        _mockNameSpaceRepository.Setup(repo => repo.Update(It.IsAny<NameSpace>()));
        _mockNameSpaceRepository.Setup(repo => repo.FindAllNamespaceComponents()).Returns([child]);

        _nameSpaceService.AddFatherToNameSpace(childId, fatherId);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException), "NameSpace does not exist")]
    public void AddFatherToNameSpace_ShouldThrowException_WhenFatherDoesNotExist()
    {
        var childId = Guid.NewGuid();
        var fatherId = Guid.NewGuid();

        var child = new NameSpace { Id = childId, Name = "ChildNamespace" };

        _mockNameSpaceRepository.Setup(repo => repo.Find(childId)).Returns(child);
        _mockNameSpaceRepository.Setup(repo => repo.Find(fatherId)).Returns((NameSpace?)null);
        _mockNameSpaceRepository.Setup(repo => repo.FindAllNamespaceComponents()).Returns([child]);

        _nameSpaceService.AddFatherToNameSpace(childId, fatherId);
    }

    [TestMethod]
    public void AssociateNameSpaceToClass_ShouldAssociateSuccessfully()
    {
        var ns = new NameSpace("TestNS", null);
        var classEntity = new Class { Id = Guid.NewGuid(), NameSpace = null };

        _mockNameSpaceRepository.Setup(r => r.Find(ns.Id)).Returns(ns);
        _mockClassRepository.Setup(r => r.Find(classEntity.Id)).Returns(classEntity);
        _mockClassRepository.Setup(r => r.Update(classEntity));

        _nameSpaceService.AssociateNameSpaceToClass(ns.Id, classEntity.Id);

        Assert.AreEqual(ns, classEntity.NameSpace);
        _mockClassRepository.Verify(r => r.Update(classEntity), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void AssociateNameSpaceToClass_ShouldThrow_WhenClassDoesNotExist()
    {
        var ns = new NameSpace("TestNS", null);

        _mockNameSpaceRepository.Setup(r => r.Find(ns.Id)).Returns(ns);
        _mockClassRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns((Class)null!);

        _nameSpaceService.AssociateNameSpaceToClass(ns.Id, Guid.NewGuid());
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void AssociateNameSpaceToClass_ShouldThrowException_WhenClassNotInNamespaceChilds()
    {
        var ns = new NameSpace("TestNS", null);
        var classEntity = new Class { Id = Guid.NewGuid(), Name = "MyClass", NameSpace = null };

        _mockNameSpaceRepository.Setup(r => r.Find(ns.Id)).Returns(ns);
        _mockClassRepository.Setup(r => r.Find(classEntity.Id)).Returns(classEntity);

        _nameSpaceService.AssociateNameSpaceToClass(ns.Id, classEntity.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void AssociateNameSpaceToInterface_ShouldThrowException_WhenInterfaceNotInNamespaceChilds()
    {
        var ns = new NameSpace("TestNS", null);
        var iface = new Interface { Id = Guid.NewGuid(), Name = "IMyInterface", NameSpace = null };

        _mockNameSpaceRepository.Setup(r => r.Find(ns.Id)).Returns(ns);
        _mockInterfaceRepository.Setup(r => r.Find(iface.Id)).Returns(iface);

        _nameSpaceService.AssociateNameSpaceToInterface(ns.Id, iface.Id);
    }

    [TestMethod]
    public void AssociateNameSpaceToInterface_ShouldAssociateSuccessfully()
    {
        var ns = new NameSpace("TestNS", null);
        var iface = new Interface { Id = Guid.NewGuid(), NameSpace = null };

        _mockNameSpaceRepository.Setup(r => r.Find(ns.Id)).Returns(ns);
        _mockInterfaceRepository.Setup(r => r.Find(iface.Id)).Returns(iface);
        _mockInterfaceRepository.Setup(r => r.Update(iface));

        _nameSpaceService.AssociateNameSpaceToInterface(ns.Id, iface.Id);

        Assert.AreEqual(ns, iface.NameSpace);
        _mockInterfaceRepository.Verify(r => r.Update(iface), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void AssociateNameSpaceToInterface_ShouldThrow_WhenInterfaceDoesNotExist()
    {
        var ns = new NameSpace("TestNS", null);

        _mockNameSpaceRepository.Setup(r => r.Find(ns.Id)).Returns(ns);
        _mockInterfaceRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns((Interface)null!);

        _nameSpaceService.AssociateNameSpaceToInterface(ns.Id, Guid.NewGuid());
    }

    [TestMethod]
    public void AddChildToNameSpace_ShouldAddSuccessfully()
    {
        var parent = new NameSpace("Parent", null);
        var child = new NameSpace("Child", null);

        _mockNameSpaceRepository.Setup(r => r.Find(parent.Id)).Returns(parent);
        _mockNameSpaceRepository.Setup(r => r.Find(child.Id)).Returns(child);
        _mockNameSpaceRepository.Setup(r => r.Update(parent));
        _mockNameSpaceRepository.Setup(r => r.Update(child));

        _mockNameSpaceRepository
            .Setup(r => r.FindAllNamespaceComponents())
            .Returns([parent, child]);

        _nameSpaceService.AddChildToNameSpace(parent.Id, child.Id);

        Assert.AreEqual(parent, child.Father);
        Assert.IsTrue(parent.Childs.Contains(child));
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void AddChildToNameSpace_ShouldThrow_WhenCircularReference()
    {
        var ns = new NameSpace("NS", null);
        ns.Father = ns;

        _mockNameSpaceRepository.Setup(r => r.Find(ns.Id)).Returns(ns);
        _mockNameSpaceRepository
            .Setup(r => r.FindAllNamespaceComponents())
            .Returns([ns]);

        _nameSpaceService.AddChildToNameSpace(ns.Id, ns.Id);
    }

    [TestMethod]
    public void RemoveChildFromNameSpace_ShouldRemoveSuccessfully()
    {
        var parent = new NameSpace("Parent", null);
        var child = new NameSpace("Child", null);
        parent.AddChild(child);

        _mockNameSpaceRepository
            .Setup(r => r.FindAllNamespaceComponents())
            .Returns([child]);

        _mockNameSpaceRepository.Setup(r => r.Find(parent.Id)).Returns(parent);
        _mockNameSpaceRepository.Setup(r => r.Find(child.Id)).Returns(child);

        _mockNameSpaceRepository.Setup(r => r.Update(It.IsAny<NameSpace>()));

        _nameSpaceService.RemoveChildFromNameSpace(parent.Id, child.Id);

        Assert.IsFalse(parent.Childs.Contains(child));
        Assert.IsNull(child.Father);

        _mockNameSpaceRepository.Verify(r => r.Update(parent), Times.Once);
        _mockNameSpaceRepository.Verify(r => r.Update(child), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void RemoveChildFromNameSpace_ShouldThrowIfChildIsNotInParent()
    {
        var parent = new NameSpace("Parent", null);
        var child = new NameSpace("Child", null);

        _mockNameSpaceRepository
            .Setup(r => r.FindAllNamespaceComponents())
            .Returns([child]);

        _mockNameSpaceRepository.Setup(r => r.Find(parent.Id)).Returns(parent);
        _mockNameSpaceRepository.Setup(r => r.Find(child.Id)).Returns(child);

        _nameSpaceService.RemoveChildFromNameSpace(parent.Id, child.Id);
    }

    [TestMethod]
    public void GetAllNameSpace_ShouldReturnAllNameSpacesAsDTOs()
    {
        var nameSpace1 = new NameSpace { Id = Guid.NewGuid(), Name = "NS1" };
        var nameSpace2 = new NameSpace { Id = Guid.NewGuid(), Name = "NS2" };
        var nameSpaces = new List<NameSpace> { nameSpace1, nameSpace2 };

        _mockNameSpaceRepository.Setup(r => r.FindAll()).Returns(nameSpaces);

        var service = new NameSpaceService(_mockNameSpaceRepository.Object, _mockClassRepository.Object, _mockInterfaceRepository.Object);

        var result = service.GetAllNameSpace();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(nameSpace1.Id.ToString(), result[0].Id);
        Assert.AreEqual(nameSpace1.Name, result[0].Name);
        Assert.AreEqual(nameSpace2.Id.ToString(), result[1].Id);
        Assert.AreEqual(nameSpace2.Name, result[1].Name);
    }

    [TestMethod]
    public void GetAllNamespaceComponents_ShouldReturnOnlyPrimitivesAsDTOs()
    {
        var primitive1 = new Primitive { Id = Guid.NewGuid() };
        var primitive2 = new Primitive { Id = Guid.NewGuid() };
        var otherComponent = new NameSpace { Id = Guid.NewGuid(), Name = "Other" };

        _mockNameSpaceRepository
            .Setup(r => r.FindAllNamespaceComponents())
            .Returns([primitive1, primitive2, otherComponent]);

        var service = new NameSpaceService(_mockNameSpaceRepository.Object, _mockClassRepository.Object, _mockInterfaceRepository.Object);

        var result = service.GetAllNamespaceComponents();

        Assert.AreEqual(1, result.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException), "Circular reference detected")]
    public void AddFatherToNameSpace_ShouldThrowException_WhenCircularReference()
    {
        var ns1 = new NameSpace { Id = Guid.NewGuid(), Name = "NS1" };
        var ns2 = new NameSpace { Id = Guid.NewGuid(), Name = "NS2" };

        ns1.Father = ns2;
        ns2.Father = ns1;

        _mockNameSpaceRepository.Setup(r => r.Find(ns1.Id)).Returns(ns1);
        _mockNameSpaceRepository.Setup(r => r.Find(ns2.Id)).Returns(ns2);
        _mockNameSpaceRepository.Setup(r => r.FindAllNamespaceComponents()).Returns([ns1, ns2]);

        _nameSpaceService.AddFatherToNameSpace(ns1.Id, ns2.Id);
    }
}
