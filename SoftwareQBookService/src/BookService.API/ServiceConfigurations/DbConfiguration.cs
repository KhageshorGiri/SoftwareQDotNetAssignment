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
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        })
            .AddEntityFrameworkStores<BookServiceDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
        });
    }

    
}
