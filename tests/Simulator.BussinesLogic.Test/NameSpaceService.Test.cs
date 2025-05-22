using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simulator.BussinesLogic;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class NameSpaceService_Test
{
    private Mock<INameSpaceRepository> _mockNameSpaceRepository = null!;
    private Mock<IClassRepository> _mockClassRepository = null!;
    private NameSpaceService _nameSpaceService = null!;
    private NameSpaceDTO _dto = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mockNameSpaceRepository = new Mock<INameSpaceRepository>(MockBehavior.Strict);
        _mockClassRepository = new Mock<IClassRepository>(MockBehavior.Strict);

        _nameSpaceService = new NameSpaceService(_mockNameSpaceRepository.Object, _mockClassRepository.Object);

        _dto = new NameSpaceDTO
        {
            Name = "MyNamespace"
        };
    }

    [TestMethod]
    public void CreateNameSpace_ShouldCallRepositoryAdd_AndReturnId()
    {
        var entity = _dto.ToEntity();
        _mockNameSpaceRepository.Setup(repo => repo.Add(It.IsAny<NameSpace>()))
            .Callback<NameSpace>(ns => entity = ns);

        var resultId = _nameSpaceService.CreateNameSpace(_dto);

        _mockNameSpaceRepository.Verify(repo => repo.Add(It.Is<NameSpace>(
            ns => ns.Name == _dto.Name)), Times.Once);
        Assert.AreEqual(entity.Id, resultId);
    }

    [TestMethod]
    public void AsociateToClass_ShouldSetNamespaceOnClass_AndUpdateClass()
    {
        var nsId = Guid.NewGuid();
        var classId = Guid.NewGuid();

        var nameSpace = new NameSpace { Id = nsId, Name = "MyNamespace" };
        var classEntity = new Class { Id = classId, Name = "MyClass" };

        _mockNameSpaceRepository.Setup(repo => repo.Find(nsId)).Returns(nameSpace);
        _mockClassRepository.Setup(repo => repo.Find(classId)).Returns(classEntity);
        _mockClassRepository.Setup(repo => repo.Update(It.IsAny<Class>()));

        _nameSpaceService.AsociateToClass(nsId, classId);

        Assert.AreEqual(nameSpace, classEntity.NameSpace);
        _mockClassRepository.Verify(repo => repo.Update(classEntity), Times.Once);
    }
}
