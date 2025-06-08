using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class LocalVariableTest
{
    private static readonly Mock<IMethodService> _methodServiceMock = new Mock<IMethodService>();
    private readonly LocalVariableController _controller = new LocalVariableController(_methodServiceMock.Object);

    [TestMethod]
    public void CreateLocalVariable_ShouldReturnCreated_WhenLocaVariableIsCreated()
    {
        var localVariableDto = new LocalVariableDTO
        {
            Name = "TestParameter",
            ReferenceType = new TypeDTO { Name = "TestType" }
        };
        var expectedParameterId = Guid.NewGuid();

        _methodServiceMock.Setup(service => service.CreateLocalVariable(localVariableDto))
            .Returns(expectedParameterId);

        var result = _controller.CreateLocalVariable(localVariableDto) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");

        var createdValue = result.Value?.GetType().GetProperty("id")?.GetValue(result.Value, null);

        Assert.IsNotNull(createdValue, "The created value should contain an 'id'.");
        Assert.AreEqual(expectedParameterId, createdValue, "The returned id should match the expected id.");

        _methodServiceMock.Verify(service => service.CreateLocalVariable(localVariableDto), Times.Once);
    }

    [TestMethod]
    public void SetInvokeMethodToLocalVariable_ShouldReturnCreated_WhenLocalVariableIsAssociatedWithInvokedMethod()
    {
        var localVariableId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        _methodServiceMock
            .Setup(service => service.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId))
            .Verifiable();

        var result = _controller.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");

        var responseMessage = result.Value?.GetType().GetProperty("message")?.GetValue(result.Value, null);
        Assert.AreEqual("Local variable associated with invoked method successfully", responseMessage);

        _methodServiceMock.Verify(service => service.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId), Times.Once);
    }
}
