using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Simulator.Exporters.Abstractions;
using Simulator.IBussinesLogic;

namespace Simulator.BussinesLogic;

public class ExporterService() : IExportService
{
    public string[] GetAvailableExporters()
    {
        var assemblies = LoadAssemblies();
        return [.. assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && typeof(ITransformerExporter).IsAssignableFrom(t))
            .Select(t => ((ITransformerExporter)(Activator.CreateInstance(t) ?? throw new System.Exception())).GetFormat())];
    }

    private static IEnumerable<Assembly> LoadAssemblies()
    {
        var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        DirectoryInfo? projectRoot = currentDirectory;

        while(projectRoot != null && projectRoot.Name != "BackEnd")
        {
            projectRoot = projectRoot.Parent;
        }

        if(projectRoot == null)
        {
            yield break;
        }

        var path1 = Path.Combine(projectRoot.FullName, "src", "Simulator.WebApi", "Exporters");

        if(!Directory.Exists(path1))
        {
            yield break;
        }

        var dllFiles = Directory.GetFiles(path1, "*.dll", SearchOption.TopDirectoryOnly);

        foreach(var dllPath in dllFiles)
        {
            Assembly? assembly = null;

            try
            {
                assembly = Assembly.LoadFrom(dllPath);
                Console.WriteLine($"Assembly cargado: {dllPath}");
            }
            catch(System.Exception ex)
            {
                Console.WriteLine($"Load failed '{dllPath}': {ex.Message}");
            }

            if(assembly != null)
            {
                yield return assembly;
            }
        }
    }
}
