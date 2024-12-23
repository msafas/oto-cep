using CarApp.Configs.Common;

namespace CarApp.Configs;

public class BaseConfig
{
    public AuthConfig Auth { get; set; } = new();
    public DbConfig Database { get; set; } = new();
    public RedisConfig Redis { get; set; } = new();
    public string Enviroment { get; set; } = BaseConfigConstants.ConfigName;
}

public static class BaseConfigConstants
{
    public static readonly string ConfigName = Environment.GetEnvironmentVariable("CONFIG_NAME") ?? "Development";
}