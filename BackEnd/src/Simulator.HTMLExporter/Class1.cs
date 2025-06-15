using System.Net;
using Simulator.Exporters.Abstractions;
namespace Simulator.HTMLExporter;

public class Class1
{
    public string GetFormat() => "html";
    public string Export(string data)
    {
        var safeData = WebUtility.HtmlEncode(data);

        return $"""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                    <meta charset="UTF-8">
                    <title>Exported Data</title>
                </head>
                <body>
                    <h1>Export Result</h1>
                    <pre>{safeData}</pre>
                </body>
                </html>
                """;
    }
}
