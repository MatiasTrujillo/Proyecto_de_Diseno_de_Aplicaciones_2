using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class NameSpaceControllerTest
{
    private static readonly Mock<INameSpaceService> _mockNameSpaceService = new(MockBehavior.Strict);
    private readonly NameSpaceController _controller = new(_mockNameSpaceService.Object);

    [TestMethod]
    public void CreateNameSpace_ShouldReturnCreated_WhenNamespaceIsCreated()
    {
        var nameSpaceDto = new NameSpaceDTO { Name = "TestNamespace" };
        var expectedId = Guid.NewGuid();

        _mockNameSpaceService
            .Setup(service => service.CreateNameSpace(nameSpaceDto))
            .Returns(expectedId);

        var result = _controller.CreateNameSpace(nameSpaceDto) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        var returnedId = result?.Value?.GetType().GetProperty("id")?.GetValue(result.Value, null);
        Assert.AreEqual(expectedId, returnedId);

        _mockNameSpaceService.Verify(service => service.CreateNameSpace(nameSpaceDto), Times.Once);
    }

    [TestMethod]
    public void AssociateNameSpaceWithClass_ShouldReturnCreated_WhenAssociationSucceeds()
    {
        var nameSpaceId = Guid.NewGuid();
        var classId = Guid.NewGuid();

        _mockNameSpaceService
            .Setup(service => service.AsociateToClass(nameSpaceId, classId))
            .Verifiable();

        var result = _controller.AssociateNameSpaceWithClass(nameSpaceId, classId) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        var message = result?.Value?.GetType().GetProperty("message")?.GetValue(result.Value, null);
        Assert.AreEqual("Namespace associated with class successfully", message);

        _mockNameSpaceService.Verify(service => service.AsociateToClass(nameSpaceId, classId), Times.Once);
    }

    [TestMethod]
    public void AssociateNameSpaceWithInterface_ShouldReturnCreated_WhenAssociationSucceeds()
    {
        var nameSpaceId = Guid.NewGuid();
        var interfaceId = Guid.NewGuid();

        _mockNameSpaceService
            .Setup(service => service.AsociateToInterface(nameSpaceId, interfaceId))
            .Verifiable();

        var result = _controller.AssociateNameSpaceWithInterface(nameSpaceId, interfaceId) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        var message = result?.Value?.GetType().GetProperty("message")?.GetValue(result.Value, null);
        Assert.AreEqual("Namespace associated with interface successfully", message);

        _mockNameSpaceService.Verify(service => service.AsociateToInterface(nameSpaceId, interfaceId), Times.Once);
    }
}
