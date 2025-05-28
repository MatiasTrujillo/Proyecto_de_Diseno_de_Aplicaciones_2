

using Simulator.Transformer.Abstractions;

namespace Simulator.IBussinesLogic;

public interface IExportService
{
    // string ExportTransfer(string exporterType, TransformerForExporter transformer);
    string[] GetAvailableExporters();
}
