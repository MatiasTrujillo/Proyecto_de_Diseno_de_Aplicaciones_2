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
}
