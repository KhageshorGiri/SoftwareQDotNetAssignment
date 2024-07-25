using BookService.Domain.Entities;
using BookService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookService.API.ServiceConfigurations;

public static class DbConfiguration
{
    public static void RegisterDbConfiguration(this WebApplicationBuilder builder)
    {
        // Add DbContext Service
        builder.Services.AddDbContext<BookServiceDbContext>(options =>options
                    .UseInMemoryDatabase("InMemoryDb"));

        // Add Identity services
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<BookServiceDbContext>()
            .AddDefaultTokenProviders();

    }

    
}
