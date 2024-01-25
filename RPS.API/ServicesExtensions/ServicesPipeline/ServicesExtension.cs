using RPS.API.ServicesExtensions.Auth;
using RPS.API.ServicesExtensions.Database;
using RPS.API.ServicesExtensions.Identity;
using RPS.API.ServicesExtensions.MassTransit;
using RPS.API.ServicesExtensions.SecurityAndCors;
using RPS.API.ServicesExtensions.Services;
using RPS.API.ServicesExtensions.Swagger;

namespace RPS.API.ServicesExtensions.ServicesPipeline;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddServicesPipeline(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCustomCors("testSpecific");
        services.AddCustomAuth(configuration);
        services.AddCustomServices(configuration);
        services.AddMsSql(configuration);
        services.AddSignalR();
        services.AddIdentity();
        services.AddMasstransitRabbitMq(configuration);
        services.AddCustomSwaggerGenerator();
        return services;
    }
}