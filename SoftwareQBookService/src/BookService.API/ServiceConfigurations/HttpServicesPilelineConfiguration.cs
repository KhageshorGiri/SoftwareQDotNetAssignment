using BookService.API.Middlewares;
using BookService.Application.DependencyConfigurationExtension;

namespace BookService.API.ServiceConfigurations;

public static class HttpServicesPilelineConfiguration
{
    public static void ServicesPilelineConfiguration(this WebApplicationBuilder builder)
    {
        RegisterServicesConfiguration(builder);
        RegisterServices(builder);
    }

    private static void RegisterServicesConfiguration(WebApplicationBuilder builder)
    {
        // Build in services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        SwaggerConfiguration(builder);

        //health checks
        builder.Services.AddHealthChecks();

        //Exception Handeler
        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    }

    private static void SwaggerConfiguration(WebApplicationBuilder builder)
    {
        //builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerGenOptionConfiguration>();
        builder.Services.AddSwaggerGen();
    }
    private static void RegisterServices(WebApplicationBuilder builder)
    {
        // Register application layer services
        builder.Services.AddServicesDependencyConfiguration();
    }
}
