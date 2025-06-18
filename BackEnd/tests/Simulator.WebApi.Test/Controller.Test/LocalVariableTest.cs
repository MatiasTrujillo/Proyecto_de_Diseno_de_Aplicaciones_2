using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.Domain;
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
            ReferenceType = new TypeDTO { Name = "TestTypeRef", Kind = "class" },
            InstanceType = new TypeDTO { Name = "TestTypeInst", Kind = "class" }
        };
        var expectedParameterId = Guid.NewGuid();

        _methodServiceMock
            .Setup(service => service.CreateLocalVariable(It.IsAny<LocalVariable>()))
            .Returns(expectedParameterId);

        var result = _controller.CreateLocalVariable(localVariableDto) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");
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
        var expectedLocalVariables = new List<LocalVariable>
        {
            new LocalVariable
            {
                Id = Guid.NewGuid(),
                Name = "var1",
                ReferenceType = new Class { Name = "RefType1" },
                InstanceType = new Class { Name = "InstType1" }
            },
            new LocalVariable
            {
                Id = Guid.NewGuid(),
                Name = "var2",
                ReferenceType = new Class { Name = "RefType2" },
                InstanceType = new Class { Name = "InstType2" }
            }
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
