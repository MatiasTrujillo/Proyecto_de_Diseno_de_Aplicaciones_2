namespace Simulator.TransformerExporter;

public interface ITransformerExporter
{
    string GetFormat();
    string Export(string exporterType);
}
