using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Filters;

public class ActiveSessionFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var sessionService = context.HttpContext.RequestServices.GetService(typeof(ISessionService)) as ISessionService;

        if(sessionService == null)
        {
            context.Result = new StatusCodeResult(500);
            return;
        }

        var headers = context.HttpContext.Request.Headers;

        if(!headers.TryGetValue("X-Session-Token", out var tokenHeader) ||
            !Guid.TryParse(tokenHeader, out var token))
        {
            context.Result = new UnauthorizedObjectResult("Token de sesión inválido o ausente.");
            return;
        }

        if(!sessionService.IsActive(token))
        {
            context.Result = new UnauthorizedObjectResult("La sesión no está activa.");
        }
    }
}
