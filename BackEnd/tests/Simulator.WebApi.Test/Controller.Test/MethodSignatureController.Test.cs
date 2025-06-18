using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.Domain;
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
        _methodServiceMock.Setup(service => service.CreateMethodSignature(signatureDto.ToEntity()))
            .Verifiable();

        var result = _controller.CreateMethod(signatureDto) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
    }

    [TestMethod]
    public void GetMethodSignatures_ShouldReturnOkWithAllSignatures()
    {
        var expectedSignatures = new List<MethodSignature>
        {
            new MethodSignature { Id = Guid.NewGuid(), Name = "Signature1" },
            new MethodSignature { Id = Guid.NewGuid(), Name = "Signature2" }
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

    [TestMethod]
    public void ConserveId_AssignsParameterIdsCorrectly()
    {
        // Arrange
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();

        var dto = new MethodSignatureDTO
        {
            Parameters = new[]
            {
                new ParameterDTO { Id = guid1.ToString() },
                new ParameterDTO { Id = guid2.ToString() }
            }
        };

        var parameters = new List<Parameter>
        {
            new Parameter { Id = Guid.Empty },
            new Parameter { Id = Guid.Empty }
        };

        // Act
        var result = typeof(MethodSignatureDTO)
            .GetMethod("ConserveId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(dto, new object[] { parameters }) as List<Parameter>;

        // Assert
        Assert.AreEqual(guid1, result[0].Id);
        Assert.AreEqual(guid2, result[1].Id);
    }
}
