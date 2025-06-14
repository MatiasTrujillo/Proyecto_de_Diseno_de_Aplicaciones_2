using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.Domain;
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
        var expectedComponents = new List<NamespaceComponent>
        {
            new NameSpace { Id = Guid.NewGuid() },
            new NameSpace { Id = Guid.NewGuid() }
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

        mockService.Verify(s => s.GetAllNamespaceComponents(), Times.Once);
    }
}
