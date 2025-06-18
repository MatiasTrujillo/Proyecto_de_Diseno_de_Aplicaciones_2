using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Simulator.WebApi.Filters;

namespace Simulator.WebApi.Test.Filters.Test;

[TestClass]
public class AuthorizationFilterTests
{
    private AuthorizationFilter _filter = null;

    [TestInitialize]
    public void Setup()
    {
        _filter = new AuthorizationFilter();
    }

    [TestMethod]
    public void OnAuthorization_ValidToken_AllowsAccess()
    {
        var validToken = "b142d2d6-ac0c-4af1-b272-c03e431555f3";
        var context = CreateAuthContextWithHeader("Authorization", validToken);

        _filter.OnAuthorization(context);

        Assert.IsNull(context.Result);
    }

    [TestMethod]
    public void OnAuthorization_InvalidToken_ReturnsUnauthorized()
    {
        var invalidToken = Guid.NewGuid().ToString();
        var context = CreateAuthContextWithHeader("Authorization", invalidToken);

        _filter.OnAuthorization(context);

        Assert.IsInstanceOfType(context.Result, typeof(UnauthorizedObjectResult));
        var result = context.Result as UnauthorizedObjectResult;
        Assert.AreEqual("API Key inválida o ausente", result.Value);
    }

    [TestMethod]
    public void OnAuthorization_MissingHeader_ReturnsUnauthorized()
    {
        var context = CreateAuthContextWithHeader(null, null);

        _filter.OnAuthorization(context);

        Assert.IsInstanceOfType(context.Result, typeof(UnauthorizedObjectResult));
        var result = context.Result as UnauthorizedObjectResult;
        Assert.AreEqual("Missing session token.", result.Value);
    }

    [TestMethod]
    public void OnAuthorization_InvalidGuidFormat_ReturnsUnauthorized()
    {
        var context = CreateAuthContextWithHeader("Authorization", "not-a-guid");

        _filter.OnAuthorization(context);

        Assert.IsInstanceOfType(context.Result, typeof(UnauthorizedObjectResult));
        var result = context.Result as UnauthorizedObjectResult;
        Assert.AreEqual("API Key inválida o ausente", result.Value);
    }

    private AuthorizationFilterContext CreateAuthContextWithHeader(string headerKey, string headerValue)
    {
        var httpContext = new DefaultHttpContext();
        if(headerKey != null && headerValue != null)
        {
            httpContext.Request.Headers[headerKey] = headerValue;
        }

        var actionContext = new ActionContext
        {
            HttpContext = httpContext,
            RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
            ActionDescriptor = new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor()
        };

        return new AuthorizationFilterContext(actionContext, []);
    }

    [TestMethod]
    public void OnAuthorization_BearerPrefix_ValidToken_AllowsAccess()
    {
        var validToken = "b142d2d6-ac0c-4af1-b272-c03e431555f3";
        var context = CreateAuthContextWithHeader("Authorization", "Bearer " + validToken);

        _filter.OnAuthorization(context);

        Assert.IsNull(context.Result);
    }
}
