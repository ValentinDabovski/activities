using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Extensions;

public static class Authentication
{
    public static void ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5002";
                options.Audience = "activities";

                options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
            });
    }
}