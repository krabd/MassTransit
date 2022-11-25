using MassTransit.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Core.Extensions;

public static class MassTransitServiceCollectionExtensions
{
    public static IConsumerBuilder AddMassTransit(this IServiceCollection services)
    {
        var consumerBuilder = new ConsumerBuilder(services);

        return consumerBuilder;
    }
}