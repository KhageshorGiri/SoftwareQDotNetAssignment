using BookService.Domain.IRepositories;
using BookService.Infrastructure.Repositories;
using BoosService.Application.Businesses;
using BoosService.Application.IBusinesses;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BoosService.Application.DependencyConfiguration;

public static class ServiceDependencyConfiguration
{
    public static IServiceCollection AddServicesDIConfiguration(this IServiceCollection services)
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
        services.AddScoped<IBookBusiness, BookBusiness>();
        return services;
    }
}
