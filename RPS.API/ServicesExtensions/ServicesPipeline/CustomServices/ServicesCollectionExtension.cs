using MassTransit;
using RPS.Application.Helpers.JwtGenerator;
using RPS.Application.Services;
using RPS.Application.Services.Abstractions;
using RPS.Domain.Repositories.Abstractions;
using RPS.Infrastructure.Database.Repositories;

namespace RPS.API.ServicesExtensions.Services;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<HttpClient>();

        services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());

        return services;
    }
}