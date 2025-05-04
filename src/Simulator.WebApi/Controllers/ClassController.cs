using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/classes")]
public class ClassController(IClassService classService) : ControllerBase
{
    private readonly IClassService _classService = classService;

    [HttpPost]
    public IActionResult CreateClass([FromBody] ClassDTO classDto)
    {
        var classId = _classService.CreateClass(classDto);
        return Created(string.Empty, new { id = classId });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateClass([FromRoute] Guid id, [FromBody] UpdateClassDTO uClass)
    {
        _classService.UpdateClass(id, uClass);
        return Ok(new { message = "Class updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteClass([FromRoute] Guid id)
    {
        _classService.DeleteClass(id);
        return NoContent();
    }
}
