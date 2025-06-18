namespace Simulator.Exporters.Abstractions;

public interface ITransformerExporter
{
    string Export(string? data);
    string GetFormat();
}
