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
        _amethodService.CreateMethod(method);
        return Created(string.Empty, new { message = "Method created successfully" });
    }

    [HttpPost("{methodId}/associate/{classId}")]
    public IActionResult AssociateMethodWithClass([FromRoute] Guid methodId, [FromRoute] Guid classId)
    {
        _amethodService.AssociateMethodWithClass(methodId, classId);
        return Created(string.Empty, new { message = "Method associated with class successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMethod([FromRoute] Guid id)
    {
        _amethodService.DeleteMethod(id);
        return NoContent();
    }

    [HttpPost("{id}/add/{idToAdd}")]
    public IActionResult AddAMethod([FromRoute] Guid id, [FromRoute] Guid idToAdd)
    {
        _amethodService.AddAMethod(id, idToAdd);
        return Created(string.Empty, new { message = "Method added successfully" });
    }
}
