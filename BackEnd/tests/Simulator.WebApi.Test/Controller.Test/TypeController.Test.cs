using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class TypeController_Test
{
    private static readonly Mock<IClassService> _classServiceMock = new Mock<IClassService>();
    private readonly TypeController _controller = new TypeController(_classServiceMock.Object);

    [TestMethod]
    public void GetTypes_ShouldReturnOkWithListOfTypesDTO()
    {
        var types = new List<Simulator.Domain.Type>
        {
            new Simulator.Domain.Class(null, null, null!, null!, "ClassA"),
            new Simulator.Domain.Interface("InterfaceA"),
            new Simulator.Domain.Primitive { Name = "int" }
        };

        _classServiceMock.Setup(service => service.GetTypes()).Returns(types);

        var result = _controller.GetTypes() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        var value = result.Value as List<TypeDTO>;
        Assert.IsNotNull(value);
        Assert.AreEqual(3, value.Count);
        Assert.AreEqual("ClassA", value[0].Name);
        Assert.AreEqual("class", value[0].Kind);
        Assert.AreEqual("InterfaceA", value[1].Name);
        Assert.AreEqual("interface", value[1].Kind);
        Assert.AreEqual("int", value[2].Name);
        Assert.AreEqual("primitive", value[2].Kind);

        _classServiceMock.Verify(service => service.GetTypes(), Times.Once);
    }
}
