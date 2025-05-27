namespace Simulator.Transformer.Abstractions;

public interface ITransformerExporter
{
    string GetFormat();
    string Export(string exporterType);
}
