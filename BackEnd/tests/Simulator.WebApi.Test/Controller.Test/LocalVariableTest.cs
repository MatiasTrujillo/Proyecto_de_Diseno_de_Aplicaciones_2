using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class LocalVariableTest
{
    private static readonly Mock<ILocalVariableService> _methodServiceMock = new Mock<ILocalVariableService>();
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

    [TestMethod]
    public void GetAllLocalVariables_ShouldReturnOkWithAllLocalVariables()
    {
        // Arrange
        var expectedLocalVariables = new List<LocalVariableDTO>
        {
            new LocalVariableDTO { Id = "1", Name = "var1" },
            new LocalVariableDTO { Id = "2", Name = "var2" }
        };

        var mockService = new Mock<ILocalVariableService>();
        mockService.Setup(s => s.GetAllLocalVariables()).Returns(expectedLocalVariables);

        var controller = new LocalVariableController(mockService.Object);

        // Act
        var result = controller.GetAllLocalVariables() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        var value = result.Value as List<LocalVariableDTO>;
        Assert.IsNotNull(value);
        Assert.AreEqual(2, value.Count);
        Assert.AreEqual("var1", value[0].Name);
        Assert.AreEqual("var2", value[1].Name);

        mockService.Verify(s => s.GetAllLocalVariables(), Times.Once);
    }
}
