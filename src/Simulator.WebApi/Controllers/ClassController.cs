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
        _classService.CreateClass(classDto);
        return Created(string.Empty, new { message = "Class created successfully" });
    }

    [HttpPatch("names/{id}")]
    public IActionResult UpdateClassName([FromRoute] Guid id, [FromQuery] string name)
    {
        _classService.UpdateNameClass(id, name);
        return Ok(new { message = "Class updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteClass([FromRoute] Guid id)
    {
        _classService.DeleteClass(id);
        return NoContent();
    }
}
