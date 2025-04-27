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
            AttributeException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            ClassException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            MethodException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            InvalidClassStereotypeException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 500 }
        };
        context.ExceptionHandled = true;
    }
}
