using System;
using System.Collections.Generic;
using System.Linq;
using MassTransit.Core.Interfaces;
using MassTransit.Core.Models.Options;
using MassTransit.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Core;

internal class ConsumerBuilder : IConsumerBuilder
{
    private readonly List<Type> _consumerMessageTypes = new();
    private readonly List<Type> _respondMessageTypes = new();
    private readonly List<Type> _requestMessageTypes = new();

    private readonly IServiceCollection _services;
    private readonly IConfiguration _configuration;
    private readonly string _serviceName;

    public ConsumerBuilder(IServiceCollection services, IConfiguration configuration, string serviceName)
    {
        _services = services;
        _configuration = configuration;
        _serviceName = serviceName;
    }

    public IConsumerBuilder AddConsumer<T>()
    {
        _consumerMessageTypes.Add(typeof(T));

        return this;
    }

    public IConsumerBuilder AddRespondConsumer<T>()
    {
        _respondMessageTypes.Add(typeof(T));

        return this;
    }

    public IConsumerBuilder AddRequestClient<T>()
    {
        _requestMessageTypes.Add(typeof(T));

        return this;
    }

    public IServiceCollection Build()
    {
        var rabbitMqConfiguration = _configuration.GetSection(nameof(RabbitMqOptions));
        _services.Configure<RabbitMqOptions>(rabbitMqConfiguration);

        var rabbitMqOptions = rabbitMqConfiguration.Get<RabbitMqOptions>();
        _services.AddMassTransit(busConfig =>
        {
            var consumers = _consumerMessageTypes.Select(i => typeof(ConsumerWrapper<>).MakeGenericType(i)).ToList();
            foreach (var consumer in consumers)
            {
                busConfig.AddConsumer(consumer);
            }

            var respondConsumers = _respondMessageTypes.Select(i => typeof(RespondConsumerWrapper<>).MakeGenericType(i)).ToList();
            foreach (var respondConsumer in respondConsumers)
            {
                busConfig.AddConsumer(respondConsumer);
            }

            busConfig.UsingRabbitMq((context, rabbitConfig) =>
            {
                rabbitConfig.Host(rabbitMqOptions.Uri, "/", rabbitHostConfig =>
                {
                    rabbitHostConfig.Username(rabbitMqOptions.UserName);
                    rabbitHostConfig.Password(rabbitMqOptions.Password);
                });

                var allConsumers = consumers.Concat(respondConsumers);
                foreach (var consumer in allConsumers)
                {
                    rabbitConfig.ReceiveEndpoint($"{_serviceName}-{consumer.GenericTypeArguments.First().Name.ToLowerInvariant()}",
                        c => { c.ConfigureConsumer(context, consumer); });
                }
            });

            foreach (var respondMessageType in _requestMessageTypes)
            {
                busConfig.AddRequestClient(respondMessageType);
            }
        });

        _services.AddScoped<IProducerService, ProducerService>();
        _services.AddMassTransitHostedService();

        return _services;
    }
}