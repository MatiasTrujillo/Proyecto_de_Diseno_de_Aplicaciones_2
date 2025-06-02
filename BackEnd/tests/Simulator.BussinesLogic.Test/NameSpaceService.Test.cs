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
    private NameSpaceService _nameSpaceService = null!;
    private NameSpaceDTO _dto = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mockNameSpaceRepository = new Mock<INameSpaceRepository>(MockBehavior.Strict);
        _nameSpaceService = new NameSpaceService(_mockNameSpaceRepository.Object);

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
}
