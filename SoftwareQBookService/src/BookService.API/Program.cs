using BookService.API.ServiceConfigurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Loger configuration
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    // Configure serilog logger globally.
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);

    logger.Information("Starting web application");

    // Add services to the container.
    builder.RegisterDbConfiguration();
    builder.ServicesPilelineConfiguration();

    var app = builder.Build();
    // Configure the HTTP request / Middleware pipeline.
    app.MiddleWarePipelIne();
    app.Run();
}
catch (Exception ex)
{
    logger.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

