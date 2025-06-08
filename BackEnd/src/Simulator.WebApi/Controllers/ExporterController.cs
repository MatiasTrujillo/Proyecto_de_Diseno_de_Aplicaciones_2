using Microsoft.AspNetCore.Mvc;
using Simulator.Exporters.Abstractions;
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

    [HttpPost("{exporterName}/export")]
    public IActionResult Export(string exporterName, [FromBody] TransformerForExporter data)
    {
        var result = exporterService.ExportTransfer(exporterName, data);
        return Ok(result);
    }
}
