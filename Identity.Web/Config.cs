using Duende.IdentityServer.Models;

namespace Identity.Web;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("activities.read", "Reads  activities."),
            new ApiScope("activities.write", "Create  activities."),
            new ApiScope("manage", "Provides administrative access.")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource("activities", "Activities API")
            {
                Scopes = { "activities.read", "activities.write", "manage" }
            }
        };


    public static IEnumerable<Client> Clients =>
        new[]
        {
            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "Activities_ClientApp",
                ClientName = "Activities Client App",
                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "http://localhost:3000/signin-oidc" },
                FrontChannelLogoutUri = "http://localhost:3000/signout-oidc",
                PostLogoutRedirectUris = { "http://localhost:3000/signout-callback-oidc" },

                AllowOfflineAccess = true,
                RequireClientSecret = false,
                AllowedScopes = { "openid", "profile", "activities.read", "activities.write", "manage" }
            },

            new Client
            {
                ClientId = "Activities_Api",
                ClientName = "Activities Api",
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "activities.read", "activities.write", "manage" }
            }
        };
}