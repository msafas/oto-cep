using CarApp.Extensions;
using Serilog;

var builder = WebApplication
    .CreateBuilder(args).ConfigureApplication();

builder.Services.Configure();

var app = builder
    .Build().ConfigureApplication();

try
{
    Log.Information("Starting host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}