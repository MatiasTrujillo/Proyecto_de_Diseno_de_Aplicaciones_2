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
            ClassException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            MethodException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            AttributeException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            AttributeAlreadyInClassException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 409 },
            AttributeNotFoundException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 404 },
            ClassInUseException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 409 },
            ClassNotFoundException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 404 },
            InvalidAttributeException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            InvalidClassException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            InvalidMethodException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 400 },
            LocalVariableNotFoundException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 404 },
            MethodInUseException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 409 },
            MethodNotFoundException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 404 },
            ParameterNotFoundException _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 404 },
            _ => new ObjectResult(new { context.Exception.Message }) { StatusCode = 500 }
        };
        context.ExceptionHandled = true;
    }
}
