using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Filters;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/methodSignature")]
public class MethodSignatureController(IMethodService methodService) : ControllerBase
{
    private readonly IMethodService _amethodService = methodService;
    [HttpPost]
    public IActionResult CreateMethod([FromBody] MethodSignatureDTO method)
    {
        var methodId = _amethodService.CreateMethodSignature(method);
        return Created(string.Empty, new { id = methodId });
    }
}
