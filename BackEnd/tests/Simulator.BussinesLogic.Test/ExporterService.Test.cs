using Simulator.Exporters.Abstractions;

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

    [TestMethod]
    public void TransformerForExporter_DefaultConstructor_ShouldInitializeWithEmptyGuids()
    {
        var transformer = new TransformerForExporter();

        Assert.AreEqual(Guid.Empty, transformer.MethodId);
        Assert.AreEqual(Guid.Empty, transformer.ObjectToCreateId);
        Assert.AreEqual(Guid.Empty, transformer.ReferenceId);
    }

    [TestMethod]
    public void TransformerForExporter_ParameterizedConstructor_ShouldSetValuesCorrectly()
    {
        var methodId = Guid.NewGuid();
        var objectToCreateId = Guid.NewGuid();
        var referenceId = Guid.NewGuid();

        var transformer = new TransformerForExporter(referenceId, objectToCreateId, methodId);

        Assert.AreEqual(methodId, transformer.MethodId);
        Assert.AreEqual(objectToCreateId, transformer.ObjectToCreateId);
        Assert.AreEqual(referenceId, transformer.ReferenceId);
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
