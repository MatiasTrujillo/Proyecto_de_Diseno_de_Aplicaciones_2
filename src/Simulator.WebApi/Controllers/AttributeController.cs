using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AttributeController(IAttributeService attributeService) : ControllerBase
{
    private readonly IAttributeService _attributeService = attributeService;

    [HttpPost]
    public IActionResult CreateAttribute([FromBody] AttributeDto attribute)
    {
        var createdAttributeId = _attributeService.CreateAttribute(attribute);
        return Created(string.Empty, new { id = createdAttributeId });
    }

    [HttpPost("{id}/classes/{idClass}")]
    public IActionResult AsociateAttribute([FromRoute] Guid id, [FromRoute] Guid idClass)
    {
        _attributeService.AsociateAttribute(id, idClass);
        return Created(string.Empty, new { message = "Attributes associate successfully" });
    }

    [HttpPatch("{id}/names/")]
    public IActionResult UpdateAttribute([FromRoute] Guid id, [FromQuery] string name)
    {
        _attributeService.UpdateAttributeName(id, name);
        return Ok(new { message = "Attribute updated successfully" });
    }

    [HttpPatch("{id}/accessibility/")]
    public IActionResult UpdateAttributeAccessibiliti([FromRoute] Guid id, [FromQuery] string accessibility)
    {
        _attributeService.UpdateAttributeAccessibility(id, accessibility);
        return Ok(new { message = "Attribute accessibility updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAttribute([FromRoute] Guid id)
    {
        _attributeService.DeleteAttribute(id);
        return NoContent();
    }
}
