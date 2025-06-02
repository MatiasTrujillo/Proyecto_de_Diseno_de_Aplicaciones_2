using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/namespaces")]
public class NameSpaceController(INameSpaceService nameSpaceService) : ControllerBase
{
    private readonly INameSpaceService _nameSpaceService = nameSpaceService;

    [HttpPost]
    public IActionResult CreateNameSpace([FromBody] NameSpaceDTO nameSpace)
    {
        var nameSpaceId = _nameSpaceService.CreateNameSpace(nameSpace);
        return Created(string.Empty, new { id = nameSpaceId });
    }
}
