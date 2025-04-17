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
}
