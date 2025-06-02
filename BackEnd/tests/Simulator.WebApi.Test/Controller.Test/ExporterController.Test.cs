using Moq;
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
        // Arrange
        var expected = new[] { "Exporter1", "Exporter2" };
        _mockExportService.Setup(service => service.GetAvailableExporters()).Returns(expected);

        // Act
        var result = _controller.GetAll();

        // Assert
        CollectionAssert.AreEqual(expected, result);
    }
}
