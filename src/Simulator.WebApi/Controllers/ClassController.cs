using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ClassController(IClassService classService) : ControllerBase
{
    private readonly IClassService _classService = classService;

    [HttpPost]
    public IActionResult CreateClass([FromBody] ClassDTO classDto)
    {
        var classId = _classService.CreateClass(classDto);
        return Created(string.Empty, new { id = classId });
    }

    [HttpPatch("{id}/names")]
    public IActionResult UpdateClassName([FromRoute] Guid id, [FromQuery] string name)
    {
        _classService.UpdateNameClass(id, name);
        return Ok(new { message = "Class updated successfully" });
    }

    [HttpPatch("{id}/stereotypes")]
    public IActionResult UpdateClassStereotype([FromRoute] Guid id, [FromQuery] string stereotype)
    {
        _classService.UpdateClassStereotype(id, stereotype);
        return Ok(new { message = "Class stereotype updated successfully" });
    }

    [HttpPatch("{id}/inherits/{idClass}")]
    public IActionResult UpdateClassInherits([FromRoute] Guid id, [FromRoute] Guid idClass)
    {
        _classService.UpdateClassInherits(id, idClass);
        return Ok(new { message = "Class inheritance updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteClass([FromRoute] Guid id)
    {
        _classService.DeleteClass(id);
        return NoContent();
    }
}
