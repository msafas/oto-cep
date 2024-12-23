using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CarApp.Configs;
using CarApp.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace CarApp.Extensions;

public static class ProgramExtensions
{
    public static WebApplicationBuilder ConfigureApplication(this WebApplicationBuilder builder)
    {
        var currentConfig = builder.Configuration.GetConfig<BaseConfig>(BaseConfigConstants.ConfigName);
        builder.Services.ConfigureCorsPolicy();


        builder.Services.AddTransient<BaseConfig>(_ => currentConfig);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        #region Serialisation

        // Add Json Options
        builder.Services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            opt.SerializerOptions.PropertyNameCaseInsensitive = true;
            opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            opt.SerializerOptions.PropertyNameCaseInsensitive = true;
        });

        #endregion Serialisation



        #region Project Dependencies

        // Configure Database Connection
        builder.Services.ConfigureMongoDbContext(currentConfig.Database.MongoConnectionString,currentConfig.Database.Database);

        // Configure Redis Connection
        builder.Services.ConfigureRedisConnection(currentConfig.Redis.ConfigurationOptions,
            currentConfig.Enviroment);


        // Configure Identity Session
        builder.Services.ConfigureSession();

        #endregion Project Dependencies


        #region Authentication

        IdentityModelEventSource.ShowPII = true;

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = currentConfig.Auth.ValidIssuer,
                    ValidAudience = currentConfig.Auth.ValidAudience,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(currentConfig.Auth.SecretKey!).ToArray())
                };
            });

        #endregion

        #region Authorization

        builder.Services.AddAuthorization();

        #endregion

        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
            loggerConfiguration
                .MinimumLevel.Information()
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
        );

        return builder;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        #region Configuration

        // Get Vault Configuration
        var config = app.Services.GetRequiredService<BaseConfig>();

        #endregion

        #region Exceptions

        // Add Global Exception Handler
        app.UseGlobalExceptionHandler();

        #endregion Exceptions

        #region Security

        app.UseHsts();
        app.UseCors();

        #endregion Security

        #region Authentication

        app.UseAuthentication();
        app.UseSession();
        app.UseMiddleware<AuthenticationMiddleware>();

        #endregion

        #region Authorization

        app.UseAuthorization();

        #endregion

        #region API Configuration

        app.UseStaticFiles();
        app.UseHttpsRedirection();

        #endregion API Configuration

        app.MapControllers();

        app.Lifetime.ApplicationStarted.Register(() =>
        {
        });

        return app;
    }
}