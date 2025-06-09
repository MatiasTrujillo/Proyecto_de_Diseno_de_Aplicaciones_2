using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class ParameterController_Test
{
    private static readonly Mock<IParameterService> _parameterServiceMock = new Mock<IParameterService>();
    private readonly ParameterController _controller = new ParameterController(_parameterServiceMock.Object);

    [TestMethod]
    public void CreateParameter_ShouldReturnCreated_WhenParameterIsCreated()
    {
        var parameterDto = new ParameterDTO
        {
            Name = "TestParameter",
            ParameterType = new TypeDTO { Name = "TestType" }
        };
        var expectedParameterId = Guid.NewGuid();

        _parameterServiceMock.Setup(service => service.CreateParameter(parameterDto))
            .Returns(expectedParameterId);

        var result = _controller.CreateParameter(parameterDto) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");

        var createdValue = result.Value?.GetType().GetProperty("id")?.GetValue(result.Value, null);

        Assert.IsNotNull(createdValue, "The created value should contain an 'id'.");
        Assert.AreEqual(expectedParameterId, createdValue, "The returned id should match the expected id.");

        _parameterServiceMock.Verify(service => service.CreateParameter(parameterDto), Times.Once);
    }

    [TestMethod]
    public void SetInvokeMethodToParameter_ShouldReturnCreated_WhenAssociationIsSuccessful()
    {
        var parameterId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        _parameterServiceMock
            .Setup(service => service.SetInvokeMethodToParameter(parameterId, invokedMethodId))
            .Verifiable();

        var result = _controller.SetInvokeMethodToParameter(parameterId, invokedMethodId) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");

        var message = result.Value?.GetType().GetProperty("message")?.GetValue(result.Value, null)?.ToString();
        Assert.AreEqual("Parameter associated with invoked method successfully", message, "The message should match the expected one.");

        _parameterServiceMock.Verify(service => service.SetInvokeMethodToParameter(parameterId, invokedMethodId), Times.Once);
    }
}
