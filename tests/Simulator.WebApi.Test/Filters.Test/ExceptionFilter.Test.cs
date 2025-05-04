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

    [TestMethod]
    public void OnException_ShouldSetResultTo404AndHandleMethodNotFoundException_WhenParameterNotFoundExceptionIsThrown()
    {
        var exception = new ParameterNotFoundException("Parameter not found exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(404, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Parameter not found exception", message);
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo409AndHandleAttributeAlreadyInClassException_WhenAttributeAlreadyInClassExceptionIsThrown()
    {
        var exception = new AttributeAlreadyInClassException("Attribute already in class exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(409, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Attribute already in class exception", message);
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo409AndHandleClassInUseException_WhenClassInUseExceptionIsThrown()
    {
        var exception = new ClassInUseException("Class in use exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(409, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Class in use exception", message);
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo400AndHandleInvalidAttributeException_WhenInvalidAttributeExceptionIsThrown()
    {
        var exception = new InvalidAttributeException("Invalid attribute exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(400, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Invalid attribute exception", message);
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo400AndHandleInvalidClassException_WhenInvalidClassExceptionIsThrown()
    {
        var exception = new InvalidClassException("Invalid class exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(400, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Invalid class exception", message);
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo400AndHandleInvalidMethodException_WhenInvalidMethodExceptionIsThrown()
    {
        var exception = new InvalidMethodException("Invalid method exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(400, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Invalid method exception", message);
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo400AndHandleAttributeException_WhenAttributeExceptionIsThrown()
    {
        var exception = new AttributeException("Attribute exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(400, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Attribute exception", message);
    }

    [TestMethod]
    public void OnException_ShouldSetResultTo404AndHandleInvokedMethodException_WhenInvokedMethodNotFoundException()
    {
        var exception = new InvokedMethodNotFoundException("InvokedMethod exception");
        _context.Exception = exception;

        _filter.OnException(_context);

        Assert.IsInstanceOfType(_context.Result, typeof(ObjectResult));
        var result = _context.Result as ObjectResult;
        Assert.AreEqual(404, result?.StatusCode);
        Assert.IsTrue(_context.ExceptionHandled);

        var message = result?.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("InvokedMethod exception", message);
    }
}
