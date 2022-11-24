using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Mediator.Extensions;

public static class MediatRServiceCollectionExtensions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services, Assembly assembly)
    {
        MediatR.ServiceCollectionExtensions.AddMediatR(services, assembly);

        return services;
    }
}