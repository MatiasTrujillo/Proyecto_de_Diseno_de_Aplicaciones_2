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
        _nameSpaceService = new NameSpaceService(_mockNameSpaceRepository.Object, _mockClassRepository.Object, _mockInterfaceRepository.Object);

        _dto = new NameSpaceDTO
        {
            Name = "MyNamespace"
        };
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

        _nameSpaceService.AddFatherToNameSpace(childId, fatherId);

        Assert.AreEqual(father, child.Father);
        _mockNameSpaceRepository.Verify(repo => repo.Update(child), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException), "NameSpace does not exist")]
    public void AddFatherToNameSpace_ShouldThrowException_WhenChildDoesNotExist()
    {
        var childId = Guid.NewGuid();
        var fatherId = Guid.NewGuid();

        _mockNameSpaceRepository.Setup(repo => repo.Find(childId)).Returns((NameSpace?)null);

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
    public void RemoveNameSpace_ShouldRemove_WhenNoFather()
    {
        var ns = new NameSpace("TestNS", null);

        _mockNameSpaceRepository.Setup(r => r.Find(ns.Id)).Returns(ns);
        _mockNameSpaceRepository.Setup(r => r.Delete(ns));

        _nameSpaceService.RemoveNameSpace(ns.Id);

        _mockNameSpaceRepository.Verify(r => r.Delete(ns), Times.Once);
    }

    [TestMethod]
    public void RemoveNameSpace_ShouldRemoveAndUpdateFather_WhenHasFather()
    {
        var father = new NameSpace("Father", null);
        var child = new NameSpace("Child", father);
        father.AddChild(child);

        _mockNameSpaceRepository.Setup(r => r.Find(child.Id)).Returns(child);
        _mockNameSpaceRepository.Setup(r => r.Update(father));
        _mockNameSpaceRepository.Setup(r => r.Delete(child));

        _nameSpaceService.RemoveNameSpace(child.Id);

        _mockNameSpaceRepository.Verify(r => r.Update(father), Times.Once);
        _mockNameSpaceRepository.Verify(r => r.Delete(child), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void RemoveNameSpace_ShouldThrow_WhenDoesNotExist()
    {
        _mockNameSpaceRepository.Setup(r => r.Find(It.IsAny<Guid>())).Returns((NameSpace)null!);
        _nameSpaceService.RemoveNameSpace(Guid.NewGuid());
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

        _nameSpaceService.AddChildToNameSpace(ns.Id, ns.Id);
    }

    [TestMethod]
    public void RemoveChildFromNameSpace_ShouldRemoveSuccessfully()
    {
        var parent = new NameSpace("Parent", null);
        var child = new NameSpace("Child", parent);
        parent.AddChild(child);

        _mockNameSpaceRepository.Setup(r => r.Find(parent.Id)).Returns(parent);
        _mockNameSpaceRepository.Setup(r => r.Find(child.Id)).Returns(child);
        _mockNameSpaceRepository.Setup(r => r.Update(parent));
        _mockNameSpaceRepository.Setup(r => r.Update(child));

        _nameSpaceService.RemoveChildFromNameSpace(parent.Id, child.Id);

        Assert.IsFalse(parent.Childs.Contains(child));
        Assert.IsNull(child.Father);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void RemoveChildFromNameSpace_ShouldThrow_WhenNotAChild()
    {
        var parent = new NameSpace("Parent", null);
        var child = new NameSpace("NotAChild", null); // no está en la lista del padre

        _mockNameSpaceRepository.Setup(r => r.Find(parent.Id)).Returns(parent);
        _mockNameSpaceRepository.Setup(r => r.Find(child.Id)).Returns(child);

        _nameSpaceService.RemoveChildFromNameSpace(parent.Id, child.Id);
    }
}
