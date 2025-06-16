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

        var resultId = _nameSpaceService.CreateNameSpace(entity);

        _mockNameSpaceRepository.Verify(repo => repo.Add(It.Is<NameSpace>(ns => ns.Name == _dto.Name)), Times.Once);
        Assert.AreEqual(entity.Id, resultId);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void CreateNameSpace_ShouldThrowException_WhenNameAlreadyExists()
    {
        _mockNameSpaceRepository.Setup(repo => repo.FindAll()).Returns([new NameSpace { Name = "MyNamespace" }]);

        _nameSpaceService.CreateNameSpace(_dto.ToEntity());
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
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([]);
        _nameSpaceService.AddChildToNameSpace(parent.Id, child.Id);

        Assert.AreEqual(parent, child.Father);
        Assert.IsTrue(parent.Childs.Contains(child));
    }

    [TestMethod]
    public void AddChildToNameSpaceClass_ShouldAddSuccessfully()
    {
        var parent = new NameSpace("Parent", null);
        var child = new Class()
        {
            Id = Guid.NewGuid(),
            Name = "ChildClass",
            NameSpace = null
        };

        _mockNameSpaceRepository.Setup(r => r.Find(parent.Id)).Returns(parent);
        _mockNameSpaceRepository.Setup(r => r.Find(child.Id)).Returns((NameSpace)null!);
        _mockClassRepository.Setup(r => r.Find(child.Id)).Returns(child);
        _mockNameSpaceRepository.Setup(r => r.Update(parent));
        _mockClassRepository.Setup(r => r.Update(child));

        _mockNameSpaceRepository
            .Setup(r => r.FindAllNamespaceComponents())
            .Returns([parent, child]);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([]);
        _nameSpaceService.AddChildToNameSpace(parent.Id, child.Id);

        Assert.AreEqual(parent, child.Father);
        Assert.IsTrue(parent.Childs.Contains(child));
    }

    [TestMethod]
    public void AddChildToNameSpaceInterface_ShouldAddSuccessfully()
    {
        var parent = new NameSpace("Parent", null);
        var child = new Interface()
        {
            Id = Guid.NewGuid(),
            Name = "ChildClass",
            NameSpace = null
        };

        _mockNameSpaceRepository.Setup(r => r.Find(parent.Id)).Returns(parent);
        _mockNameSpaceRepository.Setup(r => r.Find(child.Id)).Returns((NameSpace)null!);
        _mockInterfaceRepository.Setup(r => r.Find(child.Id)).Returns(child);
        _mockNameSpaceRepository.Setup(r => r.Update(parent));
        _mockInterfaceRepository.Setup(r => r.Update(child));

        _mockNameSpaceRepository
            .Setup(r => r.FindAllNamespaceComponents())
            .Returns([parent, child]);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([]);
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

        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([]);

        _nameSpaceService.RemoveChildFromNameSpace(parent.Id, child.Id);

        Assert.IsFalse(parent.Childs.Contains(child));
        Assert.IsNull(child.Father);

        _mockNameSpaceRepository.Verify(r => r.Update(parent), Times.Once);
        _mockNameSpaceRepository.Verify(r => r.Update(child), Times.Once);
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
        Assert.AreEqual(nameSpace1.Id, result[0].Id);
        Assert.AreEqual(nameSpace1.Name, result[0].Name);
        Assert.AreEqual(nameSpace2.Id, result[1].Id);
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
    public void AddChildToNameSpace_ShouldThrow_WhenCircularReferenceDetected()
    {
        var a = new NameSpace("A", null) { Id = Guid.NewGuid() };
        var b = new NameSpace("B", null) { Id = Guid.NewGuid() };
        a.Father = b;
        b.Father = a;

        _mockNameSpaceRepository.Setup(r => r.Find(a.Id)).Returns(a);
        _mockNameSpaceRepository.Setup(r => r.Find(b.Id)).Returns(b);
        _mockNameSpaceRepository.Setup(r => r.FindAllNamespaceComponents()).Returns([a, b]);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([]);

        Assert.ThrowsException<NameSpaceException>(() =>
            _nameSpaceService.AddChildToNameSpace(a.Id, b.Id)
        );
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void AddChildToNameSpace_ShouldThrow_WhenChildComponentDoesNotExist()
    {
        var parent = new NameSpace("Parent", null);
        var childId = Guid.NewGuid();

        _mockNameSpaceRepository.Setup(r => r.FindAllNamespaceComponents()).Returns([parent]);
        _mockNameSpaceRepository.Setup(r => r.Find(parent.Id)).Returns(parent);

        _nameSpaceService.AddChildToNameSpace(parent.Id, childId);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void AddChildToNameSpace_ShouldThrow_WhenParentDoesNotExist()
    {
        var child = new NameSpace("Child", null);
        var parentId = Guid.NewGuid();

        _mockNameSpaceRepository.Setup(r => r.FindAllNamespaceComponents()).Returns([child]);
        _mockNameSpaceRepository.Setup(r => r.Find(parentId)).Returns((NameSpace)null!);

        _nameSpaceService.AddChildToNameSpace(parentId, child.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void RemoveChildFromNameSpace_ShouldThrow_WhenChildNotPartOfParent()
    {
        var parent = new NameSpace("Parent", null);
        var child = new NameSpace("Child", null);

        _mockNameSpaceRepository.Setup(r => r.FindAllNamespaceComponents()).Returns([child]);
        _mockNameSpaceRepository.Setup(r => r.Find(parent.Id)).Returns(parent);
        _mockNameSpaceRepository.Setup(r => r.Find(child.Id)).Returns(child);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([]);

        _nameSpaceService.RemoveChildFromNameSpace(parent.Id, child.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void AddChildToNameSpace_ShouldThrow_WhenChildWithSameNameExists()
    {
        var parent = new NameSpace("Parent", null);
        var existingChild = new NameSpace("ChildName", null);
        parent.AddChild(existingChild);

        var newChild = new NameSpace("ChildName", null);

        _mockNameSpaceRepository.Setup(r => r.FindAllNamespaceComponents()).Returns([parent, newChild]);
        _mockNameSpaceRepository.Setup(r => r.Find(parent.Id)).Returns(parent);
        _mockNameSpaceRepository.Setup(r => r.Find(newChild.Id)).Returns(newChild);
        _mockClassRepository.Setup(r => r.FindAll()).Returns([]);
        _mockInterfaceRepository.Setup(r => r.FindAll()).Returns([]);

        _nameSpaceService.AddChildToNameSpace(parent.Id, newChild.Id);
    }
}
