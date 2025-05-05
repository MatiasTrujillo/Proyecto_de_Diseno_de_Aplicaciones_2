using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/attributes")]
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

    [HttpPut("{id}")]
    public IActionResult UpdateAttribute([FromRoute] Guid id, [FromBody] UpdateAttributeDTO uAttribute)
    {
        _attributeService.UpdateAttribute(id, uAttribute);
        return Ok(new { message = "Attribute updated successfully" });
    }

    [HttpPost("{id}/invokedMethods/{idMethodInvoke}")]
    public IActionResult SetInvokeMethodToAttribute([FromRoute] Guid id, [FromRoute] Guid idMethodInvoke)
    {
        _attributeService.SetInvokeMethodToAttribute(id, idMethodInvoke);
        return Created(string.Empty, new { message = "Method invoke associate successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAttribute([FromRoute] Guid id)
    {
        _attributeService.DeleteAttribute(id);
        return NoContent();
    }
}
