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
}
