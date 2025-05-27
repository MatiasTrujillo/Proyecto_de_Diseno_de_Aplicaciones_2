using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/parameters")]
public class ParameterController(IMethodService methodService) : ControllerBase
{
    private readonly IMethodService _amethodService = methodService;

    // [AuthorizationFilter]
    [HttpPost]
    public IActionResult CreateParameter([FromBody] ParameterDTO parameter)
    {
        var parameterId = _amethodService.CreateParameter(parameter);
        return Created(string.Empty, new { id = parameterId });
    }

    [HttpPost("{id}/invokedMethods/{invokedMethodId}")]
    public IActionResult SetInvokeMethodToParameter([FromRoute] Guid id, [FromRoute] Guid invokedMethodId)
    {
        _amethodService.SetInvokeMethodToParameter(id, invokedMethodId);
        return Created(string.Empty, new { message = "Parameter associated with invoked method successfully" });
    }
}
