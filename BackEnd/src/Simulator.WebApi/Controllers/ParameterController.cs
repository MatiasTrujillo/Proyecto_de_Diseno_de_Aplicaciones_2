using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/parameters")]
public class ParameterController(IParameterService parameterService) : ControllerBase
{
    private readonly IParameterService _parameterService = parameterService;

    [HttpPost]
    public IActionResult CreateParameter([FromBody] ParameterDTO parameter)
    {
        var parameterId = _parameterService.CreateParameter(parameter);
        return Created(string.Empty, new { id = parameterId });
    }

    [HttpPost("{id}/invokedMethods/{invokedMethodId}")]
    public IActionResult SetInvokeMethodToParameter([FromRoute] Guid id, [FromRoute] Guid invokedMethodId)
    {
        _parameterService.SetInvokeMethodToParameter(id, invokedMethodId);
        return Created(string.Empty, new { message = "Parameter associated with invoked method successfully" });
    }
}
