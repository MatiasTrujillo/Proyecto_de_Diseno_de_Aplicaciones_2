using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.BussinesLogic.DTO;
using Simulator.IBussinesLogic;
using WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class AttributeControllerTest
{
    private static readonly Mock<IAttributeService> _attributeServiceMock = new Mock<IAttributeService>();
    private readonly AttributeController _controller = new AttributeController(_attributeServiceMock.Object);

    [TestMethod]
    public void CreateAttribute_ShouldReturnCreated_WhenAttributeIsCreated()
    {
        var attribute = new AttributeDTO();
        _attributeServiceMock.Setup(service => service.CreateAttribute(attribute))
            .Verifiable();

        var result = _controller.CreateAttribute(attribute) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        _attributeServiceMock.Verify(service => service.CreateAttribute(attribute), Times.Once);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldReturnOk_WhenAttributeIsUpdated()
    {
        var id = Guid.NewGuid();
        var attribute = new AttributeDTO();
        _attributeServiceMock.Setup(service => service.UpdateAttribute(id, attribute))
            .Verifiable();

        var result = _controller.UpdateAttribute(id, attribute) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        _attributeServiceMock.Verify(service => service.UpdateAttribute(id, attribute), Times.Once);
    }

    [TestMethod]
    public void DeleteAttribute_ShouldReturnNoContent_WhenAttributeIsDeleted()
    {
        var id = Guid.NewGuid();
        _attributeServiceMock.Setup(service => service.DeleteAttribute(id))
            .Verifiable();

        var result = _controller.DeleteAttribute(id) as NoContentResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(204, result.StatusCode);
        _attributeServiceMock.Verify(service => service.DeleteAttribute(id), Times.Once);
    }
}
