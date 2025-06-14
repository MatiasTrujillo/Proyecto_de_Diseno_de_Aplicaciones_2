using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/sessions")]
public class SessionController(ISessionService sessionService) : ControllerBase
{
    private readonly ISessionService _amethodService = sessionService;

    [HttpPost]
    public IActionResult SignIn([FromBody] SessionDTO sessionDto)
    {
        var token = _amethodService.SignIn(sessionDto.ToEntity());
        return Ok(new { Token = token });
    }

    [HttpPatch("logins/{token}")]
    public IActionResult LogIn([FromRoute] Guid token)
    {
        _amethodService.LogIn(token);
        return Ok(new { message = "Logged in successfully." });
    }

    [HttpPatch("logout/{token}")]
    public IActionResult LogOut([FromRoute] Guid token)
    {
        _amethodService.LogOut(token);
        return Ok(new { message = "Logged out successfully." });
    }
}
