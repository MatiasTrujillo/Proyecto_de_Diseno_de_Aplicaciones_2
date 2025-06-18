using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.Domain;
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
            .Setup(service => service.CreateNameSpace(It.IsAny<NameSpace>()))
            .Returns(expectedId);

        var result = _controller.CreateNameSpace(nameSpaceDto) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);

        var returnedId = result.Value?.GetType().GetProperty("id")?.GetValue(result.Value, null);
        Assert.AreEqual(expectedId, returnedId);
    }

    [TestMethod]
    public void AddChildToNameSpace_ShouldReturnNoContent()
    {
        var parentId = Guid.NewGuid();
        var childId = Guid.NewGuid();

        _mockNameSpaceService
            .Setup(service => service.AddChildToNameSpace(parentId, childId));

        var result = _controller.AddChildToNameSpace(parentId, childId) as NoContentResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(204, result.StatusCode);
        _mockNameSpaceService.Verify(service => service.AddChildToNameSpace(parentId, childId), Times.Once);
    }

    [TestMethod]
    public void RemoveChildFromNameSpace_ShouldReturnNoContent()
    {
        var parentId = Guid.NewGuid();
        var childId = Guid.NewGuid();

        _mockNameSpaceService
            .Setup(service => service.RemoveChildFromNameSpace(parentId, childId));

        var result = _controller.RemoveChildFromNameSpace(parentId, childId) as NoContentResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(204, result.StatusCode);
        _mockNameSpaceService.Verify(service => service.RemoveChildFromNameSpace(parentId, childId), Times.Once);
    }

    [TestMethod]
    public void GetAllNameSpace_ShouldReturnOkWithAllNameSpaces()
    {
        var nameSpaces = new List<NameSpace>
        {
            new NameSpace { Id = Guid.NewGuid(), Name = "NS1" },
            new NameSpace { Id = Guid.NewGuid(), Name = "NS2" }
        };

        var mockService = new Mock<INameSpaceService>(MockBehavior.Strict);
        mockService.Setup(s => s.GetAllNameSpace()).Returns(nameSpaces);

        var controller = new NameSpaceController(mockService.Object);

        var result = controller.GetAllNameSpace() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        var value = result.Value as List<NameSpaceDTO>;
        Assert.IsNotNull(value);
        Assert.AreEqual(2, value.Count);
        Assert.AreEqual("NS1", value[0].Name);
        Assert.AreEqual("NS2", value[1].Name);

        mockService.Verify(s => s.GetAllNameSpace(), Times.Once);
    }

    [TestMethod]
    public void Properties_GetSet_ShouldBeCoveredByCoverage()
    {
        var dto = new NamespaceComponentDTO();
        dto.Id = "123";
        dto.Name = "TestName";

        // Acceso a los getters
        Assert.AreEqual("123", dto.Id);
        Assert.AreEqual("TestName", dto.Name);
    }
}
