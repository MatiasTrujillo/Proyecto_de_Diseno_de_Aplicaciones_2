using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class SessionControllerTests
{
    private Mock<ISessionService> _mockSessionService = null!;
    private SessionController _controller = null!;
    private Guid _testToken;
    private SessionDTO _testSessionDto = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockSessionService = new Mock<ISessionService>(MockBehavior.Strict);
        _controller = new SessionController(_mockSessionService.Object);

        _testToken = Guid.NewGuid();
        _testSessionDto = new SessionDTO { Token = _testToken };
    }

    [TestMethod]
    public void SignIn_ReturnsOkWithToken()
    {
        _mockSessionService.Setup(s => s.SignIn(_testSessionDto)).Returns(_testToken);

        var result = _controller.SignIn(_testSessionDto) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public void LogIn_ReturnsOk()
    {
        _mockSessionService.Setup(s => s.LogIn(_testToken));

        var result = _controller.LogIn(_testToken) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual("Logged in successfully.", result.Value);
    }

    [TestMethod]
    public void LogOut_ReturnsOk()
    {
        _mockSessionService.Setup(s => s.LogOut(_testToken));

        var result = _controller.LogOut(_testToken) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual("Logged out successfully.", result.Value);
    }

    [TestMethod]
    [ExpectedException(typeof(UnauthorizedAccessException))]
    public void LogIn_ThrowsUnauthorizedException()
    {
        _mockSessionService.Setup(s => s.LogIn(_testToken)).Throws(new UnauthorizedAccessException());

        _controller.LogIn(_testToken); // Should throw
    }

    [TestMethod]
    [ExpectedException(typeof(UnauthorizedAccessException))]
    public void LogOut_ThrowsUnauthorizedException()
    {
        _mockSessionService.Setup(s => s.LogOut(_testToken)).Throws(new UnauthorizedAccessException());

        _controller.LogOut(_testToken); // Should throw
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SignIn_ThrowsInvalidOperationException()
    {
        _mockSessionService.Setup(s => s.SignIn(_testSessionDto)).Throws(new InvalidOperationException());

        _controller.SignIn(_testSessionDto); // Should throw
    }
}
