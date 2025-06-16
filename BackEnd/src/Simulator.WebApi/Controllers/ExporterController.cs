using Microsoft.AspNetCore.Mvc;
using Simulator.Exporters.Abstractions;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Filters;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v2/exporters")]
public sealed class ExportersController(IExportService exporterService) : ControllerBase
{
    [HttpGet]
    public string[] GetAll()
    {
        return exporterService.GetAvailableExporters();
    }

    [AuthorizationFilter]
    [HttpPost("{exporterName}/export")]
    public IActionResult Export(string exporterName, [FromBody] TransformerForExporter data)
    {
        var exporterTypes = exporterService.ExportTransfer(exporterName, data);
        return Ok(new { exporterTypes });
    }
}
