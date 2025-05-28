namespace Simulator.Exporters.Abstractions;

public interface ITransformerExporter
{
    string GetFormat();
    string Export(string exporterType);
}
