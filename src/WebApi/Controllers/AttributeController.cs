using Microsoft.AspNetCore.Mvc;
using Simulator.BussinesLogic.DTO;
using Simulator.IBussinesLogic;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AttributeController(IAttributeService attributeService) : ControllerBase
{
    private readonly IAttributeService _attributeService = attributeService;

    [HttpPost]
    public IActionResult CreateAttribute([FromBody] AttributeDTO attribute)
    {
        _attributeService.CreateAttribute(attribute);
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
