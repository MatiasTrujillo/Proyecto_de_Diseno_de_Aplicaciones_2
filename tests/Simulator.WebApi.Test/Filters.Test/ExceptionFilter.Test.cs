using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain.Exception;
using Simulator.WebApi.Filters;

namespace Simulator.WebApi.Test.Filters.Test;

[TestClass]
public class ExceptionFilterTest
{
    private ExceptionFilter? _filter = new ExceptionFilter();
    private ExceptionContext? _context = null;
    private ActionContext? _actionContext = null;
    private DefaultHttpContext? _httpContext = null;

    [TestInitialize]
    public void Setup()
    {
        _httpContext = new DefaultHttpContext();
        _actionContext = new ActionContext(
            _httpContext,
            new Microsoft.AspNetCore.Routing.RouteData(),
            new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
        _context = new ExceptionContext(_actionContext, []);
        _filter = new ExceptionFilter();
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo500AndHandleException_WhenGenericExceptionIsThrown()
    {
        var exception = new Exception("Test exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(500, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Test exception", message);
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo400AndHandleMethodException_WhenMethodExceptionIsThrown()
    {
        var exception = new MethodException("Method exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(400, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Method exception", message);
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo404AndHandleAttributeNotFoundException_WhenAttributeNotFoundExceptionIsThrown()
    {
        var exception = new AttributeNotFoundException("Attribute not found exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(404, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Attribute not found exception", message);
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo404AndHandleClassNotFoundException_WhenClassNotFoundExceptionIsThrown()
    {
        var exception = new ClassNotFoundException("Class not found exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(404, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Class not found exception", message);
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo404AndHandleMethodNotFoundException_WhenMethodNotFoundExceptionIsThrown()
    {
        var exception = new MethodNotFoundException("Method not found exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(404, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Method not found exception", message);
    }
}
