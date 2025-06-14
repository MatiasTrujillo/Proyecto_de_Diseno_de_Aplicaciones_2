using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.UpdateDTO;
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
        var classT = new ClassDTO
        {
            Name = "TestClass"
        };

        _classServiceMock.Setup(service => service.CreateClass(It.IsAny<Class>()))
            .Verifiable();

        var result = _controller.CreateClass(classT) as CreatedResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
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

        var result = _controller.UpdateClass(id.ToString(), dto) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.IsInstanceOfType(result.Value, typeof(object));

        var response = result.Value?.GetType().GetProperty("message")?.GetValue(result.Value, null) as string;
        Assert.AreEqual("Class updated successfully", response);

        _classServiceMock.Verify(service => service.UpdateClass(id, dto), Times.Once);
    }

    [TestMethod]
    public void GetClasses_ShouldReturnOkResult_WithListOfClasses()
    {
        var expectedClasses = new List<ClassDTO>
        {
            new ClassDTO { Id = "7f6aa6cc-506e-4773-bd8f-d4872430c15e", Name = "Class1" },
            new ClassDTO { Id = "7f6aa6cc-506e-4773-bd8f-d4872430c12e", Name = "Class2" }
        };
        _classServiceMock.Setup(service => service.GetClasses())
            .Returns(expectedClasses.Select(c =>
            {
                if(c.Id != null)
                {
                    return new Simulator.Domain.Class { Id = Guid.Parse(c.Id), Name = c.Name };
                }

                return null;
            }).ToList()!);

        var result = _controller.GetClasses() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        _classServiceMock.Verify(service => service.GetClasses(), Times.Once);
    }
}
