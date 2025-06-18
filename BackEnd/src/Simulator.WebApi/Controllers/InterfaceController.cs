using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.UpdateDTO;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v2/interfaces")]
public class InterfaceController(IInterfaceService interfaceService) : ControllerBase
{
    private readonly IInterfaceService _interfaceService = interfaceService;

    [HttpPost]
    public IActionResult CreateInterface([FromBody] InterfaceDTO interfaceDto)
    {
        var interfaceId = _interfaceService.CreateInterface(interfaceDto.ToEntity());
        return Created(string.Empty, new { id = interfaceId });
    }

    [HttpPost("{id}/methodSignatures/{idMethodSignature}")]
    public IActionResult AssociateInterfaceSignature([FromRoute] Guid id, [FromRoute] Guid idMethodSignature)
    {
        var uInterface = new UpdateInterfaceDTO
        {
            IdMethodSignature = idMethodSignature
        };
        _interfaceService.UpdateInterface(id, uInterface);
        return Ok(new { message = "Interface updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteInterface([FromRoute] Guid id)
    {
        _interfaceService.DeleteInterface(id);
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetInterfaces()
    {
        var interfaces = _interfaceService.GetInterfaces()
            .Select(i => i.ToDTO())
            .ToList();
        return Ok(interfaces);
    }
}
