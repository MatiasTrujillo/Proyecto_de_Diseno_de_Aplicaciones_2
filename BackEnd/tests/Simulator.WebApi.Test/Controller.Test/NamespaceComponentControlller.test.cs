using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class NamespaceComponentController_Test
{
    [TestMethod]
    public void GetAllNamespaceComponents_ShouldReturnOkWithAllComponents()
    {
        var expectedComponents = new List<NamespaceComponentDTO>
        {
            new NamespaceComponentDTO { Id = Guid.NewGuid().ToString() },
            new NamespaceComponentDTO { Id = Guid.NewGuid().ToString() }
        };

        var mockService = new Mock<INameSpaceService>(MockBehavior.Strict);
        mockService.Setup(s => s.GetAllNamespaceComponents()).Returns(expectedComponents);

        var controller = new NamespaceComponentController(mockService.Object);

        var result = controller.GetAllNamespaceComponents() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        var value = result.Value as List<NamespaceComponentDTO>;
        Assert.IsNotNull(value);
        Assert.AreEqual(2, value.Count);
        Assert.AreEqual(expectedComponents[0].Id, value[0].Id);
        Assert.AreEqual(expectedComponents[1].Id, value[1].Id);

        mockService.Verify(s => s.GetAllNamespaceComponents(), Times.Once);
    }
}
