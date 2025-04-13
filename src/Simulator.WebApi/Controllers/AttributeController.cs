using Microsoft.AspNetCore.Mvc;
using Simulator.BussinesLogic.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AttributeController(IAttributeService attributeService) : ControllerBase
{
    private readonly IAttributeService _attributeService = attributeService;

    [HttpPost("{id}")]
    public IActionResult CreateAttribute([FromRoute] Guid id,[FromBody] AttributeDTO attribute)
    {
        _attributeService.CreateAttributeForClass(id,attribute);
        return Created(string.Empty, new { message = "Attribute created successfully" });
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateAttribute([FromRoute] Guid id, [FromBody] AttributeDTO attribute)
    {
        _attributeService.UpdateAttribute(id, attribute);
        return Ok(new { message = "Attribute updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAttribute([FromRoute] Guid id)
    {
        _attributeService.DeleteAttribute(id);
        return NoContent();
    }
}
