using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.DTO;
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

    [TestMethod]
    public void DeleteClass_ShouldReturnNoContent_WhenClassIsDeleted()
    {
        var id = Guid.NewGuid();
        _classServiceMock.Setup(service => service.DeleteClass(id))
            .Verifiable();

        var result = _controller.DeleteClass(id) as NoContentResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(204, result.StatusCode);
        _classServiceMock.Verify(service => service.DeleteClass(id), Times.Once);
    }

    [TestMethod]
    public void UpdateClass_ShouldReturnOk_WhenClassIsUpdated()
    {
        var id = Guid.NewGuid();
        var dto = new UpdateClassDTO { Name = "UpdatedClassName" };

        _classServiceMock.Setup(service => service.UpdateClass(id, dto)).Verifiable();

        var result = _controller.UpdateClass(id, dto) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.IsInstanceOfType(result.Value, typeof(object));

        var response = result.Value?.GetType().GetProperty("message")?.GetValue(result.Value, null) as string;
        Assert.AreEqual("Class updated successfully", response);

        _classServiceMock.Verify(service => service.UpdateClass(id, dto), Times.Once);
    }

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
