using Moq;
using Simulator.BussinesLogic;
using Simulator.IBussinesLogic;

[TestClass]
public class ExporterServiceTests
{
    private string _exportersPath = null!;
    private string _htmlExporterDllPath = null!;

    [TestMethod]
    public void GetAvailableExporters_ShouldReturnHtml_WhenHtmlExporterDllExists()
    {
        var methodServiceMock = new Mock<IMethodService>();
        var exporterService = new ExporterService(methodServiceMock.Object);

        var available = exporterService.GetAvailableExporters();

        CollectionAssert.Contains(available.ToList(), "html");
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_htmlExporterDllPath))
        {
            File.Delete(_htmlExporterDllPath);
        }

        if (Directory.Exists(_exportersPath) && !Directory.EnumerateFileSystemEntries(_exportersPath).Any())
        {
            Directory.Delete(_exportersPath);
        }
    }
}
