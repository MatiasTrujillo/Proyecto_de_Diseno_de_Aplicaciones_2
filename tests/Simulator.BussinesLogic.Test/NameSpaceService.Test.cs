using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simulator.BussinesLogic;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IDataAcces;
using System;
using System.Collections.Generic;

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

        _mockNameSpaceRepository.Setup(repo => repo.FindAll()).Returns(new List<NameSpace>());
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
        _mockNameSpaceRepository.Setup(repo => repo.FindAll()).Returns(new List<NameSpace>
        {
            new NameSpace { Name = "MyNamespace" }
        });

        _nameSpaceService.CreateNameSpace(_dto);
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

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void AsociateToClass_ShouldThrow_WhenClassNotFound()
    {
        var nsId = Guid.NewGuid();
        var classId = Guid.NewGuid();

        _mockNameSpaceRepository.Setup(repo => repo.Find(nsId)).Returns(new NameSpace { Id = nsId });
        _mockClassRepository.Setup(repo => repo.Find(classId)).Returns((Class?)null);

        _nameSpaceService.AsociateToClass(nsId, classId);
    }

    [TestMethod]
    [ExpectedException(typeof(NameSpaceException))]
    public void AsociateToClass_ShouldThrow_WhenNamespaceNotFound()
    {
        var nsId = Guid.NewGuid();
        var classId = Guid.NewGuid();

        _mockNameSpaceRepository.Setup(repo => repo.Find(nsId)).Returns((NameSpace?)null);

        _nameSpaceService.AsociateToClass(nsId, classId);
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
    [ExpectedException(typeof(NullReferenceException))]
    public void AddFatherToNameSpace_ShouldThrow_WhenChildNotFound()
    {
        var childId = Guid.NewGuid();
        var fatherId = Guid.NewGuid();

        _mockNameSpaceRepository.Setup(repo => repo.Find(childId)).Returns((NameSpace?)null);
        _mockNameSpaceRepository.Setup(repo => repo.Find(fatherId)).Returns(new NameSpace());

        _nameSpaceService.AddFatherToNameSpace(childId, fatherId);
    }
}
