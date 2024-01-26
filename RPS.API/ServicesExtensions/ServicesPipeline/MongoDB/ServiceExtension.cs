﻿using RPS.Infrastructure.MongoClient;
using RPS.Shared.Configs;

namespace RPS.API.ServicesExtensions.ServicesPipeline.MongoDB;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMongo(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MongoDbConfig>
            (configuration.GetSection("Mongo"));
        services.AddScoped<IMongoDbClient, MongoDbClient>();
        
        return services;
    }
    
}