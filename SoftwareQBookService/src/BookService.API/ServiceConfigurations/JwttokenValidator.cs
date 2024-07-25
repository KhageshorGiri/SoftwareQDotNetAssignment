using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookService.API.ServiceConfigurations;

public static class JwttokenValidator
{

    public static void AddJwtTokenValidator(this IServiceCollection services, string secretKey)
    {

        // Configure JWT authentication
        var key = Encoding.ASCII.GetBytes(secretKey);

        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            option.SaveToken = true;
            option.RequireHttpsMetadata = false;
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };
        });
    }
}
