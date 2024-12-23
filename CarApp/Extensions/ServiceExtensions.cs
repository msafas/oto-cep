using System.Globalization;
using CarApp.Services;
using CarApp.Services.Interfaces;
using Microsoft.AspNetCore.Localization;
using MongoDB.Driver;
using StackExchange.Redis;

namespace CarApp.Extensions;

public static class ServiceExtensions
{
    public static void Configure(this IServiceCollection services)
    {
        services.AddScoped<ISharedIdentityService, SharedIdentityService>();



        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();

        services.AddAutoMapper(typeof(Program));
        var supportedCultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("tr-TR")
        };

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("en-US");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
    }

    public static void ConfigureRedisConnection(this IServiceCollection services,
        ConfigurationOptions configurationOptions, string? environment = "dev")
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = $"CarApp-{environment}-";
            options.ConfigurationOptions = configurationOptions;
        });

        services.AddTransient(_ => ConnectionMultiplexer.Connect(configurationOptions));
    }

    public static void ConfigureMongoDbContext(this IServiceCollection services, string connectionString,string database)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
        services.AddSingleton(new MongoClient(connectionString).GetDatabase(database));
    }

    public static void ConfigureSession(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(60);
            options.Cookie.HttpOnly = false;
            options.IOTimeout = TimeSpan.FromMinutes(60);
            options.Cookie.IsEssential = true;
        });
    }
}