using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class MethodControllerTest
{
    private static readonly Mock<IMethodService> _methodServiceMock = new Mock<IMethodService>();
    private readonly MethodController _controller = new MethodController(_methodServiceMock.Object);

    [TestMethod]
    public void CreateMethod_ShouldReturnCreated_WhenMethodIsCreated()
    {
        var methodDto = new MethodDTO();
        _methodServiceMock.Setup(service => service.CreateMethod(methodDto))
            .Verifiable();

        var result = _controller.CreateMethod(methodDto) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        _methodServiceMock.Verify(service => service.CreateMethod(methodDto), Times.Once);
    }
}
