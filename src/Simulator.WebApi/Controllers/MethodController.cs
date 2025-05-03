using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MethodController(IMethodService methodService) : ControllerBase
{
    private readonly IMethodService _amethodService = methodService;

    [HttpPost]
    public IActionResult CreateMethod([FromBody] MethodDTO method)
    {
        var methodId = _amethodService.CreateMethod(method);
        return Created(string.Empty, new { id = methodId });
    }

    [HttpPost("parameters")]
    public IActionResult CreateParameter([FromBody] ParameterDTO parameter)
    {
        var parameterId = _amethodService.CreateParameter(parameter);
        return Created(string.Empty, new { id = parameterId });
    }

    [HttpPost("localVariables")]
    public IActionResult CreateLocalVariable([FromBody] LocalVariableDTO localVariable)
    {
        var localVariableId = _amethodService.CreateLocalVariable(localVariable);
        return Created(string.Empty, new { id = localVariableId });
    }

    [HttpPost("{methodId}/classes/{classId}")]
    public IActionResult AssociateMethodWithClass([FromRoute] Guid methodId, [FromRoute] Guid classId)
    {
        _amethodService.AssociateMethodWithClass(methodId, classId);
        return Created(string.Empty, new { message = "Method associated with class successfully" });
    }

    [HttpPost("{id}/invokedMethod")]
    public IActionResult AddAMethod([FromRoute] Guid id, [FromBody] InvokedMethodDTO invokedMethodDto)
    {
        _amethodService.AddAMethod(id, invokedMethodDto);
        return Created(string.Empty, new { message = "Method added successfully" });
    }

    [HttpPost("{methodId}/localVariables/{localVariableId}")]
    public IActionResult AddLocalVariableToMethod([FromRoute] Guid methodId, [FromRoute] Guid localVariableId)
    {
        _amethodService.AddLocalVariableToMethod(methodId, localVariableId);
        return Created(string.Empty, new { message = "Local variable added successfully" });
    }

    [HttpPost("{methodId}/parameters/{parameterId}")]
    public IActionResult AddParameterToMethod([FromRoute] Guid methodId, [FromRoute] Guid parameterId)
    {
        _amethodService.AddParameterToMethod(methodId, parameterId);
        return Created(string.Empty, new { message = "Parameter added successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMethod([FromRoute] Guid id)
    {
        _amethodService.DeleteMethod(id);
        return NoContent();
    }
}
