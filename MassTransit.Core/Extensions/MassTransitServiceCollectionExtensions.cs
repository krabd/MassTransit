using System;
using System.Linq;
using MassTransit.Core.Interfaces;
using MassTransit.Core.Models.Options;
using MassTransit.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Core.Extensions;

public static class MassTransitServiceCollectionExtensions
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration, params Type[] requestMessageTypes)
    {
        var rabbitMqConfiguration = configuration.GetSection(nameof(RabbitMqOptions));
        services.Configure<RabbitMqOptions>(rabbitMqConfiguration);

        var rabbitMqOptions = rabbitMqConfiguration.Get<RabbitMqOptions>();
        services.AddMassTransit(busConfig =>
        {
            busConfig.UsingRabbitMq((context, rabbitConfig) =>
            {
                rabbitConfig.Host(rabbitMqOptions.Uri, "/", rabbitHostConfig =>
                {
                    rabbitHostConfig.Username(rabbitMqOptions.UserName);
                    rabbitHostConfig.Password(rabbitMqOptions.Password);
                });
            });

            foreach (var requestMessageType in requestMessageTypes)
            {
                busConfig.AddRequestClient(requestMessageType);
            }
        });

        services.AddScoped<IProducerService, ProducerService>();
        services.AddMassTransitHostedService();

        return services;
    }

    public static IServiceCollection AddMassTransitConsumer(this IServiceCollection services, IConfiguration configuration, string serviceName, Type[] consumerMessageTypes, Type[] requestMessageTypes, Type[] respondMessageTypes)
    {
        var rabbitMqConfiguration = configuration.GetSection(nameof(RabbitMqOptions));
        services.Configure<RabbitMqOptions>(rabbitMqConfiguration);

        var rabbitMqOptions = rabbitMqConfiguration.Get<RabbitMqOptions>();
        services.AddMassTransit(busConfig =>
        {
            var consumers = consumerMessageTypes.Select(i => typeof(ConsumerWrapper<>).MakeGenericType(i)).ToList();
            foreach (var consumer in consumers)
            {
                busConfig.AddConsumer(consumer);
            }

            foreach (var respondMessageType in respondMessageTypes)
            {
                busConfig.AddConsumer(respondMessageType);
            }

            busConfig.UsingRabbitMq((context, rabbitConfig) =>
            {
                rabbitConfig.Host(rabbitMqOptions.Uri, "/", rabbitHostConfig =>
                {
                    rabbitHostConfig.Username(rabbitMqOptions.UserName);
                    rabbitHostConfig.Password(rabbitMqOptions.Password);
                });

                foreach (var consumer in consumers)
                {
                    rabbitConfig.ReceiveEndpoint($"{serviceName}-{consumer.GenericTypeArguments.First().Name.ToLowerInvariant()}",
                        c => { c.ConfigureConsumer(context, consumer); });
                }

                foreach (var respondMessageType in respondMessageTypes)
                {
                    rabbitConfig.ReceiveEndpoint($"{serviceName}-{respondMessageType.Name.ToLowerInvariant()}",
                        c => { c.ConfigureConsumer(context, respondMessageType); });
                }
            });

            foreach (var respondMessageType in requestMessageTypes)
            {
                busConfig.AddRequestClient(respondMessageType);
            }
        });

        services.AddScoped<IProducerService, ProducerService>();
        services.AddMassTransitHostedService();

        return services;
    }
}