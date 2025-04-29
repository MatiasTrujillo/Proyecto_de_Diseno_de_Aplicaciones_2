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

    [TestMethod]
    public void AssociateMethodWithClass_ShouldReturnCreated_WhenAssociationIsSuccessful()
    {
        var methodId = Guid.NewGuid();
        var classId = Guid.NewGuid();
        _methodServiceMock.Setup(service => service.AssociateMethodWithClass(methodId, classId))
            .Verifiable();

        var result = _controller.AssociateMethodWithClass(methodId, classId) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);

        _methodServiceMock.Verify(service => service.AssociateMethodWithClass(methodId, classId), Times.Once);
    }

    [TestMethod]
    public void DeleteMethod_ShouldReturnNoContent_WhenMethodIsDeleted()
    {
        var id = Guid.NewGuid();
        _methodServiceMock.Setup(service => service.DeleteMethod(id))
            .Verifiable();

        var result = _controller.DeleteMethod(id) as NoContentResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(204, result.StatusCode);
        _methodServiceMock.Verify(service => service.DeleteMethod(id), Times.Once);
    }

    [TestMethod]
    public void AddAMethod_ShouldReturnCreated_WhenMethodIsAddedSuccessfully()
    {
        var id = Guid.NewGuid();
        var idToAdd = Guid.NewGuid();
        _methodServiceMock.Setup(service => service.AddAMethod(id, idToAdd))
            .Verifiable();

        var result = _controller.AddAMethod(id, idToAdd) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);

        _methodServiceMock.Verify(service => service.AddAMethod(id, idToAdd), Times.Once);
    }

    [TestMethod]
    public void AddLocalVariableToMethod_ShouldReturnCreated_WhenLocalVariableIsAddedSuccessfully()
    {
        var methodId = Guid.NewGuid();
        var localVariable = new LocalVariableDTO
        {
            Name = "TestVariable",
        };
        _methodServiceMock.Setup(service => service.AddLocalVariableToMethod(methodId, localVariable))
            .Verifiable();

        var result = _controller.AddLocalVariableToMethod(methodId, localVariable) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");

        _methodServiceMock.Verify(service => service.AddLocalVariableToMethod(methodId, localVariable), Times.Once);
    }

    [TestMethod]
    public void AddParameterToMethod_ShouldReturnCreated_WhenParameterIsAddedSuccessfully()
    {
        var methodId = Guid.NewGuid();
        var parameterId = Guid.NewGuid();
        _methodServiceMock.Setup(service => service.AddParameterToMethod(methodId, parameterId))
            .Verifiable();

        var result = _controller.AddParameterToMethod(methodId, parameterId) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");

        _methodServiceMock.Verify(service => service.AddParameterToMethod(methodId, parameterId), Times.Once);
    }

    [TestMethod]
    public void CreateParameter_ShouldReturnCreated_WhenParameterIsCreated()
    {
        var parameterDto = new ParameterDTO
        {
            Name = "TestParameter",
            ParameterType = new TypeDTO { Name = "TestType" }
        };
        var expectedParameterId = Guid.NewGuid();

        _methodServiceMock.Setup(service => service.CreateParameter(parameterDto))
            .Returns(expectedParameterId);

        var result = _controller.CreateParameter(parameterDto) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");

        var createdValue = result.Value?.GetType().GetProperty("id")?.GetValue(result.Value, null);

        Assert.IsNotNull(createdValue, "The created value should contain an 'id'.");
        Assert.AreEqual(expectedParameterId, createdValue, "The returned id should match the expected id.");

        _methodServiceMock.Verify(service => service.CreateParameter(parameterDto), Times.Once);
    }
}
