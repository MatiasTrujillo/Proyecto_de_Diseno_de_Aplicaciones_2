using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/localVariables")]
public class LocalVariableController(IMethodService methodService) : ControllerBase
{
    private readonly IMethodService _amethodService = methodService;

    [HttpPost]
    public IActionResult CreateLocalVariable([FromBody] LocalVariableDTO localVariable)
    {
        var localVariableId = _amethodService.CreateLocalVariable(localVariable);
        return Created(string.Empty, new { id = localVariableId });
    }

    [HttpPost("{localVariableId}/invokedMethods/{invokedMethodId}")]
    public IActionResult SetInvokeMethodToLocalVariable([FromRoute] Guid localVariableId, [FromRoute] Guid invokedMethodId)
    {
        _amethodService.SetInvokeMethodToLocalVariable(localVariableId, invokedMethodId);
        return Created(string.Empty, new { message = "Local variable associated with invoked method successfully" });
    }
}
