using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/interfaces")]
public class InterfaceController(IInterfaceService interfaceService) : ControllerBase
{
    private readonly IInterfaceService _interfaceService = interfaceService;

    [HttpPost]
    public IActionResult CreateInterface([FromBody] InterfaceDTO interfaceDto)
    {
        var interfaceId = _interfaceService.CreateInterface(interfaceDto);
        return Created(string.Empty, new { id = interfaceId });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteInterface([FromRoute] Guid id)
    {
        _interfaceService.DeleteInterface(id);
        return NoContent();
    }
}
