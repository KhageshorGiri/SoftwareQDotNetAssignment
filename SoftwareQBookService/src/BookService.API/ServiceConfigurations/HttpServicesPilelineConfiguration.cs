using BookService.API.Middlewares;
using BookService.Application.DependencyConfigurationExtension;
using BookService.Application.Dtos;

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
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
        // Build in services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        SwaggerConfiguration(builder);

        //health checks
        builder.Services.AddHealthChecks();

        //Exception Handeler
        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddJwtTokenValidator(builder.Configuration.GetSection("ApiSettings:JwtOptions:Secret").Value); // TODO : Need To get form secret manager

        // Add CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("BookServiceCorsPolicy",
                builder => builder
                    .WithOrigins("http://localhost:7141") // Specify allowed domains
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
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
