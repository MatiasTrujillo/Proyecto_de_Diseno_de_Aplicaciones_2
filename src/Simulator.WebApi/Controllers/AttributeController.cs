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

    [HttpDelete("{id}")]
    public IActionResult DeleteAttribute([FromRoute] Guid id)
    {
        _attributeService.DeleteAttribute(id);
        return NoContent();
    }
}
