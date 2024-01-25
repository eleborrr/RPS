using Microsoft.EntityFrameworkCore;
using RPS.Infrastructure.Database;

namespace RPS.API.ServicesExtensions.Database;

public static class ServicesExtension
{
    public static IServiceCollection AddMsSql(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("BeaverTinderDatabase"));
            options.EnableSensitiveDataLogging();
        });
        return services;
    }
}