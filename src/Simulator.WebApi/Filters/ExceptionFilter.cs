using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Simulator.WebApi.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 500 }
        };
        context.ExceptionHandled = true;
    }
}
