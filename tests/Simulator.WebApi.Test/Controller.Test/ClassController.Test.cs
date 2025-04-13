using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.BussinesLogic.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class ClassControllerTest
{
    private static readonly Mock<IClassService> _classServiceMock = new Mock<IClassService>();
    private readonly ClassController _controller = new ClassController(_classServiceMock.Object);

    [TestMethod]
    public void CreateClass_ShouldReturnCreated_WhenClassIsCreated()
    {
        var classT = new ClassDTO();
        _classServiceMock.Setup(service => service.CreateClass(classT))
            .Verifiable();

        var result = _controller.CreateClass(classT) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        _classServiceMock.Verify(service => service.CreateClass(classT), Times.Once);
    }
}
