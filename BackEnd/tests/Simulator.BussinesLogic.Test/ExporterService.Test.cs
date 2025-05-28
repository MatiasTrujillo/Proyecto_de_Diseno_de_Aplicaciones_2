namespace Simulator.BussinesLogic.Test;

[TestClass]
public class ExporterServiceTests
{
    private readonly string _exportersPath = null!;
    private readonly string _htmlExporterDllPath = null!;

    [TestMethod]
    public void GetAvailableExporters_ShouldReturnHtml_WhenHtmlExporterDllExists()
    {
        var exporterService = new ExporterService();

        var available = exporterService.GetAvailableExporters();

        CollectionAssert.Contains(available.ToList(), "html");
    }

    [TestCleanup]
    public void Cleanup()
    {
        if(File.Exists(_htmlExporterDllPath))
        {
            File.Delete(_htmlExporterDllPath);
        }

        if(Directory.Exists(_exportersPath) && !Directory.EnumerateFileSystemEntries(_exportersPath).Any())
        {
            Directory.Delete(_exportersPath);
        }
    }
}
