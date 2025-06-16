using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v2/namespaceComponents")]
public class NamespaceComponentController(INameSpaceService nameSpaceService) : ControllerBase
{
    private readonly INameSpaceService _nameSpaceService = nameSpaceService;

    [HttpGet]
    public IActionResult GetAllNamespaceComponents()
    {
        var nameSpaces = _nameSpaceService.GetAllNamespaceComponents().Select(ns => new NamespaceComponentDTO
        {
            Id = ns.Id.ToString(),
            Name = ns.Name
        }).ToList();
        return Ok(nameSpaces);
    }
}
