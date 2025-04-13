using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            _ => new ObjectResult(new {Message = context.Exception.Message}) { StatusCode = 500 }
        };
        context.ExceptionHandled = true;
    }
}
