using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.Domain;
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

        _methodServiceMock.Setup(service => service.AssociateInvokeMethod(invokedMethodDto.ToEntity()))
            .Returns(expectedId);

        var result = _controller.AssociateInvokeMethod(invokedMethodDto) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");
    }

    [TestMethod]
    public void GetInvokedMethodById_ReturnsOkWithInvokedMethods()
    {
        var expectedList = new List<InvokedMethod>
        {
            new InvokedMethod { Id = Guid.NewGuid(), MethodId = Guid.NewGuid(), ReferenceName = "ref1" },
            new InvokedMethod { Id = Guid.NewGuid(), MethodId = Guid.NewGuid(), ReferenceName = "ref2" }
        };

        var methodServiceMock = new Mock<IMethodService>();
        methodServiceMock.Setup(s => s.GetInvokedMethodById()).Returns(expectedList);

        var controller = new InvokedMethodController(methodServiceMock.Object);

        var result = controller.GetInvokedMethodById();

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var returnedDtos = okResult.Value as List<InvokedMethodDTO>;
        Assert.IsNotNull(returnedDtos);
        Assert.AreEqual(expectedList.Count, returnedDtos.Count);
        for(var i = 0; i < expectedList.Count; i++)
        {
            Assert.AreEqual(expectedList[i].Id.ToString(), returnedDtos[i].Id);
            Assert.AreEqual(expectedList[i].MethodId, returnedDtos[i].MethodId);
            Assert.AreEqual(expectedList[i].ReferenceName, returnedDtos[i].ReferenceName);
        }
    }
}
