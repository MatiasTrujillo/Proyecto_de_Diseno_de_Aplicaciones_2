using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class InterfaceControllertest
{
    [TestMethod]
    public void CreateClass_ShouldReturnCreated_WhenClassIsCreated()
    {
        Mock<IInterfaceService> _interfaceServiceMock = new Mock<IInterfaceService>();
        var controller = new InterfaceController(_interfaceServiceMock.Object);
        var interfaceDto = new InterfaceDTO()
        {
            Name = "Interface"
        };

        _interfaceServiceMock.Setup(service => service.CreateInterface(interfaceDto))
            .Verifiable();
        var result = controller.CreateInterface(interfaceDto) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
    }
}
