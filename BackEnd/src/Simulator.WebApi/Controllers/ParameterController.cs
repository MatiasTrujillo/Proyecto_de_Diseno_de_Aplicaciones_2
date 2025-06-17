using Microsoft.AspNetCore.Mvc;
using Simulator.DTO;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v2/parameters")]
public class ParameterController(IParameterService parameterService) : ControllerBase
{
    private readonly IParameterService _parameterService = parameterService;

    [HttpPost]
    public IActionResult CreateParameter([FromBody] ParameterDTO parameter)
    {
        var parameterId = _parameterService.CreateParameter(parameter.ToEntity());
        return Created(string.Empty, new { id = parameterId });
    }

    [HttpPost("{id}/invokedMethods/{invokedMethodId}")]
    public IActionResult SetInvokeMethodToParameter([FromRoute] Guid id, [FromRoute] Guid invokedMethodId)
    {
        _parameterService.SetInvokeMethodToParameter(id, invokedMethodId);
        return Created(string.Empty, new { message = "Parameter associated with invoked method successfully" });
    }

    [HttpGet]
    public IActionResult GetAllParameters()
    {
        var parameters = _parameterService.GetAllParameters().Select(p => new ParameterDTO
        {
            Id = p.Id.ToString(),
            Name = p.Name,
            ReferenceType = new TypeDTO
            {
                Name = p.ReferenceType?.Name!,
                Kind = p.ReferenceType!.GetType().Name.ToLower()
            },
            InstanceType = new TypeDTO
            {
                Name = p.InstanceType!.Name,
                Kind = p.InstanceType.GetType().Name.ToLower()
            }
        }).ToList();
        return Ok(parameters);
    }
}
