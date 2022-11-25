using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Core.Interfaces;

public interface IConsumerBuilder
{
    IConsumerBuilder AddConsumer<T>();

    IConsumerBuilder AddRespondConsumer<T>();

    IConsumerBuilder AddRequestClient<T>();

    IServiceCollection Build(IConfiguration configuration, string serviceName);
}