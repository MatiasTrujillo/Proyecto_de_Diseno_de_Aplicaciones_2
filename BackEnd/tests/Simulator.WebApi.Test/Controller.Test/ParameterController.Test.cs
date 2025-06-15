using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;
using ParameterException = Simulator.Domain.Exception.ParameterException;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class ParameterController_Test
{
    private static readonly Mock<IParameterService> _parameterServiceMock = new Mock<IParameterService>();
    private readonly ParameterController _controller = new ParameterController(_parameterServiceMock.Object);

    [TestMethod]
    public void CreateParameter_ShouldReturnCreated_WhenParameterIsCreated()
    {
        var parameterDto = new ParameterDTO
        {
            Name = "TestParameter",
            ReferenceType = new TypeDTO { Name = "TestType", Kind = "class" },
            InstanceType = new TypeDTO { Name = "TestType", Kind = "class" }
        };
        var expectedParameterId = Guid.NewGuid();

        _parameterServiceMock.Setup(service => service.CreateParameter(parameterDto.ToEntity()))
            .Returns(expectedParameterId);

        var result = _controller.CreateParameter(parameterDto) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");
    }

    [TestMethod]
    public void SetInvokeMethodToParameter_ShouldReturnCreated_WhenAssociationIsSuccessful()
    {
        var parameterId = Guid.NewGuid();
        var invokedMethodId = Guid.NewGuid();

        _parameterServiceMock
            .Setup(service => service.SetInvokeMethodToParameter(parameterId, invokedMethodId))
            .Verifiable();

        var result = _controller.SetInvokeMethodToParameter(parameterId, invokedMethodId) as CreatedResult;

        Assert.IsNotNull(result, "The result should not be null.");
        Assert.AreEqual(201, result.StatusCode, "The status code should be 201 Created.");
        Assert.IsNotNull(result.Value, "The response value should not be null.");

        var message = result.Value?.GetType().GetProperty("message")?.GetValue(result.Value, null)?.ToString();
        Assert.AreEqual("Parameter associated with invoked method successfully", message, "The message should match the expected one.");

        _parameterServiceMock.Verify(service => service.SetInvokeMethodToParameter(parameterId, invokedMethodId), Times.Once);
    }

    [TestMethod]
    public void GetAllParameters_ShouldReturnOkWithAllParameters()
    {
        var expectedParameters = new List<Parameter>
        {
            new Parameter
            {
                Name = "param1",
                InstanceType = new Class { Name = "Class1" },
                ReferenceType = new Class { Name = "Ref1" }
            },
            new Parameter
            {
                Name = "param2",
                InstanceType = new Class { Name = "Class2" },
                ReferenceType = new Class { Name = "Ref2" }
            }
        };

        var mockService = new Mock<IParameterService>();
        mockService.Setup(s => s.GetAllParameters()).Returns(expectedParameters);

        var controller = new ParameterController(mockService.Object);

        var result = controller.GetAllParameters() as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        var value = result.Value as List<ParameterDTO>;
        Assert.IsNotNull(value);
        Assert.AreEqual(2, value.Count);
        Assert.AreEqual("param1", value[0].Name);
        Assert.AreEqual("param2", value[1].Name);
        Assert.AreEqual("Class1", value[0].InstanceType.Name);
        Assert.AreEqual("Class2", value[1].InstanceType.Name);
        Assert.AreEqual("Ref1", value[0].ReferenceType.Name);
        Assert.AreEqual("Ref2", value[1].ReferenceType.Name);

        mockService.Verify(s => s.GetAllParameters(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ParameterException))]
    public void ValidateParameter_ShouldThrow_WhenNameIsReservedKeyword()
    {
        var parameter = new Parameter();
        var method = typeof(Parameter).GetMethod("ValidateParameter", BindingFlags.NonPublic | BindingFlags.Instance);

        try
        {
            method!.Invoke(parameter, new object[] { "base" });
        }
        catch (TargetInvocationException ex)
        {
            throw ex.InnerException ?? ex;
        }
    }

    [TestMethod]
    public void ValidateParameter_ShouldNotThrow_WhenNameIsValid()
    {
        var parameter = new Parameter();
        var method = typeof(Parameter).GetMethod("ValidateParameter", BindingFlags.NonPublic | BindingFlags.Instance);

        try
        {
            method!.Invoke(parameter, new object[] { "ValidName" });
        }
        catch (TargetInvocationException ex)
        {
            Assert.Fail("No exception should be thrown, but got: " + ex.InnerException?.Message);
        }
    }
}
