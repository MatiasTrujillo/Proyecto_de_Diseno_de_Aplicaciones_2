using Microsoft.AspNetCore.Mvc;
using Moq;
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
    public void CreateClass_ShouldReturnCreated_WhenClassIsCreated()
    {
        _interfaceServiceMock.Setup(service => service.CreateInterface(interfaceDto))
            .Verifiable();
        var result = _interfaceController.CreateInterface(interfaceDto) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
    }
}
