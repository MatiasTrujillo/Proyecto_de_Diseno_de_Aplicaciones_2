using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain.Exception;

namespace Simulator.WebApi.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            AttributeNotFoundException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 404 },
            MethodNotFoundException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 404 },
            ClassException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            MethodException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            ClassNotFoundException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 404 },
            AttributeAlreadyInClassException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 409 },
            ClassInUseException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 409 },
            _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 500 }
        };
        context.ExceptionHandled = true;
    }
}
