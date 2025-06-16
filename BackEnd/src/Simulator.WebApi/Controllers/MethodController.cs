using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v2/methods")]
public class MethodController(IMethodService methodService) : ControllerBase
{
    private readonly IMethodService _amethodService = methodService;

    [HttpPost]
    public IActionResult CreateMethod([FromBody] MethodDTO method)
    {
        var methodId = _amethodService.CreateMethod(method.ToEntity());
        return Created(string.Empty, new { id = methodId });
    }

    [HttpPost("{id}/classes/{classId}")]
    public IActionResult AssociateMethodWithClass([FromRoute] Guid id, [FromRoute] Guid classId)
    {
        _amethodService.AssociateMethodWithClass(id, classId);
        return Created(string.Empty, new { message = "Method associated with class successfully" });
    }

    [HttpPost("{id}/invokedMethods/{invokedMethodId}")]
    public IActionResult AddAMethod([FromRoute] Guid id, [FromRoute] Guid invokedMethodId)
    {
        _amethodService.AddAMethod(id, invokedMethodId);
        return Created(string.Empty, new { message = "Method added successfully" });
    }

    [HttpPost("{id}/localVariables/{localVariableId}")]
    public IActionResult AddLocalVariableToMethod([FromRoute] Guid id, [FromRoute] Guid localVariableId)
    {
        _amethodService.AddLocalVariableToMethod(id, localVariableId);
        return Created(string.Empty, new { message = "Local variable added successfully" });
    }

    [HttpPost("{id}/parameters/{parameterId}")]
    public IActionResult AddParameterToMethod([FromRoute] Guid id, [FromRoute] Guid parameterId)
    {
        _amethodService.AddParameterToMethod(id, parameterId);
        return Created(string.Empty, new { message = "Parameter added successfully" });
    }

    [HttpPost("{methodId}/referenceClass/{referenceId}/objectToCreateClass/{objectToCreateId}")]
    public IActionResult ExecuteMethod([FromRoute] Guid referenceId, [FromRoute] Guid objectToCreateId, [FromRoute] Guid methodId, [FromBody] List<TypeDTO> parameterTypes)
    {
        var result = _amethodService.ExecuteMethod(referenceId, objectToCreateId, methodId, parameterTypes);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMethod([FromRoute] Guid id)
    {
        _amethodService.DeleteMethod(id);
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetMethods()
    {
        var methods = _amethodService.GetMethods().Select(m => new MethodDTO
        {
            Id = m.Id.ToString(),
            Name = m.Name,
            Accessibility = m.Accessibility,
            Stereotype = m.Stereotype,
            ReturnType = m.ReturnType?.ToDTO(),
        }).ToList();
        return Ok(methods);
    }
}
