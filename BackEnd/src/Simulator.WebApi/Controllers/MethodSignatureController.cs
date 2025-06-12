using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/methodSignatures")]
public class MethodSignatureController(IMethodService methodService) : ControllerBase
{
    private readonly IMethodService _amethodService = methodService;
    [HttpPost]
    public IActionResult CreateMethod([FromBody] MethodSignatureDTO method)
    {
        var methodId = _amethodService.CreateMethodSignature(method);
        return Created(string.Empty, new { id = methodId });
    }

    [HttpGet]
    public IActionResult GetMethodSignatures()
    {
        var methodSignatures = _amethodService.GetMethodSignatures();
        return Ok(methodSignatures);
    }
}
