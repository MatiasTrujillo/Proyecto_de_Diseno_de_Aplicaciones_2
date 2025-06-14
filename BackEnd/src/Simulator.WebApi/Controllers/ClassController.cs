using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.UpdateDTO;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/classes")]
public class ClassController(IClassService classService) : ControllerBase
{
    private readonly IClassService _classService = classService;

    [HttpPost]
    public IActionResult CreateClass([FromBody] ClassDTO classT)
    {
        var classId = _classService.CreateClass(classT.ToEntity());
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
            .Select(c => new ClassDTO
            {
                Name = c.Name,
                Stereotype = c.Stereotype,
                Id = c.Id.ToString()
            })
            .ToList();
        return Ok(classes);
    }
}
