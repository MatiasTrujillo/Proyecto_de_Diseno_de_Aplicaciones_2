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
        _attributeService.CreateAttribute(attribute);
        return Created(string.Empty, new { message = "Attribute created successfully" });
    }

    [HttpPost("{id}")]
    public IActionResult AsociateAttribute(Guid id, [FromBody] AttributeDto attributes)
    {
        _attributeService.AsociateAttribute(id, attributes);
        return Created(string.Empty, new { message = "Attributes associate successfully" });
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateAttribute([FromRoute] Guid id, [FromBody] AttributeDto attribute)
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
