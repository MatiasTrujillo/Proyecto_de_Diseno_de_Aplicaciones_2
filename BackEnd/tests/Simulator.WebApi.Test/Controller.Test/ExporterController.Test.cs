using Microsoft.AspNetCore.Mvc;
using Moq;
using Simulator.Exporters.Abstractions;
using Simulator.IBussinesLogic;
using Simulator.WebApi.Controllers;

namespace Simulator.WebApi.Test.Controller.Test;

[TestClass]
public class ExportersControllerTests
{
    private Mock<IExportService> _mockExportService = null!;
    private ExportersController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockExportService = new Mock<IExportService>();
        _controller = new ExportersController(_mockExportService.Object);
    }

    [TestMethod]
    public void GetAll_ReturnsAvailableExporters()
    {
        var expected = new[] { "Exporter1", "Exporter2" };
        _mockExportService.Setup(service => service.GetAvailableExporters()).Returns(expected);

        var result = _controller.GetAll();

        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Export_ReturnsOkWithExporterTypes_WhenExporterSucceeds()
    {
        var exporterName = "ValidExporter";
        var input = new TransformerForExporter();
        var expectedResult = "{\"success\":true}";
        _mockExportService.Setup(s => s.ExportTransfer(exporterName, input)).Returns(expectedResult);

        var result = _controller.Export(exporterName, input);

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.IsNotNull(okResult.Value);

        var valueType = okResult.Value.GetType();
        var prop = valueType.GetProperty("exporterTypes");
        Assert.IsNotNull(prop);
        var exporterTypesValue = prop.GetValue(okResult.Value);
        Assert.AreEqual(expectedResult, exporterTypesValue);
    }
}
