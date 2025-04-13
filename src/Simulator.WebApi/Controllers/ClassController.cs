using Microsoft.AspNetCore.Mvc;
using Simulator.BussinesLogic.DTO;
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
        _classService.CreateClass(classDto);
        return Created(string.Empty, new { message = "Class created successfully" });
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateClass([FromRoute] Guid id, [FromBody] ClassDTO classDto)
    {
        _classService.UpdateClass(id, classDto);
        return Ok(new { message = "Class updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteClass([FromRoute] Guid id)
    {
        return NoContent();
    }
}
