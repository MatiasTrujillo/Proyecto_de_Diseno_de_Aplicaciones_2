using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/types")]
public class TypeController(IClassService classService) : ControllerBase
{
    private readonly IClassService _classService = classService;

    [HttpGet]
    public IActionResult GetTypes()
    {
        var types = _classService.GetTypes()
            .Select(t => t.ToDTO())
            .ToList();
        return Ok(types);
    }
}
