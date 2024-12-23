namespace CarApp.Configs.Common;

public sealed class DbConfig
{
    public string? Host { get; set; }
    public string? Port { get; set; }
    public string? Database { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Environment { get; set; }

    public string PostgresConnectionString =>
        $"User ID={Username};Password={Password};Host={Host};Port={Port};Database={Database};{Environment}";

    public string SqlServerConnectionString =>
        $"Server={Host},{Port};Database={Database};User Id={Username};Password={Password};{Environment}";

    public string MongoConnectionString =>
        $"mongodb://{Username}:{Password}@{Host}:{Port}/{Database}?authSource=admin";
}