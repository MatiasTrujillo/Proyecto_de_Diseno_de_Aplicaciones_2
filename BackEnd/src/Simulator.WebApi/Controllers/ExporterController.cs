using Microsoft.AspNetCore.Mvc;
using Simulator.Exporters.Abstractions;
using Simulator.IBussinesLogic;

namespace Simulator.WebApi.Controllers;

[ApiController]
[Route("api/v1/exporters")]
public sealed class ExportersController(IExportService exporterService) : ControllerBase
{
    [HttpGet]
    public string[] GetAll()
    {
        return exporterService.GetAvailableExporters();
    }

    [HttpPost("{exporterName}/export")]
    public IActionResult Export(string exporterName, [FromBody] TransformerForExporter data)
    {
        var exporterTypes = exporterService.ExportTransfer(exporterName, data);
        return Ok(new { exporterTypes });
    }
}
