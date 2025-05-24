using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Filters;

namespace Simulator.WebApi.Test.Filters.Test;

[TestClass]
public class ActiveSessionFilterTests
{
    private Mock<ISessionService> _mockSessionService = null;
    private AuthorizationFilterContext _context = null;
    private DefaultHttpContext _httpContext = null;
    private ActiveSessionFilter _filter = null;

    [TestInitialize]
    public void Setup()
    {
        _mockSessionService = new Mock<ISessionService>();
        _filter = new ActiveSessionFilter();

        _httpContext = new DefaultHttpContext();
        _httpContext.RequestServices = Mock.Of<IServiceProvider>(sp =>
            sp.GetService(typeof(ISessionService)) == _mockSessionService.Object);

        var actionContext = new ActionContext
        {
            HttpContext = _httpContext,
            RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
            ActionDescriptor = new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor()
        };
        _context = new AuthorizationFilterContext(actionContext, [new ActiveSessionFilter()]);
    }

    [TestMethod]
    public void OnAuthorization_TokenValidAndSessionActive_ShouldAllow()
    {
        var token = Guid.NewGuid();
        _httpContext.Request.Headers["X-Session-Token"] = token.ToString();
        _mockSessionService.Setup(s => s.IsActive(token)).Returns(true);

        _filter.OnAuthorization(_context);

        Assert.IsNull(_context.Result);
    }

    [TestMethod]
    public void OnAuthorization_TokenValidButSessionInactive_ShouldReturnUnauthorized()
    {
        var token = Guid.NewGuid();
        _httpContext.Request.Headers["X-Session-Token"] = token.ToString();
        _mockSessionService.Setup(s => s.IsActive(token)).Returns(false);

        _filter.OnAuthorization(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(UnauthorizedObjectResult));
        Assert.AreEqual("La sesión no está activa.", ((UnauthorizedObjectResult)_context.Result).Value);
    }

    [TestMethod]
    public void OnAuthorization_TokenHeaderMissing_ShouldReturnUnauthorized()
    {
        _filter.OnAuthorization(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(UnauthorizedObjectResult));
        Assert.AreEqual("Token de sesión inválido o ausente.", ((UnauthorizedObjectResult)_context.Result).Value);
    }

    [TestMethod]
    public void OnAuthorization_TokenHeaderInvalidGuid_ShouldReturnUnauthorized()
    {
        _httpContext.Request.Headers["X-Session-Token"] = "invalid-guid";

        _filter.OnAuthorization(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(UnauthorizedObjectResult));
        Assert.AreEqual("Token de sesión inválido o ausente.", ((UnauthorizedObjectResult)_context.Result).Value);
    }

    [TestMethod]
    public void OnAuthorization_SessionServiceMissing_ShouldReturnInternalServerError()
    {
        _httpContext.RequestServices = Mock.Of<IServiceProvider>(sp =>
            sp.GetService(typeof(ISessionService)) == null);

        var token = Guid.NewGuid();
        _httpContext.Request.Headers["X-Session-Token"] = token.ToString();

        _filter.OnAuthorization(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(StatusCodeResult));
        Assert.AreEqual(500, ((StatusCodeResult)_context.Result).StatusCode);
    }
}
