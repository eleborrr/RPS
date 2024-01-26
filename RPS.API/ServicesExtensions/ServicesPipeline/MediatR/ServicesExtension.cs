using RPS.Application.Helpers;

namespace RPS.API.ServicesExtensions.ServicesPipeline.MediatR;

public static class ServicesExtension
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly);
            configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        return services;
    }
}