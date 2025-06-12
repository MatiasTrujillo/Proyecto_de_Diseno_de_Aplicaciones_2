using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class InterfaceControllertest
{
    private static readonly Mock<IInterfaceService> _interfaceServiceMock = new Mock<IInterfaceService>();
    private readonly InterfaceController _interfaceController = new InterfaceController(_interfaceServiceMock.Object);
    private readonly InterfaceDTO interfaceDto;

    public InterfaceControllertest()
    {
        interfaceDto = new InterfaceDTO()
        {
            Name = "Interfaz"
        };
    }

    [TestMethod]
    public void CreateInterface_ShouldReturnCreated_WhenInterfaceIsCreated()
    {
        _interfaceServiceMock.Setup(service => service.CreateInterface(interfaceDto))
            .Verifiable();
        var result = _interfaceController.CreateInterface(interfaceDto) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
    }

    [TestMethod]
    public void DeleteInterface_ShouldReturnNoContent_WhenInterfaceIsDeleted()
    {
        var id = Guid.NewGuid();
        _interfaceServiceMock.Setup(service => service.DeleteInterface(id))
            .Verifiable();

        var result = _interfaceController.DeleteInterface(id) as NoContentResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(204, result.StatusCode);
        _interfaceServiceMock.Verify(service => service.DeleteInterface(id), Times.Once);
    }

    [TestMethod]
    public void UpdateInterface_ShouldReturnOk_WhenInterfaceIsUpdated()
    {
        var id = Guid.NewGuid();
        var signatureId = Guid.NewGuid();

        _interfaceServiceMock.Setup(service =>
                service.UpdateInterface(
                    It.IsAny<Guid>(),
                    It.Is<UpdateInterfaceDTO>(d => d.IdMethodSignature == signatureId)))
            .Verifiable();
        var result = _interfaceController.AssociateInterfaceSignature(id, signatureId) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        _interfaceServiceMock.Verify(service =>
                service.UpdateInterface(
                    It.IsAny<Guid>(),
                    It.Is<UpdateInterfaceDTO>(d => d.IdMethodSignature == signatureId)),
            Times.Once);
    }

    [TestMethod]
    public void GetInterfaces_ShouldReturnOkWithAllInterfaces()
    {
        // Arrange
        var expectedInterfaces = new List<Interface>
        {
            new Interface { Name = "Interfaz1" },
            new Interface { Name = "Interfaz2" }
        };

        var mockService = new Mock<IInterfaceService>();
        mockService.Setup(s => s.GetInterfaces()).Returns(expectedInterfaces);

        var controller = new InterfaceController(mockService.Object);

        // Act
        var result = controller.GetInterfaces() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        mockService.Verify(s => s.GetInterfaces(), Times.Once);
    }
}
