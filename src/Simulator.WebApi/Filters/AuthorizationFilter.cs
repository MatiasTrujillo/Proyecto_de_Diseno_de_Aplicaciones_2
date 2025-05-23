using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Simulator.WebApi.Filters;

public class AuthorizationFilter : Attribute, IAuthorizationFilter
{
    private static readonly HashSet<Guid> ValidSessions =
    [
        Guid.Parse("b142d2d6-ac0c-4af1-b272-c03e431555f3"),
        Guid.Parse("675c1f61-9dfa-4ead-bae4-dc432ee2bd32"),
        Guid.Parse("756ec724-3e51-4e0d-a857-cceee846a5f6"),
        Guid.Parse("a4a20350-e7e1-4080-b4da-75e875af6b61"),
        Guid.Parse("2070a73f-97a1-4365-a231-dca969fb52fb"),
        Guid.Parse("f6e32298-4362-4ef1-9e6a-e2dfc9e58a47"),
        Guid.Parse("231ecdac-9680-4772-a95a-29abdfd4ad5f"),
        Guid.Parse("66dba073-8375-4ba0-a4bf-b1dfd2e6a0e0"),
        Guid.Parse("5c8f91eb-5bd1-4706-96ca-cfcec81473b8"),
        Guid.Parse("66d9652f-3585-4982-9ab7-8d8159197147")
    ];

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var hasHeader = context.HttpContext.Request.Headers.TryGetValue("Authorization", out var tokenString);
        if (!hasHeader || !Guid.TryParse(tokenString, out var token) || !ValidSessions.Contains(token))
        {
            context.Result = new UnauthorizedObjectResult("Invalid or missing session token.");
        }
    }
}
