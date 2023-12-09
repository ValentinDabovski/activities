using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Identity.Web.Data;

public abstract class Seed
{
    public static async Task SeedClients(IApplicationBuilder app)
    {
        await using var context = GetDbContex(app);

        foreach (var client in Config.Clients)
            if (!await context.Clients.AnyAsync(c => c.ClientId == client.ClientId))
            {
                await context.Clients.AddAsync(client.ToEntity());

                await context.SaveChangesAsync();
            }
    }

    public static async Task SeedApiScopes(IApplicationBuilder app)
    {
        await using var context = GetDbContex(app);

        foreach (var apiScope in Config.ApiScopes)
            if (!await context.ApiScopes.AnyAsync(c => c.Name == apiScope.Name))
            {
                await context.ApiScopes.AddAsync(apiScope.ToEntity());

                await context.SaveChangesAsync();
            }
    }

    public static async Task SeedIdentityResources(IApplicationBuilder app)
    {
        await using var context = GetDbContex(app);

        foreach (var identityResource in Config.IdentityResources)
            if (!await context.IdentityResources.AnyAsync(c => c.Name == identityResource.Name))
            {
                await context.IdentityResources.AddAsync(identityResource.ToEntity());

                await context.SaveChangesAsync();
            }
    }

    public static async Task SeedApiResources(IApplicationBuilder app)
    {
        await using var context = GetDbContex(app);

        foreach (var apiResource in Config.ApiResources)
            if (!await context.ApiResources.AnyAsync(c => c.Name == apiResource.Name))
            {
                await context.ApiResources.AddAsync(apiResource.ToEntity());

                await context.SaveChangesAsync();
            }
    }

    public static async Task WipeConfigurationDb(IApplicationBuilder app)
    {
        await using var context = GetDbContex(app);

        await context.IdentityResources.ExecuteDeleteAsync();

        await context.Clients.ExecuteDeleteAsync();

        await context.ApiScopes.ExecuteDeleteAsync();
    }

    private static ConfigurationDbContext GetDbContex(IApplicationBuilder app)
    {
        var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        return serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
    }
}