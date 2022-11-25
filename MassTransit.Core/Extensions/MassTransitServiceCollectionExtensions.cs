using MassTransit.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Core.Extensions;

public static class MassTransitServiceCollectionExtensions
{
    public static IConsumerBuilder AddMassTransit(this IServiceCollection services, IConfiguration configuration, string serviceName)
    {
        var consumerBuilder = new ConsumerBuilder(services, configuration, serviceName);

        return consumerBuilder;
    }
}