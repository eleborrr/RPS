﻿using MassTransit;
using RPS.Mongo.Services.Rating;
using RPS.Shared.Configs;

namespace RPS.Mongo.ServicesExtensions.RabbitMq;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitConfiguration = new RabbitMqConfig
        {
            Hostname = configuration["MessageBroker:Hostname"]!,
            Password = configuration["MessageBroker:Password"]!,
            Username = configuration["MessageBroker:Username"]!,
            Port = configuration["MessageBroker:Port"]!
        };
        
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<RatingConsumer>();
            
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var uri =
                    $"amqp://{rabbitConfiguration.Username}:{rabbitConfiguration.Password}@{rabbitConfiguration.Hostname}:{rabbitConfiguration.Port}";
                configurator.Host(uri);
                configurator.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}