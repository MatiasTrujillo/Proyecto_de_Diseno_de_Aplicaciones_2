using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/invokedMethods")]
public class InvokedMethodController(IMethodService methodService) : ControllerBase
{
    private readonly IMethodService _amethodService = methodService;
    [HttpPost]
    public IActionResult AssociateInvokeMethod([FromBody] InvokedMethodDTO invokedMethod)
    {
        var invokedMethodId = _amethodService.AssociateInvokeMethod(invokedMethod.ToEntity());
        return Created(string.Empty, new { id = invokedMethodId });
    }

    [HttpGet]
    public IActionResult GetInvokedMethodById()
    {
        var invokedMethods = _amethodService.GetInvokedMethodById().Select(iV => new InvokedMethodDTO
        {
            Id = iV.Id.ToString(),
            ReferenceName = iV.ReferenceName,
            MethodId = iV.MethodId
        }).ToList();
        return Ok(invokedMethods);
    }
}
