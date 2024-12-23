using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

namespace CarApp.Extensions;

public static class ConfigurationExtensions
{
    public static T GetConfig<T>(this ConfigurationManager configurationManager, string name)
        where T : class, new()
    {
        var baseConfig = new T();
        configurationManager.GetSection("Config").Bind(baseConfig);

        return baseConfig;
    }
}