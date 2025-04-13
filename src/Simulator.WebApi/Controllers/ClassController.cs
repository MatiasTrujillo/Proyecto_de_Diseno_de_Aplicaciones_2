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
}
