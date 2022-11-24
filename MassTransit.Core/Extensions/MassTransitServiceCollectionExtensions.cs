using System.Reflection;
using MassTransit.Core.Interfaces;
using MassTransit.Core.Models.Options;
using MassTransit.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Core.Extensions;

public static class MassTransitServiceCollectionExtensions
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
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
        });

        services.AddScoped<IProducerService, ProducerService>();

        return services;
    }

    public static IServiceCollection AddMassTransitConsumer(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfiguration = configuration.GetSection(nameof(RabbitMqOptions));
        services.Configure<RabbitMqOptions>(rabbitMqConfiguration);

        var rabbitMqOptions = rabbitMqConfiguration.Get<RabbitMqOptions>();
        services.AddMassTransit(busConfig =>
        {
            busConfig.AddConsumer<CreateMessageCommandConsumer>();
            busConfig.UsingRabbitMq((context, rabbitConfig) =>
            {
                rabbitConfig.Host(rabbitMqOptions.Uri, "/", rabbitHostConfig =>
                {
                    rabbitHostConfig.Username(rabbitMqOptions.UserName);
                    rabbitHostConfig.Password(rabbitMqOptions.Password);
                });

                rabbitConfig.ReceiveEndpoint("create-message", c => {
                    c.ConfigureConsumer<CreateMessageCommandConsumer>(context);
                });
            });
        });

        services.AddScoped<IProducerService, ProducerService>();
        services.AddMassTransitHostedService();

        return services;
    }
}