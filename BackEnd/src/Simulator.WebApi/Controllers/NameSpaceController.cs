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
        var nameSpaceId = _nameSpaceService.CreateNameSpace(nameSpace.ToEntity());
        return Created(string.Empty, new { id = nameSpaceId });
    }

    [HttpPost("{parentId}/addchilds/{childId}")]
    public IActionResult AddChildToNameSpace(Guid parentId, Guid childId)
    {
        _nameSpaceService.AddChildToNameSpace(parentId, childId);
        return NoContent();
    }

    [HttpDelete("{parentId}/removechilds/{childId}")]
    public IActionResult RemoveChildFromNameSpace(Guid parentId, Guid childId)
    {
        _nameSpaceService.RemoveChildFromNameSpace(parentId, childId);
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetAllNameSpace()
    {
        var nameSpaces = _nameSpaceService.GetAllNameSpace().Select(ns => new NameSpaceDTO
        {
            Id = ns.Id.ToString(),
            Name = ns.Name
        }).ToList();
        return Ok(nameSpaces);
    }
}
