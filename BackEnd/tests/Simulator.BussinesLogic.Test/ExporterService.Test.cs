using Moq;
using Simulator.Exporters.Abstractions;
using Simulator.IBussinesLogic;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class ExporterServiceTests
{
    private readonly string _exportersPath = null!;
    private readonly string _htmlExporterDllPath = null!;
    private Mock<IMethodService> _methodServiceMock = null!;
    private ExporterService _exporterService = null!;

    [TestInitialize]
    public void Setup()
    {
        _methodServiceMock = new Mock<IMethodService>();
        _exporterService = new ExporterService();
    }

    [TestMethod]
    public void GetAvailableExporters_ShouldReturnHtml_WhenHtmlExporterDllExists()
    {
        var exporterService = new ExporterService();

        var available = exporterService.GetAvailableExporters();
        _methodServiceMock = new Mock<IMethodService>();

        _exporterService = new ExporterService();

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

    [TestMethod]
    public void ExportTransfer_ShouldReturnHtmlWrappedString_WhenHtmlExporterIsAvailable()
    {
        var transformer = new TransformerForExporter(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        var expectedExecutionResult = "Notificador.Notificar() -> Notificador.Notificar()\n" +
                                      "\taviso.Enviar() -> Notificacion.Enviar()\n" +
                                      "\taviso.Confirmar() -> Notificacion.Confirmar()\n" +
                                      "\totroAviso.Enviar() -> Email.Enviar()\n" +
                                      "\totroAviso.Confirmar() -> Notificacion.Confirmar()";

        _methodServiceMock
            .Setup(s => s.ExecuteMethod(transformer.ReferenceId, transformer.ObjectToCreateId, transformer.MethodId))
            .Returns(expectedExecutionResult);

        _exporterService = new ExporterService(_methodServiceMock.Object); // importante si espera dependencia

        var result = _exporterService.ExportTransfer("html", transformer);

        StringAssert.Contains(result, "<html");
        StringAssert.Contains(result, expectedExecutionResult); // ahora sí va a matchear
        StringAssert.Contains(result, "</html>");
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
