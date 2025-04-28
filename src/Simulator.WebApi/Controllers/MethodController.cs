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

    [HttpPost("{methodId}/classes/{classId}")]
    public IActionResult AssociateMethodWithClass([FromRoute] Guid methodId, [FromRoute] Guid classId)
    {
        _amethodService.AssociateMethodWithClass(methodId, classId);
        return Created(string.Empty, new { message = "Method associated with class successfully" });
    }

    [HttpPost("{id}/\nmethods/{idToAdd}")]
    public IActionResult AddAMethod([FromRoute] Guid id, [FromRoute] Guid idToAdd)
    {
        _amethodService.AddAMethod(id, idToAdd);
        return Created(string.Empty, new { message = "Method added successfully" });
    }

    [HttpPost("{methodId}/localVariables")]
    public IActionResult AddLocalVariableToMethod([FromRoute] Guid methodId, [FromBody] LocalVariableDTO localVariable)
    {
        _amethodService.AddLocalVariableToMethod(methodId, localVariable);
        return Created(string.Empty, new { message = "Local variable added successfully" });
    }

    [HttpPost("{methodId}/\nparameters")]
    public IActionResult AddParameterToMethod([FromRoute] Guid methodId, [FromBody] ParameterDTO parameter)
    {
        _amethodService.AddParameterToMethod(methodId, parameter);
        return Created(string.Empty, new { message = "Parameter added successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMethod([FromRoute] Guid id)
    {
        _amethodService.DeleteMethod(id);
        return NoContent();
    }
}
