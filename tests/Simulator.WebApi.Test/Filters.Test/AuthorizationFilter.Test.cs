using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simulator.WebApi.Filters;
using System;
using System.Collections.Generic;

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
        // Arrange
        var validToken = "b142d2d6-ac0c-4af1-b272-c03e431555f3";
        var context = CreateAuthContextWithHeader("Authorization", validToken);

        // Act
        _filter.OnAuthorization(context);

        // Assert
        Assert.IsNull(context.Result); // No resultado significa acceso permitido
    }

    [TestMethod]
    public void OnAuthorization_InvalidToken_ReturnsUnauthorized()
    {
        // Arrange
        var invalidToken = Guid.NewGuid().ToString();
        var context = CreateAuthContextWithHeader("Authorization", invalidToken);

        // Act
        _filter.OnAuthorization(context);

        // Assert
        Assert.IsInstanceOfType(context.Result, typeof(UnauthorizedObjectResult));
        var result = context.Result as UnauthorizedObjectResult;
        Assert.AreEqual("Invalid or missing session token.", result.Value);
    }

    [TestMethod]
    public void OnAuthorization_MissingHeader_ReturnsUnauthorized()
    {
        // Arrange
        var context = CreateAuthContextWithHeader(null, null);

        // Act
        _filter.OnAuthorization(context);

        // Assert
        Assert.IsInstanceOfType(context.Result, typeof(UnauthorizedObjectResult));
        var result = context.Result as UnauthorizedObjectResult;
        Assert.AreEqual("Invalid or missing session token.", result.Value);
    }

    [TestMethod]
    public void OnAuthorization_InvalidGuidFormat_ReturnsUnauthorized()
    {
        // Arrange
        var context = CreateAuthContextWithHeader("Authorization", "not-a-guid");

        // Act
        _filter.OnAuthorization(context);

        // Assert
        Assert.IsInstanceOfType(context.Result, typeof(UnauthorizedObjectResult));
        var result = context.Result as UnauthorizedObjectResult;
        Assert.AreEqual("Invalid or missing session token.", result.Value);
    }

    private AuthorizationFilterContext CreateAuthContextWithHeader(string headerKey, string headerValue)
    {
        var httpContext = new DefaultHttpContext();
        if (headerKey != null && headerValue != null)
        {
            httpContext.Request.Headers[headerKey] = headerValue;
        }

        var actionContext = new ActionContext
        {
            HttpContext = httpContext,
            RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
            ActionDescriptor = new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor()
        };

        return new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>());
    }
}
