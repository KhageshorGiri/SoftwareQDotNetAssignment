using BookService.Domain.IRepositories;
using BookService.Infrastructure.Repositories;
using BookService.Application.Businesses;
using BookService.Application.IBusinesses;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookService.Application.DependencyConfigurationExtension;

public static class ServiceDependencyConfigurationExtension
{
    public static IServiceCollection AddServicesDependencyConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        AddBussinessDIConfiguration(services);
        AddDataDIConfiguration(services);
        return services;
    }

    private static IServiceCollection AddDataDIConfiguration(IServiceCollection services)
    {
        // Data
        services.AddScoped<IBookResepository, BookRepository>();
        return services;
    }

    private static IServiceCollection AddBussinessDIConfiguration(IServiceCollection services)
    {
        // Business
        services.AddScoped<IJwtTokenProvider, JswTokenProvider>();
        services.AddScoped<IAuthService, AuthsService>();
        services.AddScoped<IBookBusiness, BookBusiness>();
        return services;
    }
}
