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
    public IActionResult CreateClass([FromBody] ClassDTO classT)
    {
        var classId = _classService.CreateClass(classT);
        return Created(string.Empty, new { id = classId });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateClass([FromRoute] string id, [FromBody] UpdateClassDTO uClass)
    {
        _classService.UpdateClass(Guid.Parse(id), uClass);
        return Ok(new { message = "Class updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteClass([FromRoute] Guid id)
    {
        _classService.DeleteClass(id);
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetClasses()
    {
        var classes = _classService.GetClasses()
            .Select(c => c.ToDTO())
            .ToList();
        return Ok(classes);
    }

    [HttpGet("{id}")]
    public IActionResult GetClassById([FromRoute] Guid id)
    {
        var classDto = _classService.GetClassId(id);
        return Ok(classDto);
    }
}
