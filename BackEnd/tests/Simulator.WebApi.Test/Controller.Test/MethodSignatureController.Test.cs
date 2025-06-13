using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class MethodSignatureController_Test
{
    private static readonly Mock<IMethodService> _methodServiceMock = new Mock<IMethodService>();
    private readonly MethodSignatureController _controller = new MethodSignatureController(_methodServiceMock.Object);

    [TestMethod]
    public void CreateMethodSignature_ShouldReturnCreated_WhenMethodSignatureIsCreated()
    {
        var signatureDto = new MethodSignatureDTO();
        _methodServiceMock.Setup(service => service.CreateMethodSignature(signatureDto))
            .Verifiable();

        var result = _controller.CreateMethod(signatureDto) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        _methodServiceMock.Verify(service => service.CreateMethodSignature(signatureDto), Times.Once);
    }

    [TestMethod]
    public void GetMethodSignatures_ShouldReturnOkWithAllSignatures()
    {
        var expectedSignatures = new List<MethodSignatureDTO>
        {
            new MethodSignatureDTO { Id = "1", Name = "Signature1" },
            new MethodSignatureDTO { Id = "2", Name = "Signature2" }
        };

        var mockService = new Mock<IMethodService>();
        mockService.Setup(s => s.GetMethodSignatures()).Returns(expectedSignatures);

        var controller = new MethodSignatureController(mockService.Object);

        var result = controller.GetMethodSignatures() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        var value = result.Value as List<MethodSignatureDTO>;
        Assert.IsNotNull(value);
        Assert.AreEqual(2, value.Count);
        Assert.AreEqual("Signature1", value[0].Name);
        Assert.AreEqual("Signature2", value[1].Name);

        mockService.Verify(s => s.GetMethodSignatures(), Times.Once);
    }
}
