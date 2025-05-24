using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Filters;

namespace Simulator.WebApi.Controllers;

[ApiController]
[ActiveSessionFilter]
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

    [HttpPost("{id}/classes/{classId}")]
    public IActionResult AssociateNameSpaceWithClass([FromRoute] Guid id, [FromRoute] Guid classId)
    {
        _nameSpaceService.AsociateToClass(id, classId);
        return Created(string.Empty, new { message = "Namespace associated with class successfully" });
    }

    [HttpPost("{id}/interfaces/{interfaceId}")]
    public IActionResult AssociateNameSpaceWithInterface([FromRoute] Guid id, [FromRoute] Guid interfaceId)
    {
        _nameSpaceService.AsociateToInterface(id, interfaceId);
        return Created(string.Empty, new { message = "Namespace associated with interface successfully" });
    }
}
