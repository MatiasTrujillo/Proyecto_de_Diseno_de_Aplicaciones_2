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
        var invokedMethodId = _amethodService.AssociateInvokeMethod(invokedMethod);
        return Created(string.Empty, new { id = invokedMethodId });
    }
}
