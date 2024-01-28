using Duende.IdentityServer.EntityFramework.DbContexts;
using Identity.Web;
using Identity.Web.Data;
using Identity.Web.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(
            outputTemplate:
            "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddRazorPages();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

    builder.ConfigureIdentityServer(connectionString);

    builder.Services.AddScoped<IEventSink, EventDispatcher>();

    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("default", policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
    });

    var app = builder.Build();
    
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
        var persistedGrantDbContext = services.GetRequiredService<PersistedGrantDbContext>();
        var configurationDbContext = services.GetRequiredService<ConfigurationDbContext>();
        
        Log.Information("Applying database migrations...");
        applicationDbContext.Database.Migrate();
        persistedGrantDbContext.Database.Migrate();
        configurationDbContext.Database.Migrate();
        Log.Information("Database migrations applied successfully.");
    }

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

    app.UseStaticFiles();
    app.UseRouting();
    app.UseCors("default");
    app.UseIdentityServer();
    app.UseAuthorization();

    app.MapRazorPages()
        .RequireAuthorization();


    await Seed.WipeConfigurationDb(app);
    await Task.WhenAll(Seed.SeedClients(app), Seed.SeedApiScopes(app), Seed.SeedIdentityResources(app),
        Seed.SeedApiResources(app));

    app.Run();
}
catch (Exception ex) when
    (ex.GetType().Name is not "HostAbortedException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}