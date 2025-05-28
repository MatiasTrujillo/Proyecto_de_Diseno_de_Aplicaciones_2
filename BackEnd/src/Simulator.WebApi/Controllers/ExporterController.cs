using Microsoft.AspNetCore.Mvc;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("exporters")]
public sealed class ExportersController(IExportService exporterService) : ControllerBase
{
    [HttpGet]
    public string[] GetAll()
    {
        return exporterService.GetAvailableExporters();
    }
}
