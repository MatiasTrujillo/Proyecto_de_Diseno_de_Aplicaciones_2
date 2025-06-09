using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/localVariables")]
public class LocalVariableController(ILocalVariableService methodService) : ControllerBase
{
    private readonly ILocalVariableService _amethodService = methodService;

    [HttpPost]
    public IActionResult CreateLocalVariable([FromBody] LocalVariableDTO localVariable)
    {
        var localVariableId = _amethodService.CreateLocalVariable(localVariable);
        return Created(string.Empty, new { id = localVariableId });
    }

    [HttpPost("{id}/invokedMethods/{invokedMethodId}")]
    public IActionResult SetInvokeMethodToLocalVariable([FromRoute] Guid id, [FromRoute] Guid invokedMethodId)
    {
        _amethodService.SetInvokeMethodToLocalVariable(id, invokedMethodId);
        return Created(string.Empty, new { message = "Local variable associated with invoked method successfully" });
    }
}
