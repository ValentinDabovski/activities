using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", p =>
                {
                    p.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");

                });
            });

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddMediatR(typeof(Application.Activities.List));
        
            services.AddAutoMapper(typeof(Application.Mapping.MappingProfile).Assembly);

            return services;
        }
    }
}