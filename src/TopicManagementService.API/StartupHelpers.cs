using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using Serilog;
using TopicManagementService.Common.Services.Interfaces;

public static class StartupHelpers
{
    private const string TopicAssembliesSuffix = "Topic";

    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }

    public static WebApplicationBuilder RegisterServicesFromReferencedAssemblies(this WebApplicationBuilder builder)
        => builder.RegisterServicesFromAssemblies(GetReferencedAssemblies());

    public static WebApplicationBuilder RegisterServicesFromAssemblies(this WebApplicationBuilder builder, List<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var assemblyVariants = assembly.GetTypes()
                .Where(t => typeof(IServiceRegistration).IsAssignableFrom(t) && !t.IsInterface)
                .ToList();

            foreach (var type in assemblyVariants)
            {
                var registrationInstance = (IServiceRegistration)Activator.CreateInstance(type);
                registrationInstance.RegisterServices(builder.Services, builder.Configuration);
            }
        }

        return builder;
    }

    private static List<Assembly> GetReferencedAssemblies()
    {
        var assemblies = new List<Assembly>();
        var dependencies = DependencyContext.Default.RuntimeLibraries;

        foreach (var library in dependencies)
        {
            if (IsCandidateCompilationLibrary(library))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
        }

        return assemblies;
    }

    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary)
    {
        return compilationLibrary.Name == (Assembly.GetEntryAssembly()?.GetName().Name)
            || compilationLibrary.Dependencies.Any(d => d.Name.StartsWith(TopicAssembliesSuffix));
    }
}