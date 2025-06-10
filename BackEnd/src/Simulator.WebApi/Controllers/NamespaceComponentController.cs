using Microsoft.AspNetCore.Mvc;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/namespaceComponents")]
public class NamespaceComponentController(INameSpaceService nameSpaceService) : ControllerBase
{
    private readonly INameSpaceService _nameSpaceService = nameSpaceService;

    [HttpGet]
    public IActionResult GetAllNamespaceComponents()
    {
        var nameSpaces = _nameSpaceService.GetAllNamespaceComponents();
        return Ok(nameSpaces);
    }
}
