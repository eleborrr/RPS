using Microsoft.AspNetCore.Identity;
using RPS.Domain.Entities;
using RPS.Infrastructure.Database;

namespace RPS.API.ServicesExtensions.Identity;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.Configure<DataProtectionTokenProviderOptions>(
            o => o.TokenLifespan = TimeSpan.FromHours(24));
        return services;
    }
}