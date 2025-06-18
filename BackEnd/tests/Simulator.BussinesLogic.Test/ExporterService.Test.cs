using System.Net;
using System.Reflection;
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

        var expectedExecutionResult = "Notificador.Notificar() -&gt Notificador.Notificar()\n" +
                                      "\taviso.Enviar() -&gt Notificacion.Enviar()\n" +
                                      "\taviso.Confirmar() -&gt Notificacion.Confirmar()\n" +
                                      "\totroAviso.Enviar() -&gt Email.Enviar()\n" +
                                      "\totroAviso.Confirmar() -&gt Notificacion.Confirmar()";

        _methodServiceMock
            .Setup(s => s.ExecuteMethod(transformer.ReferenceId, transformer.ObjectToCreateId, transformer.MethodId, new List<TypeDTO>()))
            .Returns(expectedExecutionResult);

        _exporterService = new ExporterService(_methodServiceMock.Object);

        var result = _exporterService.ExportTransfer("html", transformer);

        StringAssert.Contains(result, "<html");
        StringAssert.Contains(result, "</html>");

        var preStart = result.IndexOf("<pre>") + "<pre>".Length;
        var preEnd = result.IndexOf("</pre>");
        var preContent = result.Substring(preStart, preEnd - preStart);
        var decodedPreContent = WebUtility.HtmlDecode(preContent);

        Assert.AreEqual(expectedExecutionResult, decodedPreContent);
    }

    [TestMethod]
    public void ExportTransfer_ShouldTrowException_WhenExporterNotExist()
    {
        var transformer = new TransformerForExporter(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        var expectedExecutionResult = "Notificador.Notificar() -&gt Notificador.Notificar()\n" +
                                      "\taviso.Enviar() -&gt Notificacion.Enviar()\n" +
                                      "\taviso.Confirmar() -&gt Notificacion.Confirmar()\n" +
                                      "\totroAviso.Enviar() -&gt Email.Enviar()\n" +
                                      "\totroAviso.Confirmar() -&gt Notificacion.Confirmar()";

        _methodServiceMock
            .Setup(s => s.ExecuteMethod(transformer.ReferenceId, transformer.ObjectToCreateId, transformer.MethodId, new List<TypeDTO>()))
            .Returns(expectedExecutionResult);

        _exporterService = new ExporterService(_methodServiceMock.Object);

        var exp = Assert.ThrowsException<InvalidOperationException>(() => _exporterService.ExportTransfer("nonexistent", transformer));

        Assert.AreEqual(exp.Message, "Exporter type 'nonexistent' not found.");
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

    [TestMethod]
    public void TransformerForExporter_SetReferenceId_ShouldUpdateValue()
    {
        var transformer = new TransformerForExporter();
        var newId = Guid.NewGuid();

        transformer.ReferenceId = newId;

        Assert.AreEqual(newId, transformer.ReferenceId);
    }

    [TestMethod]
    public void TransformerForExporter_SetObjectToCreateId_ShouldUpdateValue()
    {
        var transformer = new TransformerForExporter();
        var newId = Guid.NewGuid();

        transformer.ObjectToCreateId = newId;

        Assert.AreEqual(newId, transformer.ObjectToCreateId);
    }

    [TestMethod]
    public void TransformerForExporter_SetMethodId_ShouldUpdateValue()
    {
        var transformer = new TransformerForExporter();
        var newId = Guid.NewGuid();

        transformer.MethodId = newId;

        Assert.AreEqual(newId, transformer.MethodId);
    }

    [TestMethod]
    public void TransformerForExporter_SetParameterTypes_ShouldUpdateValue()
    {
        var transformer = new TransformerForExporter();
        var types = new List<TypeDTO>
        {
            new TypeDTO(),
            new TypeDTO()
        };

        transformer.ParameterTypes = types;

        Assert.AreEqual(types, transformer.ParameterTypes);
    }

    [TestMethod]
    public void LoadAssemblies_ShouldReturnAssemblies_WhenDllsAreValid()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), "BackEnd");
        var exportersDir = Path.Combine(tempRoot, "src", "Simulator.WebApi", "Exporters");
        Directory.CreateDirectory(exportersDir);

        var systemDll = typeof(object).Assembly.Location;
        var dllCopy = Path.Combine(exportersDir, "mscorlib.dll");
        File.Copy(systemDll, dllCopy);

        AppDomain.CurrentDomain.SetData("APPBASE", exportersDir);

        try
        {
            var result = (IEnumerable<Assembly>)typeof(ExporterService)
                .GetMethod("LoadAssemblies", BindingFlags.NonPublic | BindingFlags.Static)!
                .Invoke(null, null)!;

            Assert.IsTrue(result.Any());
        }
        finally
        {
            Directory.Delete(tempRoot, true);
        }
    }
}
