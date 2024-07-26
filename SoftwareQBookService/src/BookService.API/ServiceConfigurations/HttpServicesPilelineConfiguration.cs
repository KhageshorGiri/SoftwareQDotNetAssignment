using BookService.API.Middlewares;
using BookService.Application.DependencyConfigurationExtension;
using BookService.Application.Dtos;
using Microsoft.OpenApi.Models;

namespace BookService.API.ServiceConfigurations;

public static class HttpServicesPilelineConfiguration
{
    public static void ServicesPilelineConfiguration(this WebApplicationBuilder builder)
    {
        RegisterServicesConfiguration(builder);
        RegisterServices(builder);
    }
    
    private static void RegisterServices(WebApplicationBuilder builder)
    {
        // Register application layer services
        builder.Services.AddServicesDependencyConfiguration();
    }

    private static void RegisterServicesConfiguration(WebApplicationBuilder builder)
    {
        // Build in services
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddJwtTokenValidator(builder.Configuration.GetSection("ApiSettings:JwtOptions:Secret").Value); // TODO : Need To get form secret manager

        SwaggerConfiguration(builder);

        //health checks
        builder.Services.AddHealthChecks();

        //Exception Handeler
        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

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
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Service API", Version = "v1" });

            // Add JWT Bearer Authentication
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

}
