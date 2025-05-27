using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Simulator.IBussinesLogic;
using Simulator.Transformer.Abstractions;

namespace Simulator.BussinesLogic;

public class ExporterService(IMethodService service) : IExportService
{
    public string[] GetAvailableExporters()
    {
        var assemblies = LoadAssemblies();
        return [.. assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && typeof(ITransformerExporter).IsAssignableFrom(t))
            .Select(t => ((ITransformerExporter)(Activator.CreateInstance(t) ?? throw new System.Exception())).GetFormat())];
    }

    public string ExportTransfer(string exporterType, TransformerForExporter transformer)
    {
        var assemblies = LoadAssemblies();
        var types = assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && typeof(ITransformerExporter).IsAssignableFrom(t))
            .ToArray();

        var instances = types.Select(t => (ITransformerExporter)(Activator.CreateInstance(t) ?? throw new System.Exception("Exporter not found")));

        var instance = instances.FirstOrDefault(i => i.GetFormat() == exporterType)
                       ?? throw new System.Exception("Exporter not found");

        var transfer = service.ExecuteMethod(transformer.ReferenceId, transformer.ObjectToCreateId, transformer.MethodId);

        return instance.Export(transfer);
    }

    private static IEnumerable<Assembly> LoadAssemblies()
    {
        var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        DirectoryInfo? projectRoot = currentDirectory;

        // Buscar hacia arriba la carpeta llamada "BackEnd"
        while (projectRoot != null && projectRoot.Name != "BackEnd")
        {
            projectRoot = projectRoot.Parent;
        }

        if (projectRoot == null)
        {
            Console.WriteLine("No se encontró la carpeta raíz 'BackEnd'.");
            yield break;
        }

        var path1 = Path.Combine(projectRoot.FullName, "src", "Simulator.WebApi", "Exporters");
        Console.WriteLine("Path: " + path1);

        if (!Directory.Exists(path1))
        {
            Console.WriteLine("Folder not found: " + path1);
            yield break;
        }

        var dllFiles = Directory.GetFiles(path1, "*.dll", SearchOption.TopDirectoryOnly);

        foreach (var dllPath in dllFiles)
        {
            Assembly? assembly = null;

            try
            {
                assembly = Assembly.LoadFrom(dllPath);
                Console.WriteLine($"Assembly cargado: {dllPath}");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Load failed '{dllPath}': {ex.Message}");
            }

            if (assembly != null)
            {
                yield return assembly;
            }
        }
    }
}
