using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class AttributeControllerTest
{
    private static readonly Mock<IAttributeService> AttributeServiceMock = new Mock<IAttributeService>();
    private readonly AttributeController _controller = new AttributeController(AttributeServiceMock.Object);

    [TestMethod]
    public void CreateAttribute_ShouldReturnCreated_WhenAttributeIsCreated()
    {
        var attribute = new AttributeDto();
        AttributeServiceMock.Setup(service => service.CreateAttribute(attribute))
            .Verifiable();

        var result = _controller.CreateAttribute(attribute) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        AttributeServiceMock.Verify(service => service.CreateAttribute(attribute), Times.Once);
    }

    [TestMethod]
    public void DeleteAttribute_ShouldReturnNoContent_WhenAttributeIsDeleted()
    {
        var id = Guid.NewGuid();
        AttributeServiceMock.Setup(service => service.DeleteAttribute(id))
            .Verifiable();

        var result = _controller.DeleteAttribute(id) as NoContentResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(204, result.StatusCode);
        AttributeServiceMock.Verify(service => service.DeleteAttribute(id), Times.Once);
    }

    [TestMethod]
    public void AsociateAttribute_ShouldCallServiceAndReturnCreated()
    {
        var mockAttributeService = new Mock<IAttributeService>();
        var controller = new AttributeController(mockAttributeService.Object);
        var attribute = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = "TestAttribute",
            Accessibility = Accessibility.Public
        };
        var classId = Guid.NewGuid();

        var result = controller.AsociateAttribute(classId, attribute.Id) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);

        var value = result.Value as dynamic;
        Assert.IsNotNull(value);
        Assert.AreEqual(201, result.StatusCode);

        mockAttributeService.Verify(service => service.AsociateAttribute(classId, attribute.Id), Times.Once);
    }

    [TestMethod]
    public void UpdateAttribute_ShouldReturnOk_WhenAttributeIsUpdated()
    {
        var id = Guid.NewGuid();
        var dto = new UpdateAttributeDTO { Name = "UpdatedAttribute", Accessibility = "private" };

        AttributeServiceMock.Setup(service => service.UpdateAttribute(id, dto)).Verifiable();

        var result = _controller.UpdateAttribute(id, dto) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        dynamic? response = result.Value;
        Assert.IsNotNull(response);

        AttributeServiceMock.Verify(service => service.UpdateAttribute(id, dto), Times.Once);
    }

    [TestMethod]
    public void SetInvokeMethodToAttribute_ShouldCallServiceAndReturnCreated()
    {
        var attributeId = Guid.NewGuid();
        var methodInvokeId = Guid.NewGuid();

        AttributeServiceMock.Setup(service => service.SetInvokeMethodToAttribute(attributeId, methodInvokeId))
            .Verifiable();

        var result = _controller.SetInvokeMethodToAttribute(attributeId, methodInvokeId) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        Assert.IsNotNull(result.Value);

        AttributeServiceMock.Verify(service => service.SetInvokeMethodToAttribute(attributeId, methodInvokeId), Times.Once);
    }

    [TestMethod]
    public void GetAttributes_ShouldReturnOkWithAllAttributes()
    {
        // Arrange
        var expectedAttributes = new List<AttributeDto>
        {
            new AttributeDto { Name = "Atributo1" },
            new AttributeDto { Name = "Atributo2" }
        };

        var mockService = new Mock<IAttributeService>();
        mockService.Setup(s => s.GetAttributes()).Returns(expectedAttributes);

        var controller = new AttributeController(mockService.Object);

        // Act
        var result = controller.GetAttributes() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        var value = result.Value as List<AttributeDto>;
        Assert.IsNotNull(value);
        Assert.AreEqual(2, value.Count);
        Assert.AreEqual("Atributo1", value[0].Name);
        Assert.AreEqual("Atributo2", value[1].Name);

        mockService.Verify(s => s.GetAttributes(), Times.Once);
    }
}
