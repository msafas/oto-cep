using StackExchange.Redis;

namespace CarApp.Configs.Common;

public class RedisConfig
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? Password { get; set; }
    public int Database { get; set; }
    public bool IsSsl { get; set; }

    public ConfigurationOptions ConfigurationOptions =>
        new()
        {
            EndPoints = { $"{Host}:{Port}" },
            Password = Password,
            Ssl = IsSsl
        };

    public string? InstanceName { get; set; }
}