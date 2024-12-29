using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;

namespace SimpleApi;

public static class YamlConfigurationExtensions
{
    public static IConfigurationBuilder ConfigureYaml(this IConfigurationBuilder builder, IWebHostEnvironment env)
    {
        var jsonSources = builder.Sources.Where(s => s is JsonConfigurationSource).ToList();
        foreach (var source in jsonSources)
            builder.Sources.Remove(source);

        builder.AddYamlFile("appsettings.yaml", optional: true, reloadOnChange: true);
        builder.AddYamlFile($"appsettings.{env.EnvironmentName}.yaml", optional: true, reloadOnChange: true);

        var rawEnvSource = builder.Sources
            .FirstOrDefault(s => s is EnvironmentVariablesConfigurationSource { Prefix: null });
        if (rawEnvSource is not null)
            builder.Sources.Remove(rawEnvSource);

        builder.AddEnvironmentVariables("ASPNETCORE_");
        builder.AddEnvironmentVariables("DOTNET_");

        return builder;
    }
}
