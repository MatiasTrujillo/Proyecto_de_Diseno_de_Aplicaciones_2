using Simulator.Exporters.Abstractions;

namespace Simulator.HTMLExporter;

public class Class1 : ITransformerExporter
{
    public string GetFormat() => "html";
    public string Export(string data)
    {
        return $"""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                    <meta charset="UTF-8">
                    <title>Exported Data</title>
                </head>
                <body>
                    <h1>Export Result</h1>
                    <p>{data}</p>
                </body>
                </html>
                """;
    }
}
