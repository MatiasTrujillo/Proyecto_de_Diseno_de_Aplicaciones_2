using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebApi.Filters;

namespace Simulator.WebApi.Test.Filters.Test;

[TestClass]
public class ExceptionFilter_Test
{
    [TestMethod]
    public void OnException_ShouldSetResultTo500AndHandleException()
    {
        var exception = new Exception("Test exception");
        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(
            httpContext,
            new Microsoft.AspNetCore.Routing.RouteData(),
            new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
        );
        var context = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = exception
        };
        var filter = new ExceptionFilter();

        filter.OnException(context);

        Assert.IsInstanceOfType(context.Result, typeof(ObjectResult));
        var result = context.Result as ObjectResult;
        Assert.AreEqual(500, result.StatusCode);
        Assert.IsTrue(context.ExceptionHandled);
        var message = result.Value?.GetType().GetProperty("Message")?.GetValue(result.Value)?.ToString();
        Assert.AreEqual("Test exception", message);
    }
}
