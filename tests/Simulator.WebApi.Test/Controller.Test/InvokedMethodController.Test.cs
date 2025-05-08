using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class InvokedMethodController_Test
{
    private static readonly Mock<IMethodService> _methodServiceMock = new();
    private readonly InvokedMethodController _controller = new(_methodServiceMock.Object);

    [TestMethod]
    public void AssociateInvokeMethod_ShouldReturnCreated_WhenInvokedMethodIsAssociated()
    {
        var invokedMethodDto = new InvokedMethodDTO
        {
            MethodId = Guid.NewGuid(),
        };
        var expectedId = Guid.NewGuid();

        _methodServiceMock.Setup(service => service.AssociateInvokeMethod(invokedMethodDto))
            .Returns(expectedId);

        var result = _controller.AssociateInvokeMethod(invokedMethodDto) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");

        var createdValue = result.Value?.GetType().GetProperty("id")?.GetValue(result.Value, null);
        Assert.IsNotNull(createdValue, "The created value should contain an 'id'.");
        Assert.AreEqual(expectedId, createdValue, "The returned id should match the expected id.");

        _methodServiceMock.Verify(service => service.AssociateInvokeMethod(invokedMethodDto), Times.Once);
    }
}
