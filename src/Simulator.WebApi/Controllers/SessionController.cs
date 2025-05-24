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
        var token = _amethodService.SignIn(sessionDto);
        return Ok(new { Token = token });
    }

    [HttpPatch("logins")]
    public IActionResult LogIn([FromBody] Guid token)
    {
        _amethodService.LogIn(token);
        return Ok("Logged in successfully.");
    }

    [HttpPatch("logout")]
    public IActionResult LogOut([FromBody] Guid token)
    {
        _amethodService.LogOut(token);
        return Ok("Logged out successfully.");
    }
}
