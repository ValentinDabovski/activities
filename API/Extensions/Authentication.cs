namespace API.Extensions;

public static class Authentication
{
    public static void ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://localhost:5002";

                options.ClientId = "ActivitiesApi";
                options.ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A";
                options.ResponseType = "code";

                options.SaveTokens = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("activities-api");
                options.Scope.Add("offline_access");
                options.GetClaimsFromUserInfoEndpoint = true;
            });
    }
}