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
}
